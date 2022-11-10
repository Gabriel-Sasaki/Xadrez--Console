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
                Tabuleiro tab = new Tabuleiro(8, 8);

                Torre torre = new Torre(Cor.Preta, tab);
                Rei rei = new Rei(Cor.Preta, tab);

                tab.ColocarPeca(torre, new Posicao(0, 0));
                tab.ColocarPeca(torre, new Posicao(1, 3));
                tab.ColocarPeca(rei, new Posicao(2, 4));

                Tela.ImprimirTabuleiro(tab);
            }
            catch(TabuleiroException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}