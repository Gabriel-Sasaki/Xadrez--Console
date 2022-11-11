using System;
using Tabuleiros;

namespace xadrez_console
{
    internal class Tela
    {
        // Imprime a matriz na tela. Onde tem peça coloca a letra representativa do método ToString()
        // Onde não tem peça coloca um hífen -
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for(int x = 0; x < tabuleiro.Linhas; x++)
            {
                for(int y = 0; y < tabuleiro.Colunas; y++)
                {
                    Peca peca = tabuleiro.RetornaPeca(x, y);

                    if(peca != null)
                    {
                        Console.Write(tabuleiro.RetornaPeca(x, y) + " ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }

                }

                Console.WriteLine();
            }
        }
    }
}
