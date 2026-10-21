using backPadaria.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Services
{
    internal class ClienteService
    {
        private static int resultadoAtulizar;


        public static void menuCliente()
        {
            bool sair = false;
            while (!sair)
            {

                Console.WriteLine("===== GESTÂO DE CLIENTES =====");
                Console.WriteLine("1 - Cadastrar Cliente");
                Console.WriteLine("2 - Listar Todos os Clientes");
                Console.WriteLine("3 - Buscar Clinte por ID");
                Console.WriteLine("4 - Atualizar Cliente");
                Console.WriteLine("5 - Excluir Cliente");
                Console.WriteLine("6 - Visualizar pontuação");
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

                            cadastrarClienteMenu();

                            break;

                        case "2": // Listar
                            listarClientesMenu();
                            break;

                        case "3":
                            BuscarClienteMenu();
                            break;

                        case "4": // Atualizar
                            AtualizarClienteMenu();
                            break;

                        case "5": // Deletar cliente
                            DeletarClienteMenu();
                            break;

                        case "6": // Visualizar Pontuação
                            VisualizarPontuacaoMenu();
                            break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[ERRO DE VALIDAÇÃO]: {ex.Message}");
                }
            }

        } // FIM MENU

        private static void cadastrarClienteMenu()
        {
            Console.WriteLine("------ CADASTRAR CLIENTE ----- ");

            Console.WriteLine("Digite o nome do cliente: ");
            string nomeCliente = Console.ReadLine();

            Console.WriteLine("Digite o endereço do cliente: ");
            string enderecoCliente = Console.ReadLine();

            Console.WriteLine("Digite o cpf do cliente: ");
            string cpfCliente = Console.ReadLine();

            Console.WriteLine("Digite o telefone do cliente: ");
            string telefoneCliente = Console.ReadLine();

            Console.WriteLine("Digite a quantidade de pontos");
            int pontosCliente = int.Parse(Console.ReadLine());


            Cliente NovoCliente = new Cliente(
                nome: nomeCliente,
                cpf: cpfCliente,
                telefone: telefoneCliente,
                endereco: enderecoCliente,
                pontos: pontosCliente
            );

            int resultadoInsert = Cliente.Inserir(NovoCliente);

            Console.Clear();

            if (resultadoInsert > 0) Console.WriteLine("Cliente cadastrado com sucesso!!");

        }

        private static void listarClientesMenu()
        {
            Console.WriteLine("------ LISTA DE CLIENTES ----- ");

            List<Cliente> lista = Cliente.ListarTodos();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum cliente cadastrado no sistema");

            }
            else
            {
                foreach (var cliente in lista)
                {
                    Console.WriteLine($"ID: {cliente.Id} | Nome {cliente.Nome} | CPF: {cliente.Cpf}| Pontos: {cliente.Pontos} ");
                }
            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();

        }

        private static void BuscarClienteMenu()
        {
            Console.WriteLine("====== Buscar cliente por ID ======");

            Console.WriteLine("Digite o Id que deseja buscar");
            int idCliente = int.Parse(Console.ReadLine());

            Cliente? cli1 = Cliente.ListarPorID(idCliente);

            Console.WriteLine($"ID: {cli1.Id} | Nome {cli1.Nome} | CPF: {cli1.Cpf}| Pontos: {cli1.Pontos} ");

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        private static void AtualizarClienteMenu()
        {
            Console.WriteLine("====== Atualizar cliente ======");

            Console.WriteLine("Digite o Id do cliente:");

            if (!int.TryParse(Console.ReadLine(), out int idCliente))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            Console.WriteLine("Digite o novo nome do cliente:");
            string nomeCliente = Console.ReadLine();

            Console.WriteLine("Digite o novo endereço do cliente:");
            string enderecoCliente = Console.ReadLine();

            Console.WriteLine("Digite o novo CPF do cliente:");
            string cpfCliente = Console.ReadLine();

            Console.WriteLine("Digite o novo telefone do cliente:");
            string telefoneCliente = Console.ReadLine();

            Console.WriteLine("Digite a nova quantidade de pontos:");
            int pontosCliente = int.Parse(Console.ReadLine());

            Cliente clienteAtualizado = new Cliente(
                id: idCliente,
                nome: nomeCliente,
                cpf: cpfCliente,
                telefone: telefoneCliente,
                endereco: enderecoCliente,
                pontos: pontosCliente
            );

            int resultadoUpdate = Cliente.Atualizar(clienteAtualizado);

            if (resultadoUpdate > 0)
            {
                Console.WriteLine("Cliente atualizado com sucesso!");
            }
            else
            {
                Console.WriteLine("Não foi possível atualizar o cliente.");
            }
            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();

        }



        private static void DeletarClienteMenu()
        {
            Console.WriteLine("====== DELETAR CLIENTE ======");

            Console.WriteLine("Digite o ID do cliente:");

            if (!int.TryParse(Console.ReadLine(), out int idCliente))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            Cliente? cliente = Cliente.ListarPorID(idCliente);

            if (cliente == null)
            {
                Console.WriteLine("Cliente não encontrado.");
                return;
            }


            int resultadoDelete = Cliente.Deletar(cliente);

            if (resultadoDelete > 0)
            {
                Console.WriteLine("Cliente excluído com sucesso!");
            }
            else
            {
                Console.WriteLine("Não foi possível excluir o cliente.");
            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }



        private static void VisualizarPontuacaoMenu()
        {
            Console.WriteLine("====== PONTUAÇÃO DO CLIENTE ======");

            Console.WriteLine("Digite o ID do cliente:");

            if (!int.TryParse(Console.ReadLine(), out int idCliente))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            Cliente? cliente = Cliente.ListarPorID(idCliente);

            if (cliente == null)
            {
                Console.WriteLine("Cliente não encontrado.");

            }
            else
            {
                Console.WriteLine($"ID: {cliente.Id}");
                Console.WriteLine($"Nome: {cliente.Nome}");
                Console.WriteLine($"Pontuação atual: {cliente.Pontos}");
            }
            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}