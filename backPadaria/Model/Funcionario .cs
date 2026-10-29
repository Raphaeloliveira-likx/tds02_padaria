using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace backPadaria.Model
{
    internal class Funcionario
    {

        private int id;
        private string nome;
        private string cpf;
        private string cargo;

        public Funcionario(string nome, string cpf, string cargo)
        {

            if (string.IsNullOrWhiteSpace(nome)) throw new Exception("O nome do funcionário não pode ser vazio");

            this.Nome = nome;
            this.Cpf = cpf;
            this.Cargo = cargo;
        }

        public Funcionario(int id, string nome, string cpf, string cargo)
        {
            this.Id = id;
            this.Nome = nome;
            this.Cpf = cpf;
            this.Cargo = cargo;
        }

        // Gambiarra para testar
        public Funcionario(int id)
        {
            this.Id = id;
        }

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Cargo { get => cargo; set => cargo = value; }

        public static int Inserir(Funcionario funcionario)
        {
            // Instrução SQL: Define o comando de inserção (INSERT).
            // O uso de marcações com '@' (ex: @Nome) é uma prática de segurança crucial chamada de "Parametrização".
            // Ela impede ataques de SQL Injection, pois o banco entende o que é comando e o que é apenas texto do usuário.

            string sql = "INSERT INTO `Funcionario` (`Nome`, `CPF`, `Cargo`) VALUES (@Nome, @CPF, @Cargo)";

            // Gerenciamento de recursos: Cria o objeto de comando já passando a instrução SQL.
            // O 'using' garante que os recursos de memória ocupados pelo comando sejam liberados ao final do bloco.

            using (var cmd = new MySqlCommand(sql))
            {
                // Vinculação de Dados: Pega as propriedades do objeto C# (funcionario.Nome, funcionario.cpf, etc.)
                // e substitui de forma segura as marcações '@' da string SQL com os valores reais.

                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@CPF", funcionario.Cpf);
                cmd.Parameters.AddWithValue("@Cargo", funcionario.Cargo);

                // Execução: Como o comando de INSERT não retorna tabelas (apenas executa uma ação), 
                // ele envia o comando configurado para o método genérico 'executarComando' da classe 'Banco'.
                // O método retorna 1 (se o usuário for inserido com sucesso) ou -1 (se houver erro no banco).

                return Banco.ExecutarComando(cmd);
            }

        }

        public static int Deletar(Funcionario funcionario)
        {

            string sql = "DELETE FROM `Funcionario` WHERE `Id` = @id";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", funcionario.id);

                return Banco.ExecutarComando(cmd);
            }

        }

        public static List<Funcionario> ListarTodos()
        {
            string sql = "SELECT * FROM `Funcionario`";

            List<Funcionario> listaFuncionarios = new();

            using (var cmd = new MySqlCommand(sql))
            {
                // Executa a leitura. O bloco 'using' aqui é FUNDAMENTAL para garantir que o 
                // reader seja fechado no final, o que por sua vez fecha a conexão com o banco.
                using (MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd))
                {

                    // O while vai rodar uma vez para CADA linha que existir na tabela Funcionario
                    while (reader.Read())
                    {
                        // Extrai os dados da linha atual
                        int id = reader.GetInt32("Id");

                        string nome = validarCampo(reader["Nome"]);
                        string cpf = validarCampo(reader["CPF"]);
                        string cargo = validarCampo(reader["Cargo"]);

                        // Cria o objeto Funcionario com os dados extraídos
                        Funcionario funcionario = new Funcionario(id, nome, cpf, cargo);

                        // Adiciona este funcionario à nossa lista
                        listaFuncionarios.Add(funcionario);
                    }

                }
                // Retorna a lista completa (se a tabela estiver vazia, retorna uma lista com 0 itens)
                return listaFuncionarios;

            }

        }

        public static Funcionario? listarPorId(int id)
        {

            string sql = "SELECT * FROM `Funcionario` WHERE `id` = @Id";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd);

                Funcionario? funcionario = null;

                while (reader.Read())
                {

                    // Extrai os dados da linha atual
                    int idBanco = reader.GetInt32("Id");

                    string nome = validarCampo(reader["Nome"]);
                    string cpf = validarCampo(reader["CPF"]);
                    string cargo = validarCampo(reader["Cargo"]);

                    // Cria o objeto Funcionario com os dados extraídos
                    funcionario = new Funcionario(idBanco, nome, cpf, cargo);

                }

                return funcionario;
            }

        }

        public static int Atualizar(Funcionario funcionario)
        {

            string sql = "UPDATE `Funcionario` SET `Nome` = @Nome, `CPF` = @CPF, `Cargo` = @Cargo WHERE `Id` = @Id";

            using (var cmd = new MySqlCommand(sql))
            {
                // Vinculação de parâmetros: Protege contra SQL Injection e organiza os dados
                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@CPF", funcionario.Cpf);
                cmd.Parameters.AddWithValue("@Cargo", funcionario.Cargo);

                // A chave de busca: Quem será atualizado?
                cmd.Parameters.AddWithValue("@Id", funcionario.id);

                // Execução: O UPDATE também é uma instrução de ação (como INSERT e DELETE).
                // Ele vai retornar 1 (se encontrou o ID e atualizou com sucesso) ou 0 (se o ID não existia).
                return Banco.ExecutarComando(cmd);
            }

        }

        private static string validarCampo(object campo)
        {
            if (campo == DBNull.Value || campo == null)
            {
                return "";
            }
            else
            {
                return campo.ToString();
            }

        }
    }

}