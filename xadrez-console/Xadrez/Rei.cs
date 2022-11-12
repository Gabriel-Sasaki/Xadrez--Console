using System;
using Tabuleiros;

namespace Xadrez
{
    internal class Rei : Peca
    {
        private PartidaXadrez _partida;

        public Rei(Cor cor, Tabuleiro tabuleiro, PartidaXadrez partida) : base(cor, tabuleiro)
        {
            _partida= partida;
        }

        // Verifica os movimentos possíveis para o Rei
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] movPossiveis = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao pos = new Posicao(0, 0);

            pos.DefineValores(Posicao.Linha - 1, Posicao.Coluna);
            if(Tabuleiro.IsPosicaoValida(pos) && IsLocalValido(pos))
            {
                movPossiveis[pos.Linha, pos.Coluna] = true;
            }

            pos.DefineValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.IsPosicaoValida(pos) && IsLocalValido(pos))
            {
                movPossiveis[pos.Linha, pos.Coluna] = true;
            }

            pos.DefineValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tabuleiro.IsPosicaoValida(pos) && IsLocalValido(pos))
            {
                movPossiveis[pos.Linha, pos.Coluna] = true;
            }

            pos.DefineValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.IsPosicaoValida(pos) && IsLocalValido(pos))
            {
                movPossiveis[pos.Linha, pos.Coluna] = true;
            }

            pos.DefineValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.IsPosicaoValida(pos) && IsLocalValido(pos))
            {
                movPossiveis[pos.Linha, pos.Coluna] = true;
            }

            pos.DefineValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.IsPosicaoValida(pos) && IsLocalValido(pos))
            {
                movPossiveis[pos.Linha, pos.Coluna] = true;
            }

            pos.DefineValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tabuleiro.IsPosicaoValida(pos) && IsLocalValido(pos))
            {
                movPossiveis[pos.Linha, pos.Coluna] = true;
            }

            pos.DefineValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.IsPosicaoValida(pos) && IsLocalValido(pos))
            {
                movPossiveis[pos.Linha, pos.Coluna] = true;
            }

            // #jogadaespecial : roque
            if(QtdMovimentos == 0 && !_partida.Xeque)
            {
                // Roque pequeno
                Posicao posicaoTorre1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if (TesteTorreParaRoque(posicaoTorre1))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);

                    if(Tabuleiro.RetornaPeca(p1) == null && Tabuleiro.RetornaPeca(p2) == null)
                    {
                        movPossiveis[Posicao.Linha, Posicao.Coluna + 2] = true;
                    }
                }

                // Roque grande
                Posicao posicaoTorre2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                if (TesteTorreParaRoque(posicaoTorre2))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);

                    if (Tabuleiro.RetornaPeca(p1) == null && Tabuleiro.RetornaPeca(p2) == null
                        && Tabuleiro.RetornaPeca(p3) == null)
                    {
                        movPossiveis[Posicao.Linha, Posicao.Coluna - 2] = true;
                    }
                }
            }

            return movPossiveis;
        }

        private bool TesteTorreParaRoque(Posicao pos)
        {
            Peca peca = Tabuleiro.RetornaPeca(pos);
            return peca != null && peca is Torre && peca.Cor == Cor && peca.QtdMovimentos == 0;
        }

        private bool IsLocalValido(Posicao pos)
        {
            Peca peca = Tabuleiro.RetornaPeca(pos);
            return peca == null || peca.Cor != Cor;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
