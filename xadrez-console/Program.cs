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
                    try
                    {
                        Console.Clear();

                        Tela.ImprimePartida(partida);

                        Console.Write("\nOrigem: ");
                        Posicao origem = Tela.LerPosicaoXadrez().ConvertePosicao();
                        partida.ValidaPosicaoOrigem(origem);

                        bool[,] posicoesPossiveis = partida.Tabuleiro.RetornaPeca(origem).MovimentosPossiveis();

                        Console.Clear();
                        Tela.ImprimeTabuleiro(partida.Tabuleiro, posicoesPossiveis);

                        Console.Write("\nDestino: ");
                        Posicao destino = Tela.LerPosicaoXadrez().ConvertePosicao();
                        partida.ValidaPosicaoDestino(origem, destino);

                        partida.RealizaJogada(origem, destino);
                    }
                    catch(TabuleiroException e)
                    {
                        Console.WriteLine("Erro: " + e.Message);
                        Console.WriteLine("Clique ENTER para repetir a jogada!");
                        Console.ReadLine();
                    }
                    catch(FormatException)
                    {
                        Console.WriteLine("Erro: O formato da posição está incorreto. " +
                            "Utilize o padrão letra-número (exemplo: a1).");
                        Console.WriteLine("Clique ENTER para repetir a jogada!");
                        Console.ReadLine();
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("Erro: Posição fora do tabuleiro. Digite uma posição " +
                            "válida!");
                        Console.WriteLine("Clique ENTER para repetir a jogada!");
                        Console.ReadLine();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Erro inesperado: " + e.Message);
                        Console.WriteLine("Clique ENTER para repetir a jogada!");
                        Console.ReadLine();
                    }
                }

                Console.Clear();
                Tela.ImprimePartida(partida);
            }
            catch(TabuleiroException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}