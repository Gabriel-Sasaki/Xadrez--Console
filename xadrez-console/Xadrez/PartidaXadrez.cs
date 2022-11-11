using System;
using Tabuleiros;
using Tabuleiros.Exceptions;

namespace Xadrez
{
    internal class PartidaXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }

        public PartidaXadrez()
        {
            // Cria um tabuleiro 8x8
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            ColocaPecas();
        }

        // Coloca as peças nas posições iniciais
        private void ColocaPecas()
        {
            Tabuleiro.ColocaPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('c', 1).ConvertePosicao());
            Tabuleiro.ColocaPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('c', 2).ConvertePosicao());
            Tabuleiro.ColocaPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('d', 2).ConvertePosicao());
            Tabuleiro.ColocaPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('e', 2).ConvertePosicao());
            Tabuleiro.ColocaPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('e', 1).ConvertePosicao());
            Tabuleiro.ColocaPeca(new Rei(Cor.Branca, Tabuleiro), new PosicaoXadrez('d', 1).ConvertePosicao());

            Tabuleiro.ColocaPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez('c', 8).ConvertePosicao());
            Tabuleiro.ColocaPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez('c', 7).ConvertePosicao());
            Tabuleiro.ColocaPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez('d', 7).ConvertePosicao());
            Tabuleiro.ColocaPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez('e', 7).ConvertePosicao());
            Tabuleiro.ColocaPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez('e', 8).ConvertePosicao());
            Tabuleiro.ColocaPeca(new Rei(Cor.Preta, Tabuleiro), new PosicaoXadrez('d', 8).ConvertePosicao());
        }

        // Retira a peça da posição de origem, incrementa a quantidade de movimentos
        // Retira a peça da posição de destino e coloca a peça que se moveu no lugar
        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tabuleiro.RetiraPeca(origem);
            peca.IncrementaQtdMovimentos();
            Peca pecaCapturada = Tabuleiro.RetiraPeca(destino);
            Tabuleiro.ColocaPeca(peca, destino);
        }

        // Realiza a execução da jogada mudando a posição, mudando o turno e o jogador atual
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudaJogadorAtual();
        }

        // Muda o jogador atual de branca para preta e vice-versa
        private void MudaJogadorAtual()
        {
            if(JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        // Valida se a posição de origem de uma jogada é válida
        public void ValidaPosicaoOrigem(Posicao pos)
        {
            Peca pecaAux = Tabuleiro.RetornaPeca(pos);

            if (pecaAux == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }

            if(JogadorAtual != pecaAux.Cor)
            {
                throw new TabuleiroException("A peça de origem não é sua!");
            }

            if (!pecaAux.ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        // Valida se a posição de destino de uma jogada é válida
        public void ValidaPosicaoDestino(Posicao origem, Posicao destino)
        {
            Peca pecaOrigem = Tabuleiro.RetornaPeca(origem);

            if (!pecaOrigem.PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }
    }
}
