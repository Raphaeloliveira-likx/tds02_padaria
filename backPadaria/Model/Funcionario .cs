using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (string.IsNullOrWhiteSpace(nome)) throw new Exception("O nome do funcionario nao pode estar vazio");
            this.id = id;
            this.nome = nome;
            this.cpf = cpf;
            this.cargo = cargo;
        }

        public Funcionario(int id, string nome, string cpf, string cargo)
        {
            this.id = id;
            this.nome = nome;
            this.cpf = cpf;
            this.cargo = cargo;
        }
        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Cargo { get => cargo; set => cargo = value; }

        public static int Inserir(Funcionario funcionario)
        {
            string sql = "INSERT INTO `Funcionario` (`Nome`, `CPF`, `Cargo`) VALUES (@Nome, @CPF, @Cargo)";
            using (var cmd = new MySqlCommand(sql))
            {

                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@CPF", funcionario.Cpf);
                cmd.Parameters.AddWithValue("@Cargo", funcionario.cargo);

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
            string cmdSql = "SELECT * FROM `Funcionario`";

            List<Funcionario> listaFuncionario = new();

            using (var cmd = new MySqlCommand(cmdSql))
            {
                using (MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd))
                {

                    while (reader.Read())
                    {

                        int id = reader.GetInt32("Id");

                        string nome = ValidarCampo(reader["Nome"]);

                        string cpf = ValidarCampo(reader["CPF"]);
                        string cargo = ValidarCampo(reader["Cargo"]);



                        Funcionario funcionario = new Funcionario(id, nome, cpf, cargo);


                        listaFuncionario.Add(funcionario);
                    }
                }

                return listaFuncionario;
            }
        }
        public static Funcionario? ListarPorID(int id)
        {
            string sql = "SELECT * FROM `Funcionario` WHERE `id` = @ID";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd);
                Funcionario? funcionario = null;

                while (reader.Read())
                {
                    
                    int idBanco = reader.GetInt32("Id");

                    string nome = ValidarCampo(reader["Nome"]);
                    string cpf = ValidarCampo(reader["CPF"]);
                    string cargo = ValidarCampo(reader["Cargo"]);
                    
               

                   
                    funcionario = new Funcionario(idBanco, nome, cpf, cargo);


                }
                return funcionario;
            }
        }

        public static int Atualizar(Funcionario funcionario)
        {

            string sql = "UPDATE `Funcionario` SET `Nome` = @Nome, `Cpf` = @CPF, `Cargo` = @Cargo WHERE `Id` = @Id";

            using (var cmd = new MySqlCommand(sql))
            {
                
                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@CPF", funcionario.Cpf);
                cmd.Parameters.AddWithValue("@Cargo", funcionario.cargo);
              

                
                cmd.Parameters.AddWithValue("@Id", funcionario.id);

                
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

