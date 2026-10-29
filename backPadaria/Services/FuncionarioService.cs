using backPadaria.Model;
using System;
using System.Collections.Generic;

namespace backPadaria.Service
{
    public static class FuncionarioService
    {
        public static void MenuFuncionario()
        {
            bool sair = false;

            while (!sair)
            {
                Console.Clear();

                Console.WriteLine("===== GESTÃO DE FUNCIONÁRIOS =====");

                Console.WriteLine("1 - Cadastrar Funcionário");
                Console.WriteLine("2 - Listar Todos os Funcionários");
                Console.WriteLine("3 - Buscar Funcionário por ID");
                Console.WriteLine("4 - Atualizar Funcionário");
                Console.WriteLine("5 - Excluir Funcionário");
                Console.WriteLine("6 - Alterar Cargo do Funcionário");
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
                            cadastrarFuncionarioMenu();
                            break;
                        case "2": // Listar
                            listarFuncionariosMenu();
                            break;
                        case "3": // Buscar por ID
                            listarPorIdFuncionarioMenu();
                            break;
                        case "4": // Atualizar
                            atualizarFuncionarioMenu();
                            break;
                        case "5": // Excluir
                            excluirFuncionarioMenu();
                            break;
                        case "6": // Alterar Cargo
                            alterarCargoFuncionarioMenu();
                            break;
                        case "0":
                            sair = true;
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

        private static void cadastrarFuncionarioMenu()
        {
            Console.WriteLine("------ CADASTRAR FUNCIONÁRIO ----- ");

            Console.WriteLine("Digite o nome do funcionário: ");
            string nomeFunc = Console.ReadLine();

            Console.WriteLine("Digite o CPF do funcionário: ");
            string cpfFunc = Console.ReadLine();

            Console.WriteLine("Digite o cargo do funcionário: ");
            string cargoFunc = Console.ReadLine();

            Funcionario novoFuncionario = new Funcionario(
                nome: nomeFunc,
                cpf: cpfFunc,
                cargo: cargoFunc
            );

            int resultadoInsert = Funcionario.Inserir(novoFuncionario);

            Console.Clear();

            if (resultadoInsert > 0) Console.WriteLine("Funcionário cadastrado com sucesso!!");

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
        }

        private static void listarFuncionariosMenu()
        {
            Console.WriteLine("------ LISTA DE FUNCIONÁRIOS ----- ");

            List<Funcionario> lista = Funcionario.ListarTodos();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum funcionário cadastrado no sistema");
            }
            else
            {
                foreach (var funcionario in lista)
                {
                    Console.WriteLine($"ID: {funcionario.Id} | Nome: {funcionario.Nome} | CPF: {funcionario.Cpf} | Cargo: {funcionario.Cargo}");
                }
            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
        }

        private static void listarPorIdFuncionarioMenu()
        {
            Console.WriteLine("------ BUSCAR POR ID ----- ");
            Console.WriteLine("Digite o id do funcionário: ");

            int idConsole;
            int.TryParse(Console.ReadLine(), out idConsole);

            Funcionario? funcionarioBusca = Funcionario.listarPorId(idConsole);

            if (funcionarioBusca == null)
            {
                Console.WriteLine("Funcionário não encontrado");
            }
            else
            {
                Console.WriteLine($"ID: {funcionarioBusca.Id} " +
                    $"\nNome: {funcionarioBusca.Nome} " +
                    $"\nCPF: {funcionarioBusca.Cpf} " +
                    $"\nCargo: {funcionarioBusca.Cargo}");
            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
        }

        private static void atualizarFuncionarioMenu()
        {
            Console.WriteLine("------ ATUALIZAR FUNCIONÁRIO POR ID ----- ");
            Console.WriteLine("Digite o id do funcionário que deseja atualizar: ");

            int idBusca = 0;
            int.TryParse(Console.ReadLine(), out idBusca);

            Console.WriteLine($"----------> {idBusca}");

            Funcionario? funcParaAtualizar = Funcionario.listarPorId(idBusca);

            if (funcParaAtualizar != null)
            {
                Console.WriteLine($"Atualizando dados de {funcParaAtualizar.Nome}");
                Console.WriteLine("Pressione ENTER sem digitar nada para manter o dado atual \n");

                Console.WriteLine($"Novo Nome: Atual ({funcParaAtualizar.Nome})");
                string novoNome = Console.ReadLine();
                if (!string.IsNullOrEmpty(novoNome)) funcParaAtualizar.Nome = novoNome;

                Console.WriteLine($"Novo CPF: Atual ({funcParaAtualizar.Cpf})");
                string novoCpf = Console.ReadLine();
                if (!string.IsNullOrEmpty(novoCpf)) funcParaAtualizar.Cpf = novoCpf;

                Console.WriteLine($"Novo Cargo: Atual ({funcParaAtualizar.Cargo})");
                string novoCargo = Console.ReadLine();
                if (!string.IsNullOrEmpty(novoCargo)) funcParaAtualizar.Cargo = novoCargo;

                int resultadoAtualizacao = Funcionario.Atualizar(funcParaAtualizar);
                if (resultadoAtualizacao > 0) Console.WriteLine("\nFuncionário atualizado com sucesso!");
            }
            else
            {
                Console.WriteLine("Funcionário não encontrado");
            }

            Console.ReadKey();
        }

        private static void excluirFuncionarioMenu()
        {
            Console.WriteLine("------ EXCLUIR FUNCIONÁRIO POR ID ----- ");
            Console.WriteLine("Digite o id do funcionário que deseja excluir: ");

            int idBusca = 0;
            int.TryParse(Console.ReadLine(), out idBusca);

            Funcionario? funcParaExcluir = Funcionario.listarPorId(idBusca);

            if (funcParaExcluir != null)
            {
                Funcionario.Deletar(funcParaExcluir);
                Console.WriteLine("Funcionário excluído com sucesso");
            }
            else
            {
                Console.WriteLine("Funcionário não encontrado");
            }

            Console.ReadKey();
        }

        private static void alterarCargoFuncionarioMenu()
        {
            Console.WriteLine("------ ALTERAR CARGO ----- ");
            Console.WriteLine("Digite o id do funcionário: ");

            int idConsole;
            int.TryParse(Console.ReadLine(), out idConsole);

            Funcionario? funcionarioBusca = Funcionario.listarPorId(idConsole);

            if (funcionarioBusca == null)
            {
                Console.WriteLine("Funcionário não encontrado");
            }
            else
            {
                Console.WriteLine($"Funcionário encontrado: {funcionarioBusca.Nome} | Cargo Atual: {funcionarioBusca.Cargo}");
                Console.WriteLine("Digite o novo cargo: ");

                string novoCargo = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(novoCargo))
                {
                    funcionarioBusca.Cargo = novoCargo;
                    int resultado = Funcionario.Atualizar(funcionarioBusca);

                    if (resultado > 0) Console.WriteLine("Cargo alterado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Operação cancelada. O cargo não pode ser vazio.");
                }
            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
        }
    }
}