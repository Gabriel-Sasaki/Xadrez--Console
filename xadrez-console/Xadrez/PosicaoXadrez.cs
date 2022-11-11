using System;
using Tabuleiros;

namespace Xadrez
{
    // Classe para converter a posição do xadrez (letra, número) para posição da matriz (número, número)
    internal class PosicaoXadrez
    {
        public char Coluna { get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }

        // Método de conversão
        public Posicao ConvertePosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'a');
        }

        public override string ToString()
        {
            return $"{Coluna}{Linha}";
        }
    }
}
