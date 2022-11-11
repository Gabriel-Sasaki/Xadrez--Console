using System;
using Tabuleiros;

namespace Xadrez
{
    internal class Rei : Peca
    {
        public Rei(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro)
        {

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

            return movPossiveis;
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
