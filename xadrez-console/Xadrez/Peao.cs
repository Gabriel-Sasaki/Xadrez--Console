using System;
using Tabuleiros;

namespace Xadrez
{
    internal class Peao : Peca
    {
        private PartidaXadrez _partida;

        public Peao(Cor cor, Tabuleiro tabuleiro, PartidaXadrez partida) : base(cor, tabuleiro)
        {
            _partida = partida;
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
                if (Tabuleiro.IsPosicaoValida(pos) && IsExisteInimigo(pos))
                {
                    movPossiveis[pos.Linha, pos.Coluna] = true;
                }

                pos.DefineValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tabuleiro.IsPosicaoValida(pos) && IsExisteInimigo(pos))
                {
                    movPossiveis[pos.Linha, pos.Coluna] = true;
                }

                // #jogadaespecial : En Passant
                if (Posicao.Linha == 3)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);

                    if(Tabuleiro.IsPosicaoValida(esquerda) && IsExisteInimigo(esquerda)
                        && Tabuleiro.RetornaPeca(esquerda) == _partida.VulneravelEnPassant)
                    {
                        movPossiveis[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }

                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);

                    if (Tabuleiro.IsPosicaoValida(direita) && IsExisteInimigo(direita)
                        && Tabuleiro.RetornaPeca(direita) == _partida.VulneravelEnPassant)
                    {
                        movPossiveis[direita.Linha - 1, direita.Coluna] = true;
                    }
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
                if (Tabuleiro.IsPosicaoValida(pos) && IsExisteInimigo(pos))
                {
                    movPossiveis[pos.Linha, pos.Coluna] = true;
                }

                pos.DefineValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tabuleiro.IsPosicaoValida(pos) && IsExisteInimigo(pos))
                {
                    movPossiveis[pos.Linha, pos.Coluna] = true;
                }

                // #jogadaespecial : En Passant
                if (Posicao.Linha == 4)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);

                    if (Tabuleiro.IsPosicaoValida(esquerda) && IsExisteInimigo(esquerda)
                        && Tabuleiro.RetornaPeca(esquerda) == _partida.VulneravelEnPassant)
                    {
                        movPossiveis[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }

                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);

                    if (Tabuleiro.IsPosicaoValida(direita) && IsExisteInimigo(direita)
                        && Tabuleiro.RetornaPeca(direita) == _partida.VulneravelEnPassant)
                    {
                        movPossiveis[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }

            return movPossiveis;
        }

        private bool IsExisteInimigo(Posicao pos)
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
