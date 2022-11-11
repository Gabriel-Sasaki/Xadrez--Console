using System;
using Tabuleiros;
using Xadrez;
using System.Collections.Generic;

namespace xadrez_console
{
    internal class Tela
    {

        // Imprime os dados da partida na tela
        public static void ImprimePartida(PartidaXadrez partida)
        {
            ImprimeTabuleiro(partida.Tabuleiro);

            Console.WriteLine();
            ImprimePecasCapturas(partida);

            Console.WriteLine($"\nTurno: {partida.Turno}" +
                $"\nAguardando jogada: {partida.JogadorAtual}");

            if (partida.Xeque)
            {
                Console.WriteLine("VOCÊ ESTÁ EM XEQUE!");
            }
        }
        
        // Imprime as peças capturadas
        private static void ImprimePecasCapturas(PartidaXadrez partida)
        {
            Console.WriteLine("Peças capturadas:");

            Console.Write("Brancas: ");
            ImprimeConjunto(partida.PecasCapturadas(Cor.Branca));
            
            Console.Write("\nPretas: ");

            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimeConjunto(partida.PecasCapturadas(Cor.Preta));
            Console.ForegroundColor= aux;

            Console.WriteLine();
        }

        // Imprime o conjunto de peças informado
        public static void ImprimeConjunto(HashSet<Peca> pecas)
        {
            Console.Write("[ ");

            foreach(var peca in pecas)
            {
                Console.Write(peca + " ");
            }

            Console.Write("]");
        }

        // Imprime a matriz na tela. Onde tem peça coloca a letra representativa do método ToString()
        // Onde não tem peça coloca um hífen -
        public static void ImprimeTabuleiro(Tabuleiro tabuleiro)
        {
            for(int x = 0; x < tabuleiro.Linhas; x++)
            {
                Console.Write(8 - x + " ");

                for (int y = 0; y < tabuleiro.Colunas; y++)
                {
                    Peca peca = tabuleiro.RetornaPeca(x, y);

                    ImprimePeca(peca);
                }

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
        }

        // Faz o mesmo que o método de menor parâmetro, porém mostra as posições das jogadas
        // possíveis em cinza
        public static void ImprimeTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int x = 0; x < tabuleiro.Linhas; x++)
            {
                Console.Write(8 - x + " ");

                for (int y = 0; y < tabuleiro.Colunas; y++)
                {
                    Peca peca = tabuleiro.RetornaPeca(x, y);

                    if (posicoesPossiveis[x, y])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }

                    ImprimePeca(peca);
                    Console.BackgroundColor = fundoOriginal;
                }

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
        }

        // Imprime a peça na posição definida com a cor definida
        public static void ImprimePeca(Peca peca)
        {
            if(peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Branca)
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

                Console.Write(" ");
            }
        }

        // Lê uma posição no formato do xadrez e retorna essa posição
        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string str = Console.ReadLine();
            char linha = str[0];
            int coluna = int.Parse($"{str[1]}");
            return new PosicaoXadrez(linha, coluna);
        }
    }
}
