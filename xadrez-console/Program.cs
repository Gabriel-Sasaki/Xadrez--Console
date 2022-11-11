using Tabuleiros;
using Tabuleiros.Exceptions;
using Xadrez;

namespace xadrez_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaXadrez partida = new PartidaXadrez();

                while (!partida.Terminada)
                {
                    Console.Clear();

                    Tela.ImprimirTabuleiro(partida.Tabuleiro);

                    Console.Write("\nOrigem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ConvertePosicao();

                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ConvertePosicao();

                    partida.ExecutaMovimento(origem, destino);
                }

                Tela.ImprimirTabuleiro(partida.Tabuleiro);
            }
            catch(TabuleiroException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}