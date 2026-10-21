using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Model
{
    internal class Cliente
    {
        private int id;
        private string nome;
        private string cpf;
        private string telefone;
        private string endereco;
        private int pontos;



        public Cliente(string nome,
            string cpf,
            string telefone,
            string endereco,
            int pontos)
        {

            if (string.IsNullOrWhiteSpace(nome)) throw new Exception("O nome do cliente nao pode estar vazio");
            this.Nome = nome;
            this.Cpf = cpf;
            this.Telefone = telefone;
            this.Endereco = endereco;
            this.Pontos = pontos;
        }
        //Gambiarra para testar 
        public Cliente(int id, string? nome)
        {
            this.id = id;
            this.nome = nome;
        }


        public Cliente(int id,
            string nome,
            string cpf,
            string telefone,
            string endereco,
            int pontos)
        {
            this.Id = id;
            this.Nome = nome;
            this.Cpf = cpf;
            this.Telefone = telefone;
            this.Endereco = endereco;
            this.Pontos = pontos;
        }

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string Endereco { get => endereco; set => endereco = value; }
        public int Pontos { get => pontos; set => pontos = value; }

        public void AdicionarPontos(int pontos)
        {
            this.Pontos += pontos;
        }

        public static int Inserir(Cliente cliente)
        {
            // Instrução SQL: Define o comando de inserção (INSERT).
            // O uso de marcações com '@' (ex: @Nome) e uma pratica de seguranca crucial chamada de "Parametrizacao"
            // Ela impede ataques de SQL Injection, pois o banco entende o que é comando e o que é apenas texto do usuário.
            string sql = "INSERT INTO 'Cliente' ('Nome','CPF','Telefone','Endereço','Pontos') Values (@Nome,@CPF,@Telefone,@Endereco,@Pontos)";
            // Gerenciamento de recursos: Cria o objeto de comando já passando a instrução SQL.
            // O 'using' garante que os recursos de memoria ocupados pelo comando sejam liberados ao final do bloco.
            using (var cmd = new MySqlCommand(sql))
            {
                // Vinculação de Dados: Pega as propriedades do objeto C# (cliente.Nome, uuario.Email, etc.)
                // e substitui de forma segura as marcações '@' da string SQL com os valores reais.
                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@CPF", cliente.Cpf);
                cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                cmd.Parameters.AddWithValue("@Pontos", cliente.Pontos);

                // Execucao: Como o comando de INSERT nao retorna tabelas (apenas executa uma ação),
                // ele envia o comando configurado para o método genérico 'executarComando' da classe 'Banco'.
                // 0 metodo retorna 1 (se o usuario for inserido com sucesso) ou -1 (se houver erro no banco).
                return Banco.ExecutarComando(cmd);

            }
        }

        public static int Deletar(Cliente cliente)
        {
            string sql = "DELETE FROM `Cliente` WHERE `Id` = @id";
            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", cliente.id);

                return Banco.ExecutarComando(cmd);
            }
        }

                public static List<Cliente> ListarTodos()
        {
            string cmdSql = "SELECT * FROM `Cliente`";

            List<Cliente> listaClientes = new();

            using (var cmd = new MySqlCommand(cmdSql))
            {
                // Executa a leitura. 0 bloco 'using' aqui é FUNDAMENTAL para garantir que o
                // reader seja fechado no final, o que por sua vez fecha a conexao com o banco.
                using (MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd))
                {
                    // O while vai rodar uma vez para CADA linha que existir na tabela Cliente
                    while (reader.Read())
                    {
                        // Extrai os dados da linha atual
                        int id = reader.GetInt32("Id");

                        string nome = ValidarCampo(reader["Nome"]);

                        string cpf = ValidarCampo(reader["CPF"]);
                        string telefone = ValidarCampo(reader["Telefone"]);
                        string endereco = ValidarCampo(reader["Endereco"]);
                        int pontos = reader.GetInt32("Pontos");

                        // Cria o objeto Cliente com os dados extraidos
                        Cliente cliente = new(id, nome, cpf, endereco, telefone, pontos);

                        // Adiciona este cliente a nossa lista
                        listaClientes.Add(cliente);
                    }
                }

                return listaClientes;
            }
        }
        public static Cliente? ListarPorID(int id)
        {
            string sql = "SELECT * FROM `Cliente` WHERE `id` = @Id";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd);
                Cliente? cliente = null;

                while (reader.Read())
                {
                    // extrair os dados da linha atual
                    int idBanco = reader.GetInt32("Id");

                    string nome = ValidarCampo(reader["Nome"]);
                    string cpf = ValidarCampo(reader["CPF"]);
                    string telefone = ValidarCampo(reader["Telefone"]);
                    string endereco = ValidarCampo(reader["Endereco"]);

                    int pontos = reader.GetInt32("Pontos");

                    //Cria o objeto Cliente com os dados extraidos
                    cliente = new Cliente(idBanco, nome, cpf, telefone, endereco, pontos);


                }
            return cliente;
            }
        }


        public static int Atualizar(Cliente cliente)
        {

            string sql = "UPDATE `Cliente` SET `Nome` = @Nome, `Cpf` = @CPF, `Telefone` = @Telefone, `Endereco` = @Endereco, `Pontos` = @Pontos WHERE `Id` = @Id";

            using (var cmd = new MySqlCommand(sql))
            {
                // Vinculação de parâmetros: Protege contra SQL Injection e organiza os dados
                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@CPF", cliente.Cpf);
                cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                cmd.Parameters.AddWithValue("@Pontos", cliente.Pontos);

                // A chave de busca: Quem será atualizado?
                cmd.Parameters.AddWithValue("@Id", cliente.id);

                // Execução: O UPDATE também é uma instrução de ação (como INSERT e DELETE).
                // Ele vai retornar 1 (se encontrou o ID e atualizou com sucesso) ou 0 (se o ID não existia).
                return Banco.ExecutarComando(cmd);
            }

        }



        private static string ValidarCampo(object campo)
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