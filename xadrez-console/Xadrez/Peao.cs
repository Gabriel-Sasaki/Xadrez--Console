using System;
using Tabuleiros;

namespace Xadrez
{
    internal class Peao : Peca
    {
        public Peao(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro)
        {

        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] movPossiveis = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao pos = new Posicao(0, 0);

            if(Cor == Cor.Branca)
            {
                pos.DefineValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tabuleiro.IsPosicaoValida(pos) && IsFrenteLivre(pos))
                {
                    movPossiveis[pos.Linha, pos.Coluna] = true;
                }

                pos.DefineValores(Posicao.Linha - 2, Posicao.Coluna);
                if (Tabuleiro.IsPosicaoValida(pos) && IsFrenteLivre(pos) && QtdMovimentos == 0)
                {
                    movPossiveis[pos.Linha, pos.Coluna] = true;
                }

                pos.DefineValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tabuleiro.IsPosicaoValida(pos) && IsInimigoDiagonal(pos))
                {
                    movPossiveis[pos.Linha, pos.Coluna] = true;
                }

                pos.DefineValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tabuleiro.IsPosicaoValida(pos) && IsInimigoDiagonal(pos))
                {
                    movPossiveis[pos.Linha, pos.Coluna] = true;
                }
            }
            else
            {
                pos.DefineValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.IsPosicaoValida(pos) && IsFrenteLivre(pos))
                {
                    movPossiveis[pos.Linha, pos.Coluna] = true;
                }

                pos.DefineValores(Posicao.Linha + 2, Posicao.Coluna);
                if (Tabuleiro.IsPosicaoValida(pos) && IsFrenteLivre(pos) && QtdMovimentos == 0)
                {
                    movPossiveis[pos.Linha, pos.Coluna] = true;
                }

                pos.DefineValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tabuleiro.IsPosicaoValida(pos) && IsInimigoDiagonal(pos))
                {
                    movPossiveis[pos.Linha, pos.Coluna] = true;
                }

                pos.DefineValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tabuleiro.IsPosicaoValida(pos) && IsInimigoDiagonal(pos))
                {
                    movPossiveis[pos.Linha, pos.Coluna] = true;
                }
            }

            return movPossiveis;
        }

        private bool IsInimigoDiagonal(Posicao pos)
        {
            Peca peca = Tabuleiro.RetornaPeca(pos);
            return peca != null && peca.Cor != Cor;
        }

        private bool IsFrenteLivre(Posicao pos)
        {
            return Tabuleiro.RetornaPeca(pos) == null;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
