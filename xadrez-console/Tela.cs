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
                Console.Write(8 - x + " ");

                for (int y = 0; y < tabuleiro.Colunas; y++)
                {
                    Peca peca = tabuleiro.RetornaPeca(x, y);

                    if(peca != null)
                    {
                        Tela.ImprimirPeca(peca);
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }

                }

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirPeca(Peca peca)
        {
            if(peca.Cor == Cor.Branca)
            {
                Console.Write(peca);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }
    }
}
