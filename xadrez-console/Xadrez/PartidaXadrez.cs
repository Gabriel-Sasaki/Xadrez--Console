using System;
using System.Collections.Generic;
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
        private HashSet<Peca> _pecas;
        private HashSet<Peca> _pecasCapturadas;

        public PartidaXadrez()
        {
            // Cria um tabuleiro 8x8
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            _pecas = new HashSet<Peca>();
            _pecasCapturadas= new HashSet<Peca>();
            ColocaPecas();
        }

        public void ColocaNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.ColocaPeca(peca, new PosicaoXadrez(coluna, linha).ConvertePosicao());
            _pecas.Add(peca);
        }

        // Coloca as peças nas posições iniciais
        private void ColocaPecas()
        {
            ColocaNovaPeca('c', 1, new Torre(Cor.Branca, Tabuleiro));
            ColocaNovaPeca('c', 2, new Torre(Cor.Branca, Tabuleiro));
            ColocaNovaPeca('d', 2, new Torre(Cor.Branca, Tabuleiro));
            ColocaNovaPeca('e', 2, new Torre(Cor.Branca, Tabuleiro));
            ColocaNovaPeca('e', 1, new Torre(Cor.Branca, Tabuleiro));
            ColocaNovaPeca('d', 1, new Rei(Cor.Branca, Tabuleiro));

            ColocaNovaPeca('c', 8, new Torre(Cor.Preta, Tabuleiro));
            ColocaNovaPeca('c', 7, new Torre(Cor.Preta, Tabuleiro));
            ColocaNovaPeca('d', 7, new Torre(Cor.Preta, Tabuleiro));
            ColocaNovaPeca('e', 7, new Torre(Cor.Preta, Tabuleiro));
            ColocaNovaPeca('e', 8, new Torre(Cor.Preta, Tabuleiro));
            ColocaNovaPeca('d', 8, new Rei(Cor.Preta, Tabuleiro));
        }

        // Retira a peça da posição de origem, incrementa a quantidade de movimentos
        // Retira a peça da posição de destino e coloca a peça que se moveu no lugar
        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tabuleiro.RetiraPeca(origem);
            peca.IncrementaQtdMovimentos();
            Peca pecaCapturada = Tabuleiro.RetiraPeca(destino);
            Tabuleiro.ColocaPeca(peca, destino);

            if(pecaCapturada != null)
            {
                _pecasCapturadas.Add(pecaCapturada);
            }
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

        // Retorna todas as peças em jogo de uma determinada cor
        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> pecasAux = new HashSet<Peca>();

            foreach (var peca in _pecas)
            {
                if (peca.Cor == cor)
                {
                    pecasAux.Add(peca);
                }
            }

            // Remove todas as peças que já foram capturas, ou seja, que não estão em jogo
            pecasAux.ExceptWith(PecasCapturadas(cor));

            return pecasAux;
        }

        // Retorna todas as peças capturadas de uma determinada cor
        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> pecasAux = new HashSet<Peca>();

            foreach(var pecaCapturada in _pecasCapturadas)
            {
                if(pecaCapturada.Cor == cor)
                {
                    pecasAux.Add(pecaCapturada);
                }
            }
            
            return pecasAux;
        }
    }
}
