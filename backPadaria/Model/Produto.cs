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
        private int marcaId;

        public Produto(string nome, decimal preco, string categoria, bool fabricado, int marcaId)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new Exception("O nome do produto não pode ser vazio");
            if (preco < 0) throw new Exception("O preço do produto não pode ser negativo");
            if (string.IsNullOrWhiteSpace(categoria)) throw new Exception("A categoria não pode ser vazia");
            if (marcaId <= 0) throw new Exception("O id da Marca deve ser maior que 0");

            this.Nome = nome;
            this.Preco = preco;
            this.Categoria = categoria;
            this.Fabricado = fabricado;
            this.MarcaId = marcaId;
        }

        public Produto(int id, string nome, decimal preco, string categoria, bool fabricado, int marcaId)
            : this(nome, preco, categoria, fabricado, marcaId)
        {
            if (id <= 0) throw new Exception("O ID do produto é inválido");

            this.Id = id;

        }

        public Produto(int id)
        {
            this.Id = id;
        }

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public decimal Preco { get => preco; set => preco = value; }
        public string Categoria { get => categoria; set => categoria = value; }
        public bool Fabricado { get => fabricado; set => fabricado = value; }
        public int MarcaId { get => marcaId; set => marcaId = value; }

        public static int Inserir(Produto produto)
        {
            string sql = "INSERT INTO `Produto` (`Nome`, `Preco`, `Categoria`, `Fabricado`, `MarcaId`) VALUES (@Nome, @Preco, @Categoria, @Fabricado, @MarcaId)";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                cmd.Parameters.AddWithValue("@Categoria", produto.Categoria);
                cmd.Parameters.AddWithValue("@Fabricado", produto.Fabricado);
                cmd.Parameters.AddWithValue("@MarcaId", produto.MarcaId);

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
            string sql = "SELECT * FROM `Produto`";
            List<Produto> listaProdutos = new();

            using (var cmd = new MySqlCommand(sql))
            {
                using (MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd))
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("Id");
                        string nome = validarCampo(reader["Nome"]);
                        decimal preco = Convert.ToDecimal(reader["Preco"]);
                        string categoria = validarCampo(reader["Categoria"]);
                        bool fabricado = Convert.ToBoolean(reader["Fabricado"]);
                        int marcaId = Convert.ToInt32(reader["MarcaId"]);

                        Produto produto = new Produto(id, nome, preco, categoria, fabricado, marcaId);
                        listaProdutos.Add(produto);
                    }
                }
                return listaProdutos;
            }
        }

        public static Produto? listarPorId(int id)
        {
            string sql = "SELECT * FROM `Produto` WHERE `id` = @Id";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd);

                Produto? produto = null;

                while (reader.Read())
                {
                    int idBanco = reader.GetInt32("Id");
                    string nome = validarCampo(reader["Nome"]);
                    decimal preco = Convert.ToDecimal(reader["Preco"]);
                    string categoria = validarCampo(reader["Categoria"]);
                    bool fabricado = Convert.ToBoolean(reader["Fabricado"]);
                    int marcaId = Convert.ToInt32(reader["MarcaId"]);

                    produto = new Produto(idBanco, nome, preco, categoria, fabricado, marcaId);
                }
                return produto;
            }
        }

        public static int Atualizar(Produto produto)
        {
            string sql = "UPDATE `Produto` SET `Nome` = @Nome, `Preco` = @Preco, `Categoria` = @Categoria, `Fabricado` = @Fabricado, `MarcaId` = @MarcaId WHERE `Id` = @Id";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                cmd.Parameters.AddWithValue("@Categoria", produto.Categoria);
                cmd.Parameters.AddWithValue("@Fabricado", produto.Fabricado);
                cmd.Parameters.AddWithValue("@MarcaId", produto.MarcaId);
                cmd.Parameters.AddWithValue("@Id", produto.id);

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
