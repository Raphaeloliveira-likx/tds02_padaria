using backPadaria.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Services
{
    internal class MarcaService
    {
        private static int resultadoAtulizar;


        public static void menuMarca()
        {
            bool sair = false;
            while (!sair)
            {

                Console.WriteLine("===== GESTÂO DE Marca =====");
                Console.WriteLine("1 - Cadastrar Marca");
                Console.WriteLine("2 - Listar Todas as Marcas");
                Console.WriteLine("3 - Buscar Marca por ID");
                Console.WriteLine("4 - Atualizar Marca");
                Console.WriteLine("5 - Excluir Marca");               
                Console.WriteLine("0 - Sair do Programa");
                Console.WriteLine("===========================");
                Console.WriteLine("Digite uma opção: ");


                string opcao = Console.ReadLine();
                Console.Clear();

                try
                {
                    switch (opcao)
                    {
                        case "1": // Cadastrar

                            cadastrarMarcaMenu();

                            break;

                        case "2": // Listar
                            listarMarcaMenu();
                            break;

                        case "3": // Buscar
                            BuscarMarcaMenu();
                            break;

                        case "4": // Atualizar
                            AtualizarMarcaMenu();
                            break;

                        case "5": // Deletar cliente
                            DeletarMarcaMenu();
                            break;

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[ERRO DE VALIDAÇÃO]: {ex.Message}");
                }
            }

        } // FIM MENU

        private static void cadastrarMarcaMenu()
        {
            Console.WriteLine("------ CADASTRAR MARCA ----- ");

            Console.WriteLine("Digite o nome da Marca: ");
            string nomeMarca = Console.ReadLine();               
                        
            Console.WriteLine("Digite o telefone da Marca: ");
            string telefoneMarca = Console.ReadLine();



            Marca NovaMarca = new Marca(
                              
                nome: nomeMarca,
                telefone: telefoneMarca
            );

            int resultadoInsert = Marca.Inserir(NovaMarca);

            Console.Clear();

            if (resultadoInsert > 0) Console.WriteLine("Marca cadastrada com sucesso!!");

        }

        private static void listarMarcaMenu()
        {
            Console.WriteLine("------ LISTA DE MARCAS ----- ");

            List<Marca> lista = Marca.ListarTodos();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma marca cadastrada no sistema");

            }
            else
            {
                foreach (var marca in lista)
                {
                    Console.WriteLine($"ID: {marca.Id} | Nome {marca.Nome} | Telefone: {marca.Telefone} ");
                }
            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();

        }

        private static void BuscarMarcaMenu()
        {
            Console.WriteLine("====== Buscar Marca por ID ======");

            Console.WriteLine("Digite o Id que deseja buscar");
            int idMarca = int.Parse(Console.ReadLine());

            Marca? marca = Marca.ListarPorID(idMarca);

            Console.WriteLine($"ID: {marca.Id} | Nome {marca.Nome} | Telefone {marca.Telefone} ");

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        private static void AtualizarMarcaMenu()
        {
            Console.WriteLine("====== Atualizar Marca ======");

            Console.WriteLine("Digite o Id da Marca:");

            if (!int.TryParse(Console.ReadLine(), out int idMarca))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            Console.WriteLine("Digite o novo nome da Marca:");
            string nomeMarca = Console.ReadLine();
                        
            Console.WriteLine("Digite o novo telefone da Marca:");
            string telefoneMarca = Console.ReadLine();

           
            Marca marcaAtualizado = new Marca(
                id: idMarca,
                nome: nomeMarca,
                telefone: telefoneMarca
                
            );

            int resultadoUpdate = Marca.Atualizar(marcaAtualizado);

            if (resultadoUpdate > 0)
            {
                Console.WriteLine("Marca atualizada com sucesso!");
            }
            else
            {
                Console.WriteLine("Não foi possível atualizar a marca.");
            }
            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();

        }



        private static void DeletarMarcaMenu()
        {
            Console.WriteLine("====== DELETAR MARCA ======");

            Console.WriteLine("Digite o ID da Marca:");

            if (!int.TryParse(Console.ReadLine(), out int idMarca))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            Marca? marca = Marca.ListarPorID(idMarca);

            if (marca == null)
            {
                Console.WriteLine("Marca não encontrada.");
                return;
            }


            int resultadoDelete = Marca.Deletar(marca);

            if (resultadoDelete > 0)
            {
                Console.WriteLine("Marca excluída com sucesso!");
            }
            else
            {
                Console.WriteLine("Não foi possível excluir a marca.");
            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }                     
      }
    }

