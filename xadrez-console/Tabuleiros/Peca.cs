using System;

namespace Tabuleiros
{
    // Classe representativa da peça no tabuleiro
    internal abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; protected set; }

        public Peca(Cor cor, Tabuleiro tabuleiro)
        {
            Posicao = null;
            Cor = cor;
            Tabuleiro = tabuleiro;
            QtdMovimentos = 0;
        }

        public void IncrementaQtdMovimentos()
        {
            QtdMovimentos++;
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}
