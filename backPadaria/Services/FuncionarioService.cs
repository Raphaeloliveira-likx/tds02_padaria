using backPadaria.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Services
{
    internal class FuncionarioService
    {
        public static void menuFuncionario()
        {
            bool sair = false;
            while (!sair)
            {

                Console.WriteLine("===== GESTÂO DE FUNCIONARIO =====");
                Console.WriteLine("1 - Cadastrar Funcionario");
                Console.WriteLine("2 - Listar Todos os Funcionarios");
                Console.WriteLine("3 - Buscar Funcionario por ID");
                Console.WriteLine("4 - Atualizar Funcionario");
                Console.WriteLine("5 - Excluir Funcionario");
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

                            CadastrarFuncionarioMenu();

                            break;

                        case "2": // Listar
                            listarFuncionarioMenu();
                            break;

                        case "3":
                            BuscarFuncionarioMenu();
                            break;

                        case "4": // Atualizar
                            AtualizarFuncionarioMenu();
                            break;

                        case "5": // Deletar cliente
                            DeletarFuncionarioMenu();
                            break;


                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[ERRO DE VALIDAÇÃO]: {ex.Message}");
                }
            }
        }


            private static void CadastrarFuncionarioMenu()
        {
            Console.WriteLine("====== CADASTRAR FUNCIONARIO ====== ");

            Console.WriteLine("Digite o nome do Funcionario: ");
            string nomeFuncionario = Console.ReadLine();
                        
            Console.WriteLine("Digite o cpf do Funcionario: ");
            string cpfFuncionario = Console.ReadLine();

            Console.WriteLine("Digite o Cargo do funcionario: ");
            string cargoFuncionario = Console.ReadLine();
                      

            Funcionario NovoFuncionario = new Funcionario(
                nome: nomeFuncionario,
                cpf: cpfFuncionario,
                cargo: cargoFuncionario                
            );

            int resultadoInsert = Funcionario.Inserir(NovoFuncionario);

            Console.Clear();

            if (resultadoInsert > 0) Console.WriteLine("Funcionario cadastrado com sucesso!!");

        }

        private static void listarFuncionarioMenu()
        {
            Console.WriteLine("====== LISTA DE FUNCIONARIO ====== ");

            List<Funcionario> lista = Funcionario.ListarTodos();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum fucionario cadastrado no sistema");

            }
            else
            {
                foreach (var funcionario in lista)
                {
                    Console.WriteLine($"ID: {funcionario.Id} | Nome {funcionario.Nome} | CPF: {funcionario.Cpf}| cargo: {funcionario.Cargo} ");
                }
            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();

        }

        private static void BuscarFuncionarioMenu()
        {
            Console.WriteLine("====== Buscar funcionario por ID ======");

            Console.WriteLine("Digite o Id que deseja buscar");
            int idFuncionario = int.Parse(Console.ReadLine());

            Funcionario? funcionario = Funcionario.ListarPorID(idFuncionario);

            Console.WriteLine($"ID: {funcionario.Id} | Nome {funcionario.Nome} | CPF: {funcionario.Cpf}| Cargo: {funcionario.Cargo} ");

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }


        private static void AtualizarFuncionarioMenu()
        {
            Console.WriteLine("====== ATUALIZAR FUNCIONARIO ======");

            Console.WriteLine("Digite o Id do funcionario:");

            if (!int.TryParse(Console.ReadLine(), out int idFuncionario))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            Console.WriteLine("Digite o novo nome do funcionario:");
            string nomeFuncionario = Console.ReadLine();
                        
            Console.WriteLine("Digite o novo CPF do funcionario:");
            string cpfFuncionario = Console.ReadLine();

            Console.WriteLine("Digite o novo cargo do funcinario:");
            string cargoFuncionario = Console.ReadLine();
                      

            Funcionario funcionarioAtualizado = new Funcionario(
                id: idFuncionario,
                nome: nomeFuncionario,
                cpf: cpfFuncionario,
                cargo: cargoFuncionario               
            );

            int resultadoUpdate = Funcionario.Atualizar(funcionarioAtualizado);

            if (resultadoUpdate > 0)
            {
                Console.WriteLine("Funcionario atualizado com sucesso!");
            }
            else
            {
                Console.WriteLine("Não foi possível atualizar o funcionario.");
            }
            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();

        }

        private static void DeletarFuncionarioMenu()
        {
            Console.WriteLine("====== DELETAR FUNCIONARIO ======");

            Console.WriteLine("Digite o ID do funcionario:");

            if (!int.TryParse(Console.ReadLine(), out int idFuncionario))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            Funcionario? funcionario = Funcionario.ListarPorID(idFuncionario);

            Console.WriteLine(funcionario.Nome);

            if (funcionario == null)
            {
                Console.WriteLine("Funcionario não encontrado.");
                return;
            }


            int resultadoDelete = Funcionario.Deletar(funcionario);

            if (resultadoDelete > 0)
            {
                Console.WriteLine("Funcionario excluído com sucesso!");
            }
            else
            {
                Console.WriteLine("Não foi possível excluir o funcionario.");
            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
    }
 }

