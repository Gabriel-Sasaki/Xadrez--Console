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

        internal void DecrementaQtdMovimentos()
        {
            QtdMovimentos--;
        }

        // Verifica se existe pelo menos um movimento possível
        // Se sim, retorna true, se não, retorna false
        public bool ExisteMovimentosPossiveis()
        {
            bool[,] movPossiveis = MovimentosPossiveis();

            for(int x = 0; x < Tabuleiro.Linhas; x++)
            {
                for(int y = 0; y < Tabuleiro.Colunas; y++)
                {
                    if (movPossiveis[x, y])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsMovimentoPossivel(Posicao pos)
        {
            return MovimentosPossiveis()[pos.Linha, pos.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}
