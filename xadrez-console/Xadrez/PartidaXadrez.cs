using System;
using System.Collections.Generic;
using Tabuleiros;
using Tabuleiros.Exceptions;

namespace Xadrez
{
    internal class PartidaXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> _pecas;
        private HashSet<Peca> _pecasCapturadas;
        public bool Xeque { get; private set; }
        public Peca VulneravelEnPassant { get; private set; }

        public PartidaXadrez()
        {
            // Cria um tabuleiro 8x8
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            VulneravelEnPassant = null;
            _pecas = new HashSet<Peca>();
            _pecasCapturadas= new HashSet<Peca>();
            ColocaPecas();
        }

        // Coloca uma peça em uma determinada posição
        public void ColocaNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.ColocaPeca(peca, new PosicaoXadrez(coluna, linha).ConvertePosicao());
            _pecas.Add(peca);
        }

        // Coloca as peças nas posições iniciais
        private void ColocaPecas()
        {
            // Peças brancas
            ColocaNovaPeca('a', 1, new Torre(Cor.Branca, Tabuleiro));
            ColocaNovaPeca('b', 1, new Cavalo(Cor.Branca, Tabuleiro));
            ColocaNovaPeca('c', 1, new Bispo(Cor.Branca, Tabuleiro));
            ColocaNovaPeca('d', 1, new Dama(Cor.Branca, Tabuleiro));
            ColocaNovaPeca('e', 1, new Rei(Cor.Branca, Tabuleiro, this));
            ColocaNovaPeca('f', 1, new Bispo(Cor.Branca, Tabuleiro));
            ColocaNovaPeca('g', 1, new Cavalo(Cor.Branca, Tabuleiro));
            ColocaNovaPeca('h', 1, new Torre(Cor.Branca, Tabuleiro));

            ColocaNovaPeca('a', 2, new Peao(Cor.Branca, Tabuleiro, this));
            ColocaNovaPeca('b', 2, new Peao(Cor.Branca, Tabuleiro, this));
            ColocaNovaPeca('c', 2, new Peao(Cor.Branca, Tabuleiro, this));
            ColocaNovaPeca('d', 2, new Peao(Cor.Branca, Tabuleiro, this));
            ColocaNovaPeca('e', 2, new Peao(Cor.Branca, Tabuleiro, this));
            ColocaNovaPeca('f', 2, new Peao(Cor.Branca, Tabuleiro, this));
            ColocaNovaPeca('g', 2, new Peao(Cor.Branca, Tabuleiro, this));
            ColocaNovaPeca('h', 2, new Peao(Cor.Branca, Tabuleiro, this));

            // Peças pretas
            ColocaNovaPeca('a', 8, new Torre(Cor.Preta, Tabuleiro));
            ColocaNovaPeca('b', 8, new Cavalo(Cor.Preta, Tabuleiro));
            ColocaNovaPeca('c', 8, new Bispo(Cor.Preta, Tabuleiro));
            ColocaNovaPeca('d', 8, new Dama(Cor.Preta, Tabuleiro));
            ColocaNovaPeca('e', 8, new Rei(Cor.Preta, Tabuleiro, this));
            ColocaNovaPeca('f', 8, new Bispo(Cor.Preta, Tabuleiro));
            ColocaNovaPeca('g', 8, new Cavalo(Cor.Preta, Tabuleiro));
            ColocaNovaPeca('h', 8, new Torre(Cor.Preta, Tabuleiro));

            ColocaNovaPeca('a', 7, new Peao(Cor.Preta, Tabuleiro, this));
            ColocaNovaPeca('b', 7, new Peao(Cor.Preta, Tabuleiro, this));
            ColocaNovaPeca('c', 7, new Peao(Cor.Preta, Tabuleiro, this));
            ColocaNovaPeca('d', 7, new Peao(Cor.Preta, Tabuleiro, this));
            ColocaNovaPeca('e', 7, new Peao(Cor.Preta, Tabuleiro, this));
            ColocaNovaPeca('f', 7, new Peao(Cor.Preta, Tabuleiro, this));
            ColocaNovaPeca('g', 7, new Peao(Cor.Preta, Tabuleiro, this));
            ColocaNovaPeca('h', 7, new Peao(Cor.Preta, Tabuleiro, this));
        }

        // Retira a peça da posição de origem, incrementa a quantidade de movimentos
        // Retira a peça da posição de destino e coloca a peça que se moveu no lugar
        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tabuleiro.RetiraPeca(origem);
            peca.IncrementaQtdMovimentos();
            Peca pecaCapturada = Tabuleiro.RetiraPeca(destino);
            Tabuleiro.ColocaPeca(peca, destino);

            if(pecaCapturada != null)
            {
                _pecasCapturadas.Add(pecaCapturada);
            }

            // #jogadaespecial Roque Pequeno
            if(peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);

                Peca pecaTorre = Tabuleiro.RetiraPeca(origemTorre);
                pecaTorre.IncrementaQtdMovimentos();
                Tabuleiro.ColocaPeca(pecaTorre, destinoTorre);
            }

            // #jogadaespecial Roque Grande
            if (peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);

                Peca pecaTorre = Tabuleiro.RetiraPeca(origemTorre);
                pecaTorre.IncrementaQtdMovimentos();
                Tabuleiro.ColocaPeca(pecaTorre, destinoTorre);
            }

            // #jogadaespecial En Passant
            if(peca is Peao && origem.Coluna != destino.Coluna && pecaCapturada == null)
            {
                Posicao posicaoPeao;

                if(peca.Cor == Cor.Branca)
                {
                    posicaoPeao = new Posicao(destino.Linha + 1, destino.Coluna);
                }
                else
                {
                    posicaoPeao = new Posicao(destino.Linha - 1, destino.Coluna);
                }

                pecaCapturada = Tabuleiro.RetiraPeca(posicaoPeao);
                _pecasCapturadas.Add(pecaCapturada);
            }

            return pecaCapturada;
        }

        // Desfaz o último movimento realizado
        private void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca peca = Tabuleiro.RetiraPeca(destino);
            peca.DecrementaQtdMovimentos();

            if (pecaCapturada != null)
            {
                Tabuleiro.ColocaPeca(pecaCapturada, destino);
                _pecasCapturadas.Remove(pecaCapturada);
            }

            Tabuleiro.ColocaPeca(peca, origem);

            // #jogadaespecial Roque Pequeno
            if (peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);

                Peca pecaTorre = Tabuleiro.RetiraPeca(destinoTorre);
                pecaTorre.DecrementaQtdMovimentos();
                Tabuleiro.ColocaPeca(pecaTorre, origemTorre);
            }

            // #jogadaespecial Roque Grande
            if (peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);

                Peca pecaTorre = Tabuleiro.RetiraPeca(destinoTorre);
                pecaTorre.DecrementaQtdMovimentos();
                Tabuleiro.ColocaPeca(pecaTorre, origemTorre);
            }

            // #jogadaespecial En Passant
            if(peca is Peao && origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
            {
                Peca peao = Tabuleiro.RetiraPeca(destino);
                Posicao posPeao;

                if(peao.Cor == Cor.Branca)
                {
                    posPeao = new Posicao(3, destino.Coluna);
                }
                else
                {
                    posPeao = new Posicao(4, destino.Coluna);
                }

                Tabuleiro.ColocaPeca(peao, posPeao);
            }
        }

        // Realiza a execução da jogada mudando a posição, mudando o turno e o jogador atual
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (IsEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode colocar seu rei em xeque!");
            }

            Peca peca = Tabuleiro.RetornaPeca(destino);

            // #jogadaespecial Promoção
            if(peca is Peao)
            {
                if((peca.Cor == Cor.Branca && destino.Linha == 0) || (peca.Cor == Cor.Preta && destino.Linha == 7))
                {
                    peca = Tabuleiro.RetiraPeca(destino);
                    _pecas.Remove(peca);
                    Peca dama = new Dama(peca.Cor, Tabuleiro);
                    Tabuleiro.ColocaPeca(dama, destino);
                    _pecas.Add(dama);
                }
            }

            if (IsEmXeque(CorAdversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (IsEmXequeMate(CorAdversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogadorAtual();
            }

            // #jogadaespecial : En Passant
            if(peca is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                VulneravelEnPassant = peca;
            }
            else
            {
                VulneravelEnPassant = null;
            }
        }

        // Muda o jogador atual de branca para preta e vice-versa
        private void MudaJogadorAtual()
        {
            if(JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        // Valida se a posição de origem de uma jogada é válida
        public void ValidaPosicaoOrigem(Posicao pos)
        {
            Peca pecaAux = Tabuleiro.RetornaPeca(pos);

            if (pecaAux == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }

            if(JogadorAtual != pecaAux.Cor)
            {
                throw new TabuleiroException("A peça de origem não é sua!");
            }

            if (!pecaAux.ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        // Valida se a posição de destino de uma jogada é válida
        public void ValidaPosicaoDestino(Posicao origem, Posicao destino)
        {
            Peca pecaOrigem = Tabuleiro.RetornaPeca(origem);

            if (!pecaOrigem.IsMovimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        // Retorna todas as peças em jogo de uma determinada cor
        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> pecasAux = new HashSet<Peca>();

            foreach (var peca in _pecas)
            {
                if (peca.Cor == cor)
                {
                    pecasAux.Add(peca);
                }
            }

            // Remove todas as peças que já foram capturas, ou seja, que não estão em jogo
            pecasAux.ExceptWith(PecasCapturadas(cor));

            return pecasAux;
        }

        // Retorna todas as peças capturadas de uma determinada cor
        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> pecasAux = new HashSet<Peca>();

            foreach(var pecaCapturada in _pecasCapturadas)
            {
                if(pecaCapturada.Cor == cor)
                {
                    pecasAux.Add(pecaCapturada);
                }
            }
            
            return pecasAux;
        }

        // Retorna a cor adversária
        private Cor CorAdversaria(Cor cor)
        {
            if(cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        // Retorna o rei de uma dada cor
        private Peca RetornaRei(Cor cor)
        {
            foreach(var peca in PecasEmJogo(cor))
            {
                if(peca is Rei)
                {
                    return peca;
                }
            }

            return null;
        }

        // Verifica se um rei está em xeque
        public bool IsEmXeque(Cor cor)
        {
            Peca rei = RetornaRei(cor);

            if(rei == null)
            {
                throw new TabuleiroException("Não há o rei dessa cor no tabuleiro!");
            }

            foreach (var peca in PecasEmJogo(CorAdversaria(cor)))
            {
                bool[,] movimentosPossiveis = peca.MovimentosPossiveis();

                if (movimentosPossiveis[rei.Posicao.Linha, rei.Posicao.Coluna])
                {
                    return true;
                }
            }

            return false;
        }

        // Verifica se um rei está em xeque mate
        public bool IsEmXequeMate(Cor cor)
        {
            if (!IsEmXeque(cor))
            {
                return false;
            }

            foreach(var peca in PecasEmJogo(cor))
            {
                bool[,] movPossiveis = peca.MovimentosPossiveis();

                for(int x = 0; x < Tabuleiro.Linhas; x++)
                {
                    for(int y = 0; y < Tabuleiro.Colunas; y++)
                    {
                        // Se o movimento na linha x, coluna y for possível
                        if (movPossiveis[x, y])
                        {
                            Posicao destino = new Posicao(x, y);
                            Posicao origem = peca.Posicao;

                            // Executa o movimento possível
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);

                            // Testa se o movimento possível resultou em um xeque
                            bool testeXeque = IsEmXeque(cor);

                            // Desfaz o movimento possível testado
                            DesfazMovimento(origem, destino, pecaCapturada);

                            // Se o movimento possível conseguiu tirar do xeque então
                            // a peça não está em xeque mate, retornando false
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            // Se nenhum movimento possível de todas as peças tirar do xeque, então é xeque mate
            return true;
        }
    }
}
