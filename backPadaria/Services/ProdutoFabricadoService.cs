using backPadaria.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Service
{
    public static class ProdutoFabricadoService
    {
        public static void MenuProdutoFabricado()
        {
            bool sair = false;

            while (!sair)
            {
                Console.Clear();

                Console.WriteLine("===== GESTÃO DE PRODUTOS FABRICADOS =====");

                Console.WriteLine("1 - Registar Detalhes de Fabricação (Associar a Produto)");
                Console.WriteLine("2 - Listar Produtos Fabricados (Com dados herdados)");
                Console.WriteLine("3 - Pesquisar Fabricação por ID do Produto");
                Console.WriteLine("4 - Atualizar Descrição de Produção");
                Console.WriteLine("5 - Eliminar Registo de Fabricação");
                Console.WriteLine("0 - Sair do Programa");

                Console.WriteLine("======================");
                Console.WriteLine("Digite uma opção: ");

                string opcao = Console.ReadLine();

                Console.Clear();

                try
                {
                    switch (opcao)
                    {
                        case "1":
                            cadastrarProdutoFabricadoMenu();
                            break;
                        case "2":
                            listarProdutosFabricadosMenu();
                            break;
                        case "3":
                            listarPorIdProdutoFabricadoMenu();
                            break;
                        case "4":
                            atualizarProdutoFabricadoMenu();
                            break;
                        case "5":
                            excluirProdutoFabricadoMenu();
                            break;
                        case "0":
                            sair = true;
                            break;
                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[ERRO DE VALIDAÇÃO]: {ex.Message}");
                    Console.WriteLine("\nPressione uma tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        private static void cadastrarProdutoFabricadoMenu()
        {
            Console.WriteLine("------ REGISTAR DETALHES DE FABRICAÇÃO ----- ");
            Console.WriteLine("Atenção: O Produto já deve estar registado no sistema principal de Produtos.\n");

            Console.WriteLine("Digite o ID do Produto associado: ");
            int idProduto;
            int.TryParse(Console.ReadLine(), out idProduto);

            Console.WriteLine("Digite a Descrição da Produção: ");
            string descricao = Console.ReadLine();

            ProdutoFabricado novoRegisto = new ProdutoFabricado(
                produtoId: idProduto,
                descricaoProducao: descricao
            );

            int resultadoInsert = ProdutoFabricado.Inserir(novoRegisto);

            Console.Clear();

            if (resultadoInsert > 0) Console.WriteLine("Detalhes de fabricação guardados com sucesso!!");

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
        }

        private static void listarProdutosFabricadosMenu()
        {
            Console.WriteLine("------ LISTA DE PRODUTOS FABRICADOS ----- ");

            List<ProdutoFabricado> lista = ProdutoFabricado.ListarTodos();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum produto fabricado registado no sistema.");
            }
            else
            {
                foreach (var registo in lista)
                {
                    Console.WriteLine($"ID Produto: {registo.ProdutoId} | Produto: {registo.Nome} | Preço: {registo.Preco:C} | Descrição: {registo.DescricaoProducao}");
                }
            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
        }

        private static void listarPorIdProdutoFabricadoMenu()
        {
            Console.WriteLine("------ PESQUISAR FABRICAÇÃO POR ID DO PRODUTO ----- ");
            Console.WriteLine("Digite o ID do Produto: ");

            int idConsole;
            int.TryParse(Console.ReadLine(), out idConsole);

            ProdutoFabricado? registoBusca = ProdutoFabricado.listarPorId(idConsole);

            if (registoBusca == null)
            {
                Console.WriteLine("Registo de fabricação não encontrado para este produto.");
            }
            else
            {
                Console.WriteLine($"--- DADOS HERDADOS (Produto) ---");
                Console.WriteLine($"ID Principal: {registoBusca.Id}");
                Console.WriteLine($"Nome: {registoBusca.Nome}");
                Console.WriteLine($"Categoria: {registoBusca.Categoria}");
                Console.WriteLine($"Preço: {registoBusca.Preco:C}");

                Console.WriteLine($"\n--- DADOS DA ESPECIALIZAÇÃO (Fabricação) ---");
                Console.WriteLine($"ID Produto Associado: {registoBusca.ProdutoId}");
                Console.WriteLine($"Descrição de Produção: {registoBusca.DescricaoProducao}");
            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
        }

        private static void atualizarProdutoFabricadoMenu()
        {
            Console.WriteLine("------ ATUALIZAR DESCRIÇÃO DE PRODUÇÃO ----- ");
            Console.WriteLine("Digite o ID do Produto que deseja atualizar: ");

            int idBusca = 0;
            int.TryParse(Console.ReadLine(), out idBusca);

            ProdutoFabricado? registoParaAtualizar = ProdutoFabricado.listarPorId(idBusca);

            if (registoParaAtualizar != null)
            {
                Console.WriteLine($"A atualizar a fabricação do Produto: {registoParaAtualizar.Nome}");
                Console.WriteLine("Pressione ENTER sem digitar nada para manter a descrição atual \n");

                Console.WriteLine($"Nova Descrição de Produção: Atual ({registoParaAtualizar.DescricaoProducao})");
                string novaDescricao = Console.ReadLine();

                if (!string.IsNullOrEmpty(novaDescricao))
                {
                    registoParaAtualizar.DescricaoProducao = novaDescricao;
                }

                int resultadoAtualizacao = ProdutoFabricado.Atualizar(registoParaAtualizar);
                if (resultadoAtualizacao > 0) Console.WriteLine("\nDescrição atualizada com sucesso!");
            }
            else
            {
                Console.WriteLine("Registo não encontrado.");
            }

            Console.ReadKey();
        }

        private static void excluirProdutoFabricadoMenu()
        {
            Console.WriteLine("------ ELIMINAR REGISTO DE FABRICAÇÃO ----- ");
            Console.WriteLine("Digite o ID do Produto do qual deseja remover a descrição de produção: ");

            int idBusca = 0;
            int.TryParse(Console.ReadLine(), out idBusca);

            ProdutoFabricado? registoParaExcluir = ProdutoFabricado.listarPorId(idBusca);

            if (registoParaExcluir != null)
            {
                ProdutoFabricado.Deletar(registoParaExcluir);
                Console.WriteLine($"Os detalhes de fabricação do produto '{registoParaExcluir.Nome}' foram eliminados.");
                Console.WriteLine("(Nota: O produto principal não foi apagado, apenas a sua especialização).");
            }
            else
            {
                Console.WriteLine("Registo não encontrado.");
            }

            Console.ReadKey();
        }
    }
}