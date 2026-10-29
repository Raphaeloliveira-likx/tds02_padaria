using backPadaria.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Service
{
    public static class MarcaService
    {
        public static void MenuMarca()
        {
            bool sair = false;

            while (!sair)
            {
                Console.Clear();

                Console.WriteLine("===== GESTÃO DE MARCAS =====");

                Console.WriteLine("1 - Registar Marca");
                Console.WriteLine("2 - Listar Todas as Marcas");
                Console.WriteLine("3 - Pesquisar Marca por ID");
                Console.WriteLine("4 - Atualizar Marca");
                Console.WriteLine("5 - Eliminar Marca");
                Console.WriteLine("0 - Sair do Programa");

                Console.WriteLine("======================");
                Console.WriteLine("Digite uma opção: ");

                string opcao = Console.ReadLine();

                Console.Clear();

                try
                {
                    switch (opcao)
                    {
                        case "1": // Registar
                            cadastrarMarcaMenu();
                            break;
                        case "2": // Listar
                            listarMarcasMenu();
                            break;
                        case "3": // Pesquisar por ID
                            listarPorIdMarcaMenu();
                            break;
                        case "4": // Atualizar
                            atualizarMarcaMenu();
                            break;
                        case "5": // Eliminar
                            excluirMarcaMenu();
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
        } // FIM MENU

        private static void cadastrarMarcaMenu()
        {
            Console.WriteLine("------ REGISTAR MARCA ----- ");

            Console.WriteLine("Digite o nome da marca: ");
            string nomeMarca = Console.ReadLine();

            Console.WriteLine("Digite a descrição da marca: ");
            string descricaoMarca = Console.ReadLine();

            Marca novaMarca = new Marca(
                nome: nomeMarca,
                descricao: descricaoMarca
            );

            int resultadoInsert = Marca.Inserir(novaMarca);

            Console.Clear();

            if (resultadoInsert > 0) Console.WriteLine("Marca registada com sucesso!!");

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
        }

        private static void listarMarcasMenu()
        {
            Console.WriteLine("------ LISTA DE MARCAS ----- ");

            List<Marca> lista = Marca.ListarTodos();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma marca registada no sistema.");
            }
            else
            {
                foreach (var marca in lista)
                {
                    Console.WriteLine($"ID: {marca.Id} | Nome: {marca.Nome} | Descrição: {marca.Descricao}");
                }
            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
        }

        private static void listarPorIdMarcaMenu()
        {
            Console.WriteLine("------ PESQUISAR POR ID ----- ");
            Console.WriteLine("Digite o id da marca: ");

            int idConsole;
            int.TryParse(Console.ReadLine(), out idConsole);

            Marca? marcaBusca = Marca.listarPorId(idConsole);

            if (marcaBusca == null)
            {
                Console.WriteLine("Marca não encontrada.");
            }
            else
            {
                Console.WriteLine($"ID: {marcaBusca.Id} " +
                    $"\nNome: {marcaBusca.Nome} " +
                    $"\nDescrição: {marcaBusca.Descricao}");
            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
        }

        private static void atualizarMarcaMenu()
        {
            Console.WriteLine("------ ATUALIZAR MARCA POR ID ----- ");
            Console.WriteLine("Digite o id da marca que deseja atualizar: ");

            int idBusca = 0;
            int.TryParse(Console.ReadLine(), out idBusca);

            Console.WriteLine($"----------> {idBusca}");

            Marca? marcaParaAtualizar = Marca.listarPorId(idBusca);

            if (marcaParaAtualizar != null)
            {
                Console.WriteLine($"A atualizar dados de {marcaParaAtualizar.Nome}");
                Console.WriteLine("Pressione ENTER sem digitar nada para manter o dado atual \n");

                Console.WriteLine($"Novo Nome: Atual ({marcaParaAtualizar.Nome})");
                string novoNome = Console.ReadLine();
                if (!string.IsNullOrEmpty(novoNome)) marcaParaAtualizar.Nome = novoNome;

                Console.WriteLine($"Nova Descrição: Atual ({marcaParaAtualizar.Descricao})");
                string novaDescricao = Console.ReadLine();
                if (!string.IsNullOrEmpty(novaDescricao)) marcaParaAtualizar.Descricao = novaDescricao;

                int resultadoAtualizacao = Marca.Atualizar(marcaParaAtualizar);
                if (resultadoAtualizacao > 0) Console.WriteLine("\nMarca atualizada com sucesso!");
            }
            else
            {
                Console.WriteLine("Marca não encontrada.");
            }

            Console.ReadKey();
        }

        private static void excluirMarcaMenu()
        {
            Console.WriteLine("------ ELIMINAR MARCA POR ID ----- ");
            Console.WriteLine("Digite o id da marca que deseja eliminar: ");

            int idBusca = 0;
            int.TryParse(Console.ReadLine(), out idBusca);

            Marca? marcaParaExcluir = Marca.listarPorId(idBusca);

            if (marcaParaExcluir != null)
            {
                Marca.Deletar(marcaParaExcluir);
                Console.WriteLine("Marca eliminada com sucesso.");
            }
            else
            {
                Console.WriteLine("Marca não encontrada.");
            }

            Console.ReadKey();
        }
    }
}