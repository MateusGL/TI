using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace teste
{
    /* versao 1.0
     * 
     * 
     * 
     */
    class Program
    {
        static void Main(string[] args)
        {
            Opcao();
        }
        public static void ChamaArquivo()
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Digite o Nome dos arquivos de teste em formato Txt.\nNao é preciso adicionar .txt");
            Console.WriteLine("Na Ordem: Dados_Alunos, Matriz, Nome_tema");

            String Nome_Alunos = Console.ReadLine();
            String Matriz = Console.ReadLine();
            String Nome_tema = Console.ReadLine();
            LerArquivo(Nome_Alunos, Matriz, Nome_tema);
        }
        public static void Opcao(/*Grafos demo*/)
        {
            int op = 0;
            Menu();
            do
            {
                Console.Write("Digite opção:");
                try { op = int.Parse(Console.ReadLine()); }
                catch { Console.Write("Opção Invalida tenta novamente"); }
                switch (op)
                {
                    case 1: LerArquivo("Dados_Aluno_Tema", "Matriz_Dissimilaridade", "Nome_Tema_Pesquisa"); break;
                    case 2:; ChamaArquivo(); break;
                    //case 3:; ImprimeMatriz(Matriz); break;

                    //case 19: MudarGrafo(demo); break;
                    case 20: Console.WriteLine("Saindo..."); break;


                    default: Console.WriteLine("Opção inválida!"); break;
                } // switch end
            } while (op != 20);
        }
        public static void Menu()
        {
            Console.WriteLine("\n----------------------");
            Console.WriteLine("1. Usar Arquivos Padrao Dados_Aluno_Tema / Matriz_Dissimilaridade / Nome_Tema_Pesquisa");
            Console.WriteLine("2. Mudar arquivos");
            //Console.WriteLine("3. Imprimir Matriz");

            Console.WriteLine("----------------------");
            Console.WriteLine("20. ----- Sair ----");
            //----
        }

        public static void ImprimeMatriz(List<List<int>> Matriz)
        {
            // Imprime Matriz
            Console.WriteLine("\nMatriz");
            for (int x = 0; x < Matriz.Count(); x++)
            {
                for (int y = 0; y < Matriz.Count(); y++)
                {
                    Console.Write(" " + Matriz[x][y]);
                }
                Console.WriteLine();
            }
        }

        public static Grafos IniciaComplementar(int Tamanho)
        {
            int txt = Tamanho;
            List<celula>[] Complementar = new List<celula>[txt]; // grafo complementar ao grafo 1                
                                                                 // ------------------------------------------------                            
            for (int x = 0; x < txt; x++)
            /*  inicializa os V elementos do vetor  */

            {
                Complementar[x] = new List<celula>();
            }
            Grafos Grafo2 = new Grafos(Tamanho, Complementar);
            return Grafo2;
        }
        public static Grafos IniciaGrafo(int Tamanho)
        {
            List<celula>[] Lista = new List<celula>[Tamanho];
            List<celula>[] Complementar = new List<celula>[Tamanho]; // grafo complementar ao grafo
            for (int x = 0; x < Tamanho; x++)
            /*  inicializa os V elementos do vetor  */
            {
                Lista[x] = new List<celula>();
            }
            // ------------------------------------------------
            Grafos demo = new Grafos(Tamanho, Lista);
            return demo;
        }

        public static void Inicia(List<aluno> Alunos, List<List<int>> Matriz, List<Tema> Temas)
        {
            Console.WriteLine("Digite o numero de professores disponiveis");

            // K precisa ser maior que o numero de alunos
            int K = int.Parse(Console.ReadLine());
            while (K > Alunos.Count())
            {
                Console.WriteLine("Numero Invalido, Digite um numero valido.");
                K = int.Parse(Console.ReadLine());
            }
            //Console.WriteLine("Digite a taxa de dissimilaridade maxima");
            //int Dissimilaridade = int.Parse(Console.ReadLine());
            //int razao = Dissimilaridade / K;
            // - - -

            Grafos demo = IniciaGrafo(Temas.Count());


            //
            // temas/profesores = media de temas por professores ou razao
            for (int x = 0; x < Matriz.Count(); x++)
                for (int y = x + 1; y < Matriz.Count(); y++) // y = x+1 para ler apenas a diagonal de cima da matriz
                {
                    demo.AdicinarAresta(x, y, Matriz[x][y]);
                    //if (Matriz[x][y] < Dissimilaridade)
                    //{
                    //    // se a dissimilaridade entre 2 temas for menos ou igual a um valor pre determinado
                    //    // adiciona uma aresta entre os temas 
                    //    demo.AdicinarAresta(x, y, Matriz[x][y]);
                    //}
                }

            Console.WriteLine("\nGrafo Original");
            demo.ImprimeGrafo_Completo();

            Grafos ArvoreMinima = new Grafos(Temas.Count(), demo.Kruskul());
            Console.WriteLine("\nArvore Minima");
            ArvoreMinima.ImprimeGrafo_Completo();

            ArvoreMinima = Corte(ArvoreMinima, K);
            // alunos
            // Cod do aluno / cod tema
            // temas
            // cod do tema / nome 

            List<List<int>> Grupos_De_Orientacao = new List<List<int>>();

            Grupos_De_Orientacao = ArvoreMinima.Get_Componentes_conexos(Grupos_De_Orientacao);
            ArvoreMinima.Imprime_Componentes_Conexos(Temas, Alunos);

            // Imprime Matriz
            ImprimeMatriz(Matriz);
            //Imprime_Grupo(Grupos_De_Orientacao, Temas, Alunos);
            Console.WriteLine("\nDigite qualquer tecla para voltar ao menu");
            Console.ReadKey();
            Console.Clear();
            Opcao();
        }
        // grupos 
        // formados pelos x...x -> temas que abrangem os alunos x...y
        public static void Imprime_Grupo(List<List<int>> Grupos_De_Orientacao, List<Tema> Temas, List<aluno> Alunos)
        {
            for (int x = 0; x < Grupos_De_Orientacao.Count(); x++)
            {
                Console.WriteLine("Grupo " + x + " Contem os Vertices:");
                for (int y = 0; y < Grupos_De_Orientacao[x].Count(); y++)
                {
                    Console.WriteLine("-Vertice " + Grupos_De_Orientacao[x][y] +
                        " Cujo Tema é:" + Temas[Grupos_De_Orientacao[x][y]].GetNome);
                    //tem os alunos x,y,z
                    //+ "Aluno que pesquisa o tema e");
                    for (int z = 0; z < Alunos.Count(); z++)
                    {
                        if (Alunos[z].GetTema == Grupos_De_Orientacao[x][y])
                            Console.WriteLine("  - Aluno " + Alunos[z].GetCod);
                    }

                }
            }

        }

        public static Grafos Corte(Grafos ArvoreMinima, int k)
        {
            List<celula> Arestas = new List<celula>();
            for (int x = 0; x < ArvoreMinima.GetVertices; x++)
                for (int y = 0; y < ArvoreMinima.Get_N_arestas(x); y++)
                {
                    Arestas.Add(ArvoreMinima.GetAresta(x, y));
                }
            List<celula> listaOrdenada = Arestas.OrderByDescending(c => c.Getpeso).ToList();
            //deletar k arestas de maior peso
            //fazer função imprime componentes
            // k-1 pois a quantidade de cortes deve ser igual a quantidade de professores menos 1 para se formar quantidades iguais de grupos
            // ex se for 2 prof serao 1 corte  
            for (int x = 0; x < (k - 1) * 2; x++) //k*2 por conta da necessidade de deletar a aresta nas 2 direções
            {
                ArvoreMinima.DeletarArestar(listaOrdenada[x].GetOrigem, listaOrdenada[x].GetDestino, listaOrdenada[x].Getpeso);
            }

            Console.WriteLine("\nArvore Minima com K: " + (k - 1) + " Cortes");
            ArvoreMinima.ImprimeGrafo_Completo();
            Console.WriteLine();
            return ArvoreMinima;
        }

        public static void LerArquivo(string Dados_Aluno, string Matriz_Dissimilaridade, string Nome_Tema_Pesquisa)
        {
            string[] dados;
            List<aluno> alunos = new List<aluno>();

            List<List<int>> Matriz = new List<List<int>>();
            List<Tema> Temas = new List<Tema>();
            if (File.Exists("Dados_Aluno_Tema.txt")) //se existe o arquivo...// Aqui se tem certeza que o arquivo existe
            {
                Stream entrada = File.Open(Dados_Aluno + ".txt", FileMode.Open); // Abre o arquivo para leitura
                StreamReader Leitor = new StreamReader(entrada); // Abre o arquivo para leitura


                while (!Leitor.EndOfStream) // Enquato nao chegar ao fim do arquivo faça...
                {
                    string Linha = Leitor.ReadLine();
                    dados = Linha.Split(';');
                    aluno aux = new aluno(int.Parse(dados[0]), int.Parse(dados[1]));
                    alunos.Add(aux);
                }
                entrada.Close();
                Leitor.Close();

            }
            else
            {
                Console.WriteLine("Arquivo nao Encontrado: Dados_Aluno_Tema");
            }
            if (File.Exists(Matriz_Dissimilaridade + ".txt")) //se existe o arquivo...// Aqui se tem certeza que o arquivo existe
            {

                Stream entrada = File.Open("Matriz_Dissimilaridade.txt", FileMode.Open); // Abre o arquivo para leitura
                StreamReader Leitor = new StreamReader(entrada); // Abre o arquivo para leitura



                while (!Leitor.EndOfStream) // Enquato nao chegar ao fim do arquivo faça...
                {
                    List<int> aux = new List<int>();
                    string Linha = Leitor.ReadLine();
                    dados = Linha.Split(';');
                    for (int x = 0; x < dados.Count(); x++)
                    {
                        aux.Add(int.Parse(dados[x]));
                    }
                    Matriz.Add(aux);
                }
                entrada.Close();
                Leitor.Close();
            }
            else
            {
                Console.WriteLine("Arquivo nao Encontrado: Matriz_Dissimilaridade.txt");
            }
            if (File.Exists(Nome_Tema_Pesquisa + ".txt")) //se existe o arquivo...// Aqui se tem certeza que o arquivo existe
            {

                Stream entrada = File.Open("Nome_Tema_Pesquisa.txt", FileMode.Open); // Abre o arquivo para leitura
                StreamReader Leitor = new StreamReader(entrada); // Abre o arquivo para leitura

                while (!Leitor.EndOfStream) // Enquato nao chegar ao fim do arquivo faça...
                {
                    string Linha = Leitor.ReadLine();
                    dados = Linha.Split(';');
                    Tema aux = new Tema(int.Parse(dados[0]), dados[1]);
                    Temas.Add(aux);
                }
                entrada.Close();
                Leitor.Close();
            }
            else
            {
                Console.WriteLine("Arquivo nao Encontrado: Nome_Tema_Pesquisa.txt");
            }
            Inicia(alunos, Matriz, Temas);
        }
    }    
    class aluno
    {
        int cod, tema;
        public aluno(int codigo, int tema)
        {
            this.cod = codigo;
            this.tema = tema;
        }
        public int GetCod
        {
            get
            {
                return this.cod;
            }
        }
        public int GetTema
        {
            get { return this.tema; }
        }
    }
    class Tema
    {
        int cod;
        string nome;
        public Tema(int codigo, string nome)
        {
            this.cod = codigo;
            this.nome = nome;
        }
        public int GetCod
        {
            get
            {
                return this.cod;
            }
        }
        public string GetNome
        {
            get { return this.nome; }
        }
    }
}
