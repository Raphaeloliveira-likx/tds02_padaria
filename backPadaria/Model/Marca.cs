using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Model
{
    internal class Marca
    {
        private int id;
        private string nome;       
        private string telefone;

        public Marca(int id, string nome,string telefone)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new Exception("O nome da Marca nao pode estar vazio");
            this.id = id;
            this.nome = nome;         
            this.telefone = telefone;
        }
        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }        
        public string Telefone { get => telefone; set => telefone = value; }

        public static int Inserir(Marca marca)
        {
            string sql = "INSERT INTO `Marca` (`Nome`,`Telefone`) Values (@Nome,@Telefone)";
            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@Nome", marca.Nome);                
                cmd.Parameters.AddWithValue("@Telefone", marca.Telefone);

                return Banco.ExecutarComando(cmd);
            }
        }
        public static int Deletar(Marca marca)
        {
            string sql = "DELETE FROM `Marca` WHERE `Id` = @id";
            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", marca.id);

                return Banco.ExecutarComando(cmd);
            }
        }
        public static List<Marca> ListarTodos()
        {
            string cmdSql = "SELECT * FROM `Marca`";

            List<Marca> listaFornecedor = new();

            using (var cmd = new MySqlCommand(cmdSql))
            {

                using (MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd))
                {

                    while (reader.Read())
                    {

                        int id = reader.GetInt32("Id");

                        string nome = ValidarCampo(reader["Nome"]);
                       
                        string telefone = ValidarCampo(reader["Telefone"]);



                        Marca marca = new(id, nome, telefone);


                        listaMarca.Add(marca);
                    }
                }

                return listaMarca;
            }
        }

        public static Fornecedor? ListarPorID(int id)
        {
            string sql = "SELECT * FROM `Fornecedor` WHERE `id` = @Id";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd);
                Fornecedor? fornecedor = null;

                while (reader.Read())
                {

                    int idBanco = reader.GetInt32("Id");

                    string nome = ValidarCampo(reader["Nome"]);
                    string cnpj = ValidarCampo(reader["CNPJ"]);
                    string telefone = ValidarCampo(reader["Telefone"]);



                    fornecedor = new Fornecedor(idBanco, nome, cnpj, telefone);


                }
                return fornecedor;
            }
        }


        public static int Atualizar(Fornecedor fornecedor)
        {

            string sql = "UPDATE `Fornecedor` SET `Nome` = @Nome, `Cnpj` = @CNPJ, `Telefone` = @Telefone WHERE `Id` = @Id";

            using (var cmd = new MySqlCommand(sql))
            {

                cmd.Parameters.AddWithValue("@Nome", fornecedor.Nome);
                cmd.Parameters.AddWithValue("@CPF", fornecedor.Cnpj);
                cmd.Parameters.AddWithValue("@Telefone", fornecedor.Telefone);



                cmd.Parameters.AddWithValue("@Id", fornecedor.id);


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
}
