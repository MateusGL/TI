using System;
using System.Collections.Generic;
using System.Linq;

namespace teste
{
    class Grafos
    {
        // número de vértices            
        int Vertices;
        List<celula>[] Grafo;
        //Construtor
        public Grafos(int ver, List<celula>[] Listas2)
        {
            this.Vertices = ver;
            this.Grafo = Listas2;
        }
        public int GetVertices
        {
            get
            {
                return this.Vertices;
            }
            set
            {
                this.Vertices = value;
            }
        }
        public void AdicinarAresta(int origem, int destino, int peso)
        {
            //
            celula aux = new celula(origem, destino, peso);
            Grafo[origem].Add(aux);

            //Grafo nao direcionado, função para agilizar procesos de cadastro das arestas
            celula aux2 = new celula(destino, origem, peso);
            Grafo[destino].Add(aux2);
        }

        public void DeletarArestar(int origem, int destino, int peso)
        {
            for (int x = 0; x < Grafo[origem].Count(); x++)
            {
                if (Grafo[origem][x].GetDestino == destino)
                { Grafo[origem].RemoveAt(x); }
            }
            // Forma dual deleta na ida e volta
            //for (int x = 0; x < Grafo[destino].Count(); x++)
            //{
            //    if (Grafo[destino][x].GetDestino == origem)
            //    { Grafo[destino].RemoveAt(x); }
            //}

            // Forma com Remove
            //celula aux = new celula(origem, destino, peso);
            //celula aux2 = new celula(destino, origem, peso);

            //Grafo[origem].Remove(aux);
            //            
            //Grafo[destino].Remove(aux2);
        }
        public celula GetAresta(int x, int y)
        {
            return Grafo[x][y];
        }
        public int Get_N_arestas(int x)
        {
            return Grafo[x].Count();
        }
        public void AdicinarArestaDirecionada(int origem, int destino, int peso)
        {
            //
            celula aux = new celula(origem, destino, peso);
            Grafo[origem].Add(aux);
        }

        public int GetGrau(int vertice)
        {
            // Retorna o grau de um vertice
            return Grafo[vertice].Count();
        }
        public int GetgetGrauEntrada(int vertice)
        {
            int count = 0;
            // Retorna o grau de entradade um vertice, as arestas que chegam nele                
            for (int y = 0; y < Vertices; y++)
                for (int x = 0; x < Grafo[y].Count(); x++)
                {
                    if (Grafo[y][x].GetDestino == vertice) count++;
                }
            return count;
        }

        public bool isIsolado(int v1)
        {
            // Se um vertice tiver grau 0 ele é isolado
            if (GetGrau(v1) == 0) return true;
            return false;
        }

        public bool isPendente(int v1)
        {
            // Se um vertice tiver grau 1 ele é pendente 
            if (GetGrau(v1) == 1) return true;
            return false;
        }

        public bool isRegular()
        {
            // Verifica se todos os vertices do grafo tem o mesmo grau
            for (int x = 1; x < Grafo.Count(); x++) //roda o numero de vestices existente 
            {
                // Compara o grau do vertice atual com o grau do vertice anterior se for diferente retorna falso
                if (Grafo[x].Count() != Grafo[x - 1].Count()) return false;
            }
            // Se nao encontrou vertice com grau diferente retorna true
            return true;
        }

        public bool isNulo()
        {
            // Se todo os vertices do grafos forem isolados o grafo e nulo e retorna true
            for (int x = 0; x < Grafo.Count(); x++)
            {
                // Se apenas um vertice nao for isolado o grafo nao e nulo retorna false
                if (!isIsolado(x)) return false;
            }
            return true;
        }
        public bool isAdjacente(int v1, int v2)
        {
            // Pega o tamanho na lista na posição v1 do vetor
            for (int x = 0; x < Grafo[v1].Count(); x++)
            {
                if (Grafo[v1][x].GetDestino == v2) { return true; }

            }
            return false;
        }
        public bool isCompleto()
        /*      Para cada vertice varrer a lista verificando se ele é adjacente a todos os outros vertices, 
         *      Se encontrar algum vertice ao qual ele nao seja adjacente pare a execução e retorne false   */
        {
            for (int y = 1; y < Vertices; y++)
            {
                // Verificação de laços verifica se um vertice e adjacente a ele mesmo
                if (isAdjacente(y, y)) return false;
                for (int x = 1; x < Vertices; x++)
                {
                    if (x != y) if (Arestaparalela(x, y)) return false;
                    if (x != y) if (!isAdjacente(y, x)) return false;
                    // 
                }
            }
            return true;
        }
        public bool isEuleriano()
        {
            if (!isConexo()) return false;
            for (int x = 0; x < Vertices; x++)
            {
                if ((GetGrau(x) % 2 != 0)) return false; // erro if ((GetGrau(x) % 2 == 0))
            }
            return true;
        }
        public bool isUnicursal()
        {
            int impar = 0;
            //Um grafo e unicursal se e somente se for conexo e possuir 2 vertices de grau impar
            if (!isConexo()) return false;
            for (int x = 0; x < Vertices; x++)
            {
                if (!(GetGrau(x) % 2 == 0))
                {
                    impar++;
                }
            }
            if (impar == 2) return true;
            return false;

        }

        public bool Arestaparalela(int v1, int v2)
        /* função auxiliar para a função de grafo completo
         * verifica-se para um conjunto x,y de vertices se existe mais de uma aresta adjacente
         */
        {
            int aux = 0;
            for (int x = 0; x < Grafo[v1 - 1].Count(); x++)
            {
                if (Grafo[v1 - 1][x].GetDestino == v2 - 1)
                {
                    aux++;
                }
            }
            if (aux > 1) return true; // existe aresta paralela
            return false;
        }
        public List<celula>[] Kruskul()
        {
            List<celula> Arestas = new List<celula>();
            // Arvore nova "vazia" adicionar as arestas nela
            List<celula>[] ArvoreMinima = new List<celula>[Vertices];
            List<celula>[] Aux = new List<celula>[Vertices];
            for (int x = 0; x < Vertices; x++)
            {
                //  inicializa os X elementos do vetor
                ArvoreMinima[x] = new List<celula>();
            }
            for (int x = 0; x < Vertices; x++)
            {
                //  inicializa os X elementos do vetor
                Aux[x] = new List<celula>();
            }
            Aux = Grafo;

            for (int x = 0; x < Vertices; x++)
                for (int y = 0; y < Grafo[x].Count(); y++)
                {
                    Arestas.Add(Grafo[x][y]);
                    //DeletarArestar(x, y,Grafo[x][y].Getpeso);
                }

            for (int x = 0; x < Vertices; x++)
            {
                //  inicializa os X elementos do vetor
                Grafo[x] = new List<celula>();
            }


            List<celula> listaOrdenada = Arestas.OrderBy(c => c.Getpeso).ToList();
            bool[] busca = new bool[Vertices];
            int aux = 0;

            // mais de 1 componente conexo ?
            // se resta apenas 1 componente conexo no grafo a arvore minima foi encontrada encerra o programa

            // Busca em profundidade gerando uma lista de vertices ja alcançados
            busca = ChamaBuscaProfundidade(listaOrdenada[0].GetOrigem);
            for (int x = 0; x < listaOrdenada.Count(); x++) //
            {
                // Se um vertice x nao foi encontrado a partir da busca em profundidade
                if (busca[listaOrdenada[x].GetDestino] == false)
                {
                    // add aresta
                    AdicinarAresta(listaOrdenada[x].GetOrigem, listaOrdenada[x].GetDestino, listaOrdenada[x].Getpeso);
                    //Atualiza Lista
                    busca = ChamaBuscaProfundidade(listaOrdenada[0].GetOrigem);
                }
            }

            ArvoreMinima = Grafo;
            Grafo = Aux;

            /*usar uma variavel auxiliar para segurar o grafo enquanto isso deletar todas as arestas do grafo e aplicar 
             * o algoritmo de kruskul ao finalizar atribuir o grafo resultante a arvore minima e resgatar o grafo original
             * variavel auxiliar 
			 * ou
			 * refatorar o programa e retirar a função kruskul de dentro da clase ou tornala uma classe herdeira 
			 *
            */
            return ArvoreMinima;
        }

        public List<celula>[] getComplementar()
        {
            List<celula>[] Complementar = new List<celula>[Vertices];
            for (int x = 0; x < Vertices; x++)
            {
                //  inicializa os X elementos do vetor
                Complementar[x] = new List<celula>();
            }

            for (int x = 0; x < Vertices; x++)
                for (int y = 0; y < Vertices; y++)
                {
                    /* Caso nao exista adj entre um vertice x,y e x,y nao seja igual entao adiciona a aresta
                     * ao grafo complementar.
                     */
                    if (x != y && !isAdjacente(x, y))
                    {// add vertice
                        celula aux = new celula(x, y, 1);
                        Complementar[x].Add(aux);
                    }
                }
            return Complementar;
        }

        public void ImprimeGrafo_Completo(/*List<celula>[] Grafo*/)
        {
            for (int y = 0; y < Vertices; y++)
                for (int x = 0; x < Grafo[y].Count(); x++)
                {
                    Console.WriteLine("vertice: " + (y + 1) + " Destino: " + ((Grafo[y][x].GetDestino) + 1)
                        + " Peso : " + Grafo[y][x].Getpeso);
                }

        }

        public struct AuxBuscaP
        {
            int cor; // 1 branco 2 azul 3 vermelhor 
            int pred, tempo;
            public AuxBuscaP(int cor1, int pred1, int tempo1)
            {
                cor = cor1;
                pred = pred1;
                tempo = tempo1;
            }
            public int GetCor
            {
                get
                {
                    return this.cor;
                }
                set
                {
                    cor = value;
                }
            }
        }
        // -------------------------------------
        /* Vetor auxiliar guarda a cor do vertice (se ja foi descoberto, esta em atividade ou desativo)
         * -- retornar um vetor de booleanos pode ser util para simplificar a estrutura da solução
         * a função recebe um vertice qualquer X coloca ele em azul e marca todos os vertices adjacentes a ele tb com azul 
         * e recebe este processo em ordem crescente a partir do vertice recebido ao final retornar um vetor booleano com o tamanho do grafo e true 
         * para cada vertice que foi alcançado e false para os vertices que nao foram alcançados*/
        public bool[] ChamaBuscaProfundidade(int v1)
        {
            AuxBuscaP[] teste = new AuxBuscaP[Vertices];
            for (int x = 0; x < Vertices; x++)
            {
                teste[x] = new AuxBuscaP(1, 0, 0);
            }
            // -----------------------------
            bool[] tabela = new bool[Vertices];
            BuscaProfundidade(v1, ref teste, ref tabela);

            //for (int x = 0; x < Vertices; x++)
            //{
            //    // imprime a relação dos vertices que foram alcançados pela busca em profundidade 
            //    Console.WriteLine("Vertice alcançado: " + x + " " + tabela[x]);
            //}
            return tabela;
        }
        public bool[] BuscaProfundidade(int v1, ref AuxBuscaP[] teste, ref bool[] tabela)
        {
            teste[v1].GetCor = 2;
            tabela[v1] = true;
            for (int x = 0; x < Grafo[v1].Count; x++)
            {
                //if (teste[Lista[v1][x].GetDestino == v0) tem ciclo variavel auxiliar recebe true
                if (teste[Grafo[v1][x].GetDestino].GetCor == 1) BuscaProfundidade(Grafo[v1][x].GetDestino, ref teste, ref tabela);
            }
            return tabela;
        }
        public bool ChamaCiclo(int v1)
        {
            bool[] Visitado = new bool[Vertices];
            bool[] tabela = new bool[Vertices];
            bool aux = Ciclo(v1, ref Visitado, ref tabela, v1);
            return aux;
        }
        public bool Ciclo(int v1, ref bool[] visitado, ref bool[] tabela, int v0)
        {
            visitado[v1] = true;
            tabela[v1] = true;
            if (isAdjacente(v1, v0)) return true;
            for (int x = 0; x < Grafo[v1].Count; x++)
            {
                if (visitado[x] == false) return Ciclo(Grafo[v1][x].GetDestino, ref visitado, ref tabela, v0);
            }
            return false;
        }
        public bool isConexo()
        {
            bool[] tabela = ChamaBuscaProfundidade(0);
            for (int x = 0; x < Vertices; x++)
            {
                if (tabela[x] == false) return false;
            }
            return true;
        }

        // prin recebe um vertice verificar se o proximo vertice nao faz parte do grafo com a busca em profundidade
        // e se nao fizer o adiciona de, usa logica gulosa pra ver qual vertice deve ser add
        public int ComponentesConexos()
        {
            int y = 0;
            bool aux = false;
            int NumeroComponentes = 0;
            bool[] TabelaAuxiliar = new bool[Vertices];
            bool[] componentes = ChamaBuscaProfundidade(0);
            while (aux == false)
            {
                // Sempre que o algoritmo fizer uma nova busca significa que existe mais um componente
                NumeroComponentes++;
                /* Verifica a tabela procurando um vertice que nao tenha sido alcançado na busca em profundidade
                 * caso encontre faz uma busca a partir deste vertice
                 */
                for (int x = 0; x < Vertices; x++)
                {
                    if (TabelaAuxiliar[x] == false)
                    {
                        componentes = ChamaBuscaProfundidade(x);
                    }
                }
                /* Coloca na tabela auxiliar os vertices que foram encontrados 
                 */
                for (int x = 0; x < Vertices; x++)
                {
                    if (componentes[x] == true) TabelaAuxiliar[x] = componentes[x];
                }
                /* Se todos os vertices tiverem sido entrados termina a busca e retorna o numero de componentes 
                 * conexos
                 */
                int encontrados = 0;
                if (TabelaAuxiliar[0] == true)
                {
                    for (y = 1; y < Vertices; y++)
                    {
                        if (TabelaAuxiliar[y] == TabelaAuxiliar[y - 1]) encontrados++;
                    }
                }
                if (encontrados == Vertices - 1) aux = true;
            }
            return NumeroComponentes;
        }

        public void Imprime_Componentes_Conexos(List<Tema> Temas, List<aluno> Alunos)
        {
            int y = 0, cont = 0;
            bool aux = false;
            int NumeroComponentes = 0, encontrados = 0;
            bool[] TabelaAuxiliar = new bool[Vertices];
            bool[] componentes = new bool[Vertices];
            while (encontrados != Vertices)
            {
                /* Verifica a tabela procurando um vertice que nao tenha sido alcançado na busca em profundidade
                 * caso encontre faz uma busca a partir deste vertice
                 */
                for (int x = 0; x < Vertices; x++)
                {
                    if (TabelaAuxiliar[x] == false)
                    {
                        componentes = ChamaBuscaProfundidade(x);
                    }
                }
                NumeroComponentes++;
                Console.WriteLine("\n " + (NumeroComponentes) + " Grupo de orientação");
                for (int x = 0; x < Vertices; x++)
                {
                    cont = 0;
                    if (componentes[x] == true)
                    {
                        Console.WriteLine(" -----\n Vertice: " + (x + 1) + " Tema:" + Temas[x].GetNome + " cod: " + Temas[x].GetCod);
                        for (int z = 0; z < Alunos.Count(); z++)//erro
                        {
                            if (Temas[x].GetCod == Alunos[z].GetTema)
                                Console.WriteLine("\t- Aluno: " + Alunos[z].GetCod + " tema: " + Alunos[z].GetTema);
                            else
                                cont++;
                        }
                        if (cont == Alunos.Count()) Console.WriteLine("\t---N/A---");
                    }
                }
                /* Coloca na tabela auxiliar os vertices que foram encontrados 
                     */
                for (int x = 0; x < Vertices; x++)
                {
                    if (componentes[x] == true) TabelaAuxiliar[x] = componentes[x];
                }
                // verifica se todos os vertices ja foram encontrados
                encontrados = 0;
                for (y = 0; y < Vertices; y++)
                {
                    if (TabelaAuxiliar[y] == true) encontrados++;
                }
            }
        }

