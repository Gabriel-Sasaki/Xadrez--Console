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

                    Tela.ImprimeTabuleiro(partida.Tabuleiro);

                    Console.Write("\nOrigem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ConvertePosicao();

                    bool[,] posicoesPossiveis = partida.Tabuleiro.RetornaPeca(origem).MovimentosPossiveis();

                    Console.Clear();
                    Tela.ImprimeTabuleiro(partida.Tabuleiro, posicoesPossiveis);

                    Console.Write("\nDestino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ConvertePosicao();

                    partida.ExecutaMovimento(origem, destino);
                }

                Tela.ImprimeTabuleiro(partida.Tabuleiro);
            }
            catch(TabuleiroException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}