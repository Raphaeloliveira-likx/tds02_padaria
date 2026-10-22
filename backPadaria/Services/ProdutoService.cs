using backPadaria.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Services
{
    internal class ProdutoService
    {
        private static int resultadoAtulizar;


        public static void menuProduto()
        {
            bool sair = false;
            while (!sair)
            {

                Console.WriteLine("===== GESTÂO DE Produtos =====");
                Console.WriteLine("1 - Cadastrar Produto");
                Console.WriteLine("2 - Listar Todos os Produtos");
                Console.WriteLine("3 - Buscar Produto por ID");
                Console.WriteLine("4 - Atualizar Produto");
                Console.WriteLine("5 - Excluir Produto");             
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

                            CadastrarProdutoMenu();

                            break;

                        case "2": // Listar
                            ListarProdutoMenu();
                            break;

                        case "3":
                            BuscarProdutoMenu();
                            break;

                        case "4": // Atualizar
                            AtualizarProdutoMenu();
                            break;

                        case "5": // Deletar cliente
                            DeletarProdutoMenu();
                            break;

                        
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[ERRO DE VALIDAÇÃO]: {ex.Message}");
                }
            }

        } // FIM MENU

        

            private static void CadastrarProdutoMenu()
        {
            Console.WriteLine("------ CADASTRAR PRODUTO -----");

            Console.WriteLine("Digite o nome do produto:");
            string nomeProduto = Console.ReadLine();

            Console.WriteLine("Digite o preço do produto:");
            decimal precoProduto = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Digite a categoria do produto:");
            string categoriaProduto = Console.ReadLine();

            Console.WriteLine("O produto é fabricado? (Sim/Não):");
            string resposta = Console.ReadLine();

            bool fabricadoProduto;

            if (resposta.ToLower() == "sim")
            {
                fabricadoProduto = true;
                Console.WriteLine("Produto fabricado.");
            }
            else if (resposta.ToLower() == "não" || resposta.ToLower() == "nao")
            {
                fabricadoProduto = false;
                Console.WriteLine("Produto não fabricado.");
            }
            else
            {
                Console.WriteLine("Resposta inválida. Digite Sim ou Não.");
                return;
            }


        Console.WriteLine("Digite a nova marca do produto");
            string marcaProduto = Console.ReadLine();


            Produto NovoProduto = new Produto(
                nome: nomeProduto,
                preco: precoProduto,
                categoria: categoriaProduto,
                fabricado: fabricadoProduto,
                marca: marcaProduto
            );

            int resultadoInsert = Produto.Inserir(NovoProduto);

            Console.Clear();

            if (resultadoInsert > 0) Console.WriteLine("Produto cadastrado com sucesso!!");

        }

        private static void ListarProdutoMenu()
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

        private static void BuscarProdutoMenu()
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

        private static void AtualizarProdutoMenu()
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



        private static void DeletarProdutoMenu()
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
    }
}
    

