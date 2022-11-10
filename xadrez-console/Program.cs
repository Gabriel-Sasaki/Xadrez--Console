using Tabuleiros;
using Xadrez;

namespace xadrez_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);

            Torre torre1 = new Torre(Cor.Preta, tab);
            Torre torre2 = new Torre(Cor.Preta, tab);
            Rei rei1 = new Rei(Cor.Preta, tab);

            tab.ColocarPeca(torre1, new Posicao(0, 0));
            tab.ColocarPeca(torre2, new Posicao(1, 3));
            tab.ColocarPeca(rei1, new Posicao(2, 4));

            Tela.ImprimirTabuleiro(tab);
        }
    }
}