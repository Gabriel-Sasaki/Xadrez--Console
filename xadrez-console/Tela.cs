using System;
using tabuleiro;

namespace xadrez_console
{
    internal class Tela
    {
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
