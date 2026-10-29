using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backPadaria.Model;

namespace backPadaria.Services
{
    internal class VendaService
    {
        public static void MenuVendas()
        {
            bool sair = false;
            while (!sair)
            {
                Console.Clear();
                Console.WriteLine("====== PDV - Padaria Pão & Cia");
                Console.WriteLine("1 - Iniciar nova venda");
                Console.WriteLine("0 - Voltar ao menu principal");
                Console.WriteLine("==============================");
                Console.Write("Digite uma opção: ");

                string opcao = Console.ReadLine();
                Console.Clear();

                try
                {
                    switch (opcao)
                    {
                        case "1":
                            iniciarNovaVenda();
                            break;
                        case "0":
                            sair = true;
                            break;
                        default:
                            Console.WriteLine("Opção Invalida");
                            break;
                    }
                }catch (Exception ex)
                {
                    Console.WriteLine($"\nErro na venda {ex.Message} ");
                    Console.WriteLine("\nPressione uma tecla para continuar ...");
                    Console.ReadKey();
                }
            }
        }
    public static void iniciarNovaVenda()
        {
            Console.WriteLine("------ Nova Venda ------");

            // 1 -  Identificação do Funcionario

            int funcionarioID = 2;

            // 2 - Identificação do cliente 

            Console.WriteLine("ID do cliente (Pressione Enter se nao quiser se identificar)");
            int idCliente = 0;
            
            int.TryParse(Console.ReadLine(), out idCliente);

            Cliente clienteVenda = Cliente.listarPorId(idCliente);

            // -- Inicializando o objeto Compra 

            Compra compraAtiva = new Compra(DateTime.Now, funcionarioID, clienteVenda.Id);

            // Loop do carrinho de compras

            bool adicionarMais = true;

            while (adicionarMais)
            {
                Console.WriteLine("\n--- Adicionar Produto ---");
                Console.WriteLine("Id do produto ou '0' para finalizar/pagar ");

                int.TryParse(Console.ReadLine(), out int idProduto);

                if(idProduto == 0)
                {
                    adicionarMais = false;
                    continue; 
                }

                Produto produto = Produto.listarPorId(idProduto);

                if (produto == null)
                {
                    Console.WriteLine("Produto não encontrado.... Tente novamente");
                    continue;
                }

                Console.Write($"Quantidade de {produto.Nome} | Preço {produto.Preco:c}");
                int.TryParse(Console.ReadLine(), out int qtdProduto);

                if (qtdProduto >= 0)
                {

                    compraAtiva.AdicionarItem(produto, qtdProduto);
                    Console.WriteLine($"Total: {compraAtiva.CalcularTotal()}");
                                
                }
            }
            // Encerra o laço de Produtos
            // 3 - Checkout
            if (compraAtiva.Itens.Count == 0)
            {
                Console.WriteLine("\nVenda cancelada (Nenhum item foi adicionado).");
            }
            else
            {
                Console.WriteLine("\n ------ RESUMO DA VENDA ------");
                foreach (var item in compraAtiva.Itens)
                {
                    Console.WriteLine($"ID: {item.ProdutoId}" + 
                        $"| QTD: {item.Quantidade} " + 
                        $"| Preço Unit: {item.PrecoCobrado} " + 
                        $"| Subtotal: {item.CalcularSubtotal():c}");
                }

                decimal totalFinal = compraAtiva.CalcularTotal();

                Console.WriteLine("-----------------------------------");
                Console.WriteLine($"TOTAL A PAGAR: {totalFinal:c}");
                Console.WriteLine("-----------------------------------");
            }

            Console.ReadKey();
        }
    }
}
