using backPadaria.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Service
{
    public static class ClienteService
    {

        public static void MenuCliente()
        {
            bool sair = false;

            while (!sair)
            {
                Console.Clear();

                Console.WriteLine("===== GESTÃO DE CLIENTES =====");

                Console.WriteLine("1 - Cadastrar Cliente");
                Console.WriteLine("2 - Listar Todos os Clientes");
                Console.WriteLine("3 - Buscar Cliente por ID");
                Console.WriteLine("4 - Atualizar Cliente");
                Console.WriteLine("5 - Excluir Cliente");
                Console.WriteLine("6 - Visualizar pontuação");
                Console.WriteLine("0 - Sair do Programa");

                Console.WriteLine("======================");
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
                            listarPorIdClienteMenu();
                            break;
                        case "4":
                            atualizarClienteMenu();
                            break;
                        case "5":
                            excluirClienteMenu();
                            break;
                        case "6":
                            ExibirPotuacaoClienteMenu();
                            break;
                        case "0":
                            sair = true;
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


            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
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
        } //

        private static void listarPorIdClienteMenu()
        {
            Console.WriteLine("------ BUSCAR POR ID ----- ");
            Console.WriteLine("Digite o id do cliente: ");

            int idConsole;

            int.TryParse(Console.ReadLine(), out idConsole);

            Cliente? clienteBusca = Cliente.listarPorId(idConsole);

            if (clienteBusca == null)
            {

                Console.WriteLine("Cliente não encontrado");

            }
            else
            {

                Console.WriteLine($"ID: {clienteBusca.Id} " +
                    $"\nNome {clienteBusca.Nome} " +
                    $"\nCPF: {clienteBusca.Cpf} " +
                    $"\nTelefone: {clienteBusca.Telefone} " +
                    $"\nEndereco: {clienteBusca.Endereco}" +
                    $"\nPontos: {clienteBusca.Pontos} ");



            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();

        } //

        private static void atualizarClienteMenu()
        {
            Console.WriteLine("------ ATUALIZAR CLIENTE POR ID ----- ");
            Console.WriteLine("Digite o id do cliente que deseja atualizar");

            int idBusca = 0;
            int.TryParse(Console.ReadLine(), out idBusca);

            Console.WriteLine($"----------> {idBusca}");

            Cliente? clienteParaAtualizar = Cliente.listarPorId(idBusca);

            if (clienteParaAtualizar != null)
            {

                Console.WriteLine($"Atualizando dados de {clienteParaAtualizar.Nome}");

                Console.WriteLine("Pressione ENTER sem digitar nada para manter o dado atual ");

                Console.WriteLine($"Novo Endereço: Atual ({clienteParaAtualizar.Endereco})");

                string novoEndereco = Console.ReadLine();
                if (!string.IsNullOrEmpty(novoEndereco)) clienteParaAtualizar.Endereco = novoEndereco;


                Console.WriteLine($"Novo Telefone: Atual ({clienteParaAtualizar.Telefone})");
                string? novoTelefone = Console.ReadLine();
                if (!string.IsNullOrEmpty(novoEndereco)) clienteParaAtualizar.Telefone = novoTelefone;


                int resultadoAtualizacao = Cliente.Atualizar(clienteParaAtualizar);
                if (resultadoAtualizacao > 0) Console.WriteLine("Cliente atualizado com sucesso!");

            }
            else
            {
                Console.WriteLine("Cliente não encontrado");
            }


            Console.ReadKey();
        }//

        private static void excluirClienteMenu()
        {
            Console.WriteLine("------ EXCLUIR CLIENTE POR ID ----- ");
            Console.WriteLine("Digite o id do cliente que deseja excluir");

            int idBusca = 0;
            int.TryParse(Console.ReadLine(), out idBusca);



            Cliente? clienteParaExcluir = Cliente.listarPorId(idBusca);

            if (clienteParaExcluir != null)
            {
                Cliente.Deletar(clienteParaExcluir);
                Console.WriteLine("Cliente excluído com sucesso");
            }
            else
            {

                Console.WriteLine("Cliente não encontrado");

            }

            Console.ReadKey();

        } //

        private static void ExibirPotuacaoClienteMenu()
        {
            Console.WriteLine("------ EXIBIR PONTUAÇÃO ----- ");
            Console.WriteLine("Digite o id do cliente: ");

            int idConsole;

            int.TryParse(Console.ReadLine(), out idConsole);

            Cliente? clienteBusca = Cliente.listarPorId(idConsole);

            if (clienteBusca == null)
            {

                Console.WriteLine("Cliente não encontrado");

            }
            else
            {

                Console.WriteLine($"ID: {clienteBusca.Id} " +
                    $"\nNome {clienteBusca.Nome} " +
                    $"\nCPF: {clienteBusca.Cpf} " +
                    $"\nPontos: {clienteBusca.Pontos} ");


            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();




        }//


    }
}