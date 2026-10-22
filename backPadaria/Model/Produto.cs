using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Model
{
    internal class Produto
    {
        
            private int id;
            private string nome;
            private decimal preco;
            private string categoria;
            private bool fabricado;
            private string marca;

        public Produto(
                string nome,
                decimal preco,
                string categoria,
                bool fabricado,
                string marca)
        {
            
            this.nome = nome;
            this.preco = preco;
            this.categoria = categoria;
            this.fabricado = fabricado;
            this.marca = marca;
        }



            public Produto(int id,
                string nome,
                decimal preco,
                string categoria,
                bool fabricado,
                string marca)
            {

            if (string.IsNullOrWhiteSpace(nome)) throw new Exception("O nome do Produto nao pode estar vazio");
            
            this.Id = id;
                this.Nome = nome;
                this.Preco = preco;
                this.Categoria = categoria;
                this.Fabricado = fabricado;
                this.Marca = marca;
            }

            public int Id { get => id; set => id = value; }
            public string Nome { get => nome; set => nome = value; }
            public decimal Preco { get => preco; set => preco = value; }
            public string Categoria { get => categoria; set => categoria = value; }
            public bool Fabricado { get => fabricado; set => fabricado = value; }
            public string Marca { get => marca; set => marca = value; }

            
            public static int Inserir(Produto produto)
            {
                
                string sql = "INSERT INTO 'Produto' ('Nome','Preco','Categoria','Fabricado','Marca') Values (@Nome,@Preco,@Categoria,@Fabricado,@Marca)";
                
                using (var cmd = new MySqlCommand(sql))
                {
                    
                    cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                    cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                    cmd.Parameters.AddWithValue("@Categoria", produto.Categoria);
                    cmd.Parameters.AddWithValue("@Fabricado", produto.Fabricado);
                    cmd.Parameters.AddWithValue("@Marca", produto.Marca);

                    
                    return Banco.ExecutarComando(cmd);

                }
            }

            public static int Deletar(Produto produto)
            {
                string sql = "DELETE FROM `Produto` WHERE `Id` = @id";
                using (var cmd = new MySqlCommand(sql))
                {
                    cmd.Parameters.AddWithValue("@id", produto.id);

                    return Banco.ExecutarComando(cmd);
                }
            }

            public static List<Produto> ListarTodos()
            {
                string cmdSql = "SELECT * FROM `Produto`";

                List<Produto> listaProdutos = new();

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

                            decimal preco = reader.GetDecimal("Preco");
                            string categoria = ValidarCampo(reader["Categoria"]);
                            bool fabricado = reader.GetBoolean("Fabricado");
                            string marca = ValidarCampo(reader["Marca"]);

                            // Cria o objeto Cliente com os dados extraidos
                            Produto produto = new(id, nome, preco, categoria, fabricado, marca);

                            // Adiciona este cliente a nossa lista
                            listaProdutos.Add(produto);
                        }
                    }

                    return listaProdutos;
                }
            }
            public static Produto? ListarPorID(int id)
            {
                string sql = "SELECT * FROM `Produto` WHERE `id` = @Id";

                using (var cmd = new MySqlCommand(sql))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd);
                    Produto? produto = null;

                    while (reader.Read())
                    {
                        // extrair os dados da linha atual
                        int idBanco = reader.GetInt32("Id");

                        string nome = ValidarCampo(reader["Nome"]);
                        decimal preco = reader.GetDecimal("Preco");
                        bool fabricado = reader.GetBoolean("Fabricado");
                        string categoria = ValidarCampo(reader["Categoria"]);

                        string marca = ValidarCampo(reader["Marca"]);

                        //Cria o objeto Cliente com os dados extraidos
                        produto = new Produto(idBanco, nome, preco, categoria, fabricado, marca);


                    }
                    return produto;
                }
            }


            public static int Atualizar(Produto produto)
            {

                string sql = "UPDATE `Produto` SET `Nome` = @Nome, `Preco` = @Preco, `Fabricado` = @Fabricado, `Categoria` = @Categoria, `Marca` = @Marca WHERE `Id` = @Id";

                using (var cmd = new MySqlCommand(sql))
                {
                    // Vinculação de parâmetros: Protege contra SQL Injection e organiza os dados
                    cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                    cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                    cmd.Parameters.AddWithValue("@Fabricado", produto.Fabricado);
                    cmd.Parameters.AddWithValue("@categoria", produto.Categoria);
                    cmd.Parameters.AddWithValue("@Marca", produto.Marca);

                    // A chave de busca: Quem será atualizado?
                    cmd.Parameters.AddWithValue("@Id", produto.id);

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