        public List<List<int>> Get_Componentes_conexos(List<List<int>> Grupos_De_Orientacao)
        {
            int y = 0;
            bool aux = false;
            int NumeroComponentes = 0, encontrados = 0;
            bool[] TabelaAuxiliar = new bool[Vertices];
            bool[] componentes = new bool[Vertices];
            while (encontrados != Vertices)
            {
                /* Verifica a tabela procurando um vertice que nao tenha sido alcançado na busca em profundidade
                 * caso encontre faz uma busca a partir deste vertice
                 */
                for (int x = 0; x < Vertices; x++)
                {
                    if (TabelaAuxiliar[x] == false)
                    {
                        componentes = ChamaBuscaProfundidade(x);
                    }
                }

                List<int> aux2 = new List<int>();

                for (int x = 0; x < Vertices; x++)
                {
                    // Se o Vertice x faz parte do componente conexo add ele a lista
                    if (componentes[x] == true)
                        aux2.Add(x);
                }
                Grupos_De_Orientacao.Add(aux2);
                NumeroComponentes++;


                /* Coloca na tabela auxiliar os vertices que foram encontrados 
                     */
                for (int x = 0; x < Vertices; x++)
                {
                    if (componentes[x] == true) TabelaAuxiliar[x] = componentes[x];
                }
                // verifica se todos os vertices ja foram encontrados
                encontrados = 0;
                for (y = 0; y < Vertices; y++)
                {
                    if (TabelaAuxiliar[y] == true) encontrados++;
                }
            }
            return Grupos_De_Orientacao;
        }


        public int getCutVertices()
        {
            /*
             * Encontrar o numero de componenestes conexos em G = A
             * Para todo i pertencente a G testar
             * testar quantos componentes conexos G tem sem i = B 
             * Se B > A 
             * i é um componentes conexo
             * Usar buscar em profundida para encontrar numero de componenetes
             * //
             * 
             * com a tabela da busca em profundidade fazer uma busca em profundidade em cada vertice nao encontrado na 
             * primeira busca o numero de componentes conexos sera o numero de vezes q sera necessario fazer a busca
             * em profundidade para encontrar todos os vertices.
             * 
             */
            int A, B;
            A = ComponentesConexos();
            List<celula>[] ListaAuxiliar = new List<celula>[Vertices - 1];
            for (int x = 0; x < Vertices - 1; x++)
            /*  inicializa os V elementos do vetor  */
            {
                ListaAuxiliar[x] = new List<celula>();
            }
            /* Função auxiliar para deletar vertice 
             * Criar um Grafo sem o vertice X deletando qualquer aresta ligada a ele 
             * comparar o numero de componentes conexos deste novo grafo com o numero atual
             * se for menor a vertice removida dele e um cut-vertice
             */
            for (int x = 0; x < Vertices; x++)
            {
                // criasse o grafo sem o vertice X 
                for (int y = 0; y < Vertices; y++)
                {
                    if (x != y)
                    {
                        //if (Lista[x][y].GetDestino != x) ListaAuxiliar[y]
                    }

                }
            }
            return 0;
        }
    }
}
