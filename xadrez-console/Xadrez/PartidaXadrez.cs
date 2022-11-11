using System;
using Tabuleiros;

namespace Xadrez
{
    internal class PartidaXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        private int _turno;
        private Cor _jogadorAtual;
        public bool Terminada { get; private set; }

        public PartidaXadrez()
        {
            // Cria um tabuleiro 8x8
            Tabuleiro = new Tabuleiro(8, 8);
            _turno = 1;
            _jogadorAtual = Cor.Branca;
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
    }
}
