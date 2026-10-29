using backPadaria.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Service
{
    public static class ProdutoService
    {
        public static void MenuProduto()
        {
            bool sair = false;
            while (!sair)
            {
                Console.Clear();
                Console.WriteLine("===== GESTÃO DE PRODUTOS =====");
                Console.WriteLine("1 - Registar Produto (Geral e Fabricação)");
                Console.WriteLine("2 - Listar Todos os Produtos");
                Console.WriteLine("3 - Pesquisar Produto por ID");
                Console.WriteLine("4 - Atualizar Produto");
                Console.WriteLine("5 - Eliminar Produto");
                Console.WriteLine("0 - Sair do Programa");
                Console.WriteLine("======================");
                Console.WriteLine("Digite uma opção: ");

                string opcao = Console.ReadLine();
                Console.Clear();

                try
                {
                    switch (opcao)
                    {
                        case "1": cadastrarProdutoMenu(); break;
                        case "2": listarProdutosMenu(); break;
                        case "3": listarPorIdProdutoMenu(); break;
                        case "4": atualizarProdutoMenu(); break;
                        case "5": excluirProdutoMenu(); break;
                        case "0": sair = true; break;
                        default: Console.WriteLine("Opção inválida."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[ERRO]: {ex.Message}");
                    Console.WriteLine("\nPressione uma tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        private static void cadastrarProdutoMenu()
        {
            Console.WriteLine("------ REGISTAR PRODUTO ----- ");

            Console.WriteLine("Nome do produto: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Preço (Ex: 15,50): ");
            decimal.TryParse(Console.ReadLine(), out decimal preco);

            Console.WriteLine("Categoria: ");
            string categoria = Console.ReadLine();

            //  s  
            //s
            //S  == S
            Console.WriteLine("O produto é de fabricação própria? (S/N): ");
            bool fabricado = Console.ReadLine().Trim().ToUpper() == "S";

            Console.WriteLine("ID da Marca: ");
            int.TryParse(Console.ReadLine(), out int idMarca);

            Produto novoProduto = new Produto(nome, preco, categoria, fabricado, idMarca);
            int resultado = Produto.Inserir(novoProduto);

            if (resultado > 0)
            {
                Console.WriteLine("\nProduto base registado com sucesso!");

                // FLUXO MELHORADO: Se for fabricado, cadastra os detalhes imediatamente
                if (fabricado)
                {
                    // Como não alteramos o Model para retornar o ID gerado (SCOPE_IDENTITY), 
                    // buscamos o último produto inserido com este nome para fazer o vínculo.
                    Produto produtoInserido = Produto.ListarTodos().LastOrDefault(p => p.Nome == nome);

                    if (produtoInserido != null)
                    {
                        Console.WriteLine("\n--- DETALHES DE FABRICAÇÃO ---");
                        Console.WriteLine("Digite a Descrição da Produção:");
                        string descricao = Console.ReadLine();

                        ProdutoFabricado novaFabricacao = new ProdutoFabricado(produtoInserido.Id, descricao);
                        ProdutoFabricado.Inserir(novaFabricacao);

                        Console.WriteLine("Detalhes de fabricação vinculados com sucesso!");
                    }
                }
            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
        }

        private static void listarProdutosMenu()
        {
            Console.WriteLine("------ LISTA DE PRODUTOS ----- ");
            List<Produto> lista = Produto.ListarTodos();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum produto registado.");
            }
            else
            {
                foreach (var produto in lista)
                {
                    string origem = produto.Fabricado ? "Próprio" : "Terceiros";
                    string extraInfo = "";

                    // FLUXO MELHORADO: Busca a descrição de produção caso seja de fabricação própria
                    if (produto.Fabricado)
                    {
                        ProdutoFabricado detalhes = ProdutoFabricado.listarPorId(produto.Id);
                        extraInfo = detalhes != null ? $" | Produção: {detalhes.DescricaoProducao}" : " | Produção: (Sem descrição)";
                    }

                    Console.WriteLine($"ID: {produto.Id} | Nome: {produto.Nome} | Preço: {produto.Preco:C} | Categoria: {produto.Categoria} | Fabricação: {origem}{extraInfo}");
                }
            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
        }

        private static void listarPorIdProdutoMenu()
        {
            Console.WriteLine("------ PESQUISAR POR ID ----- ");
            Console.WriteLine("Digite o id do produto: ");
            int.TryParse(Console.ReadLine(), out int idConsole);

            Produto produtoBusca = Produto.listarPorId(idConsole);

            if (produtoBusca == null)
            {
                Console.WriteLine("Produto não encontrado.");
            }
            else
            {
                string origem = produtoBusca.Fabricado ? "Próprio" : "Terceiros";
                Console.WriteLine($"ID: {produtoBusca.Id}\nNome: {produtoBusca.Nome}\nPreço: {produtoBusca.Preco:C}\nCategoria: {produtoBusca.Categoria}\nFabricação: {origem}\nID Marca: {produtoBusca.MarcaId}");

                // FLUXO MELHORADO: Mostra detalhes de fabricação no mesmo ecrã
                if (produtoBusca.Fabricado)
                {
                    Console.WriteLine("\n--- DETALHES DE FABRICAÇÃO ---");
                    ProdutoFabricado detalhes = ProdutoFabricado.listarPorId(produtoBusca.Id);
                    if (detalhes != null)
                        Console.WriteLine($"Descrição: {detalhes.DescricaoProducao}");
                    else
                        Console.WriteLine("Nenhuma descrição registada na base de dados.");
                }
            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
        }

        private static void atualizarProdutoMenu()
        {
            Console.WriteLine("------ ATUALIZAR PRODUTO POR ID ----- ");
            Console.WriteLine("Digite o id do produto que deseja atualizar: ");
            int.TryParse(Console.ReadLine(), out int idBusca);

            Produto produto = Produto.listarPorId(idBusca);

            if (produto != null)
            {
                Console.WriteLine($"A atualizar: {produto.Nome}");
                Console.WriteLine("(Pressione ENTER vazio para manter o dado atual)\n");

                Console.WriteLine($"Novo Nome ({produto.Nome}): ");
                string novoNome = Console.ReadLine();
                if (!string.IsNullOrEmpty(novoNome)) produto.Nome = novoNome;

                Console.WriteLine($"Novo Preço ({produto.Preco:C}): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal novoPreco)) produto.Preco = novoPreco;

                int resultado = Produto.Atualizar(produto);
                if (resultado > 0) Console.WriteLine("Produto atualizado com sucesso!");
            }
            else
            {
                Console.WriteLine("Produto não encontrado.");
            }

            Console.ReadKey();
        }

        private static void excluirProdutoMenu()
        {
            Console.WriteLine("------ ELIMINAR PRODUTO ----- ");
            Console.WriteLine("Digite o ID: ");
            int.TryParse(Console.ReadLine(), out int id);

            Produto produto = Produto.listarPorId(id);
            if (produto != null)
            {
                // Se for de fabricação própria, deve excluir os detalhes de produção primeiro para evitar erro de Foreign Key
                if (produto.Fabricado)
                {
                    ProdutoFabricado detalhes = ProdutoFabricado.listarPorId(produto.Id);
                    if (detalhes != null) ProdutoFabricado.Deletar(detalhes);
                }

                Produto.Deletar(produto);
                Console.WriteLine("Produto eliminado com sucesso.");
            }
            else
            {
                Console.WriteLine("Produto não encontrado.");
            }
            Console.ReadKey();
        }
    }
}