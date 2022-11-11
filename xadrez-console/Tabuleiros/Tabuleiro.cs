using System;
using Tabuleiros.Exceptions;

namespace Tabuleiros
{
    // Representa o tabuleiro na tela
    internal class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] _pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            _pecas = new Peca[linhas, colunas];
        }

        // Retorna a peça localizada na linha e coluna informada
        public Peca RetornaPeca(int linha, int coluna)
        {
            return _pecas[linha, coluna];
        }

        // Retorna a peça localizada na posição informada
        public Peca RetornaPeca(Posicao pos)
        {
            return _pecas[pos.Linha, pos.Coluna];
        }

        // Coloca uma peça escolhida na posição escolhida
        public void ColocarPeca(Peca p, Posicao pos)
        {
            if (ExistePeca(pos))
            {
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            }
            _pecas[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;
        }

        // Valida se já existe uma peça na posição informada
        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return RetornaPeca(pos) != null;
        }

        // Lança uma exceção se o método PosicaoValida() resultar em false
        public void ValidarPosicao(Posicao pos)
        {
            if (!PosicaoValida(pos))
            {
                throw new TabuleiroException("Posição inválida!");
            }
        }

        // Verifica se a posição definida está fora da matriz (tabuleiro)
        public bool PosicaoValida(Posicao pos)
        {
            if(pos.Linha < 0 || pos.Linha >= Linhas || pos.Coluna < 0 || pos.Coluna >= Colunas)
            {
                return false;
            }

            return true;
        }
    }
}
