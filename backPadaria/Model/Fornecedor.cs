using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Model
{
    using MySqlConnector;
    using System;
    using System.Collections.Generic;

    namespace backPadaria.Model
    {
        public class Funcionario
        {
            // ==========================================
            // 1. PROPRIEDADES (Baseadas no Diagrama)
            // ==========================================
            public int Id { get; set; }
            public string Nome { get; set; }
            public string CPF { get; set; }
            public string Cargo { get; set; }

            // ==========================================
            // 2. CONSTRUTORES
            // ==========================================

            // Construtor vazio (Útil se no futuro decidir usar o Dapper)
            public Funcionario() { }

            // Construtor sem ID (Usado para INSERIR novos funcionários, pois o ID é gerado pela base de dados)
            public Funcionario(string nome, string cpf, string cargo)
            {
                this.Nome = nome;
                this.CPF = cpf;
                this.Cargo = cargo;
            }

            // Construtor completo (Usado quando lemos os dados que já existem na base de dados)
            public Funcionario(int id, string nome, string cpf, string cargo)
            {
                this.Id = id;
                this.Nome = nome;
                this.CPF = cpf;
                this.Cargo = cargo;
            }

            // ==========================================
            // 3. MÉTODOS DE BASE DE DADOS (CRUD)
            // ==========================================

            public static int Inserir(Funcionario funcionario)
            {
                string sql = "INSERT INTO `Funcionario` (`Nome`, `CPF`, `Cargo`) VALUES (@Nome, @CPF, @Cargo);";

                using (var cmd = new MySqlCommand(sql))
                {
                    cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                    cmd.Parameters.AddWithValue("@CPF", funcionario.CPF);
                    cmd.Parameters.AddWithValue("@Cargo", funcionario.Cargo);

                    return Banco.ExecutarComando(cmd);
                }
            }

            public static int Atualizar(Funcionario funcionario)
            {
                string sql = @"UPDATE `Funcionario` 
                           SET `Nome` = @Nome, 
                               `CPF` = @CPF, 
                               `Cargo` = @Cargo 
                           WHERE `Id` = @Id;";

                using (var cmd = new MySqlCommand(sql))
                {
                    cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                    cmd.Parameters.AddWithValue("@CPF", funcionario.CPF);
                    cmd.Parameters.AddWithValue("@Cargo", funcionario.Cargo);
                    cmd.Parameters.AddWithValue("@Id", funcionario.Id);

                    return Banco.ExecutarComando(cmd);
                }
            }

            public static int Deletar(int idFuncionario)
            {
                string sql = "DELETE FROM `Funcionario` WHERE `Id` = @Id;";

                using (var cmd = new MySqlCommand(sql))
                {
                    cmd.Parameters.AddWithValue("@Id", idFuncionario);
                    return Banco.ExecutarComando(cmd);
                }
            }

            public static List<Funcionario> ListarTodos()
            {
                string sql = "SELECT * FROM `Funcionario`";
                List<Funcionario> listaFuncionarios = new List<Funcionario>();

                using (var cmd = new MySqlCommand(sql))
                {
                    using (MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd))
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("Id");
                            string nome = validarCampo(reader["Nome"]);
                            string cpf = validarCampo(reader["CPF"]);
                            string cargo = validarCampo(reader["Cargo"]);

                            Funcionario funcionario = new Funcionario(id, nome, cpf, cargo);
                            listaFuncionarios.Add(funcionario);
                        }
                    }
                    return listaFuncionarios;
                }
            }

            public static Funcionario BuscarPorCpf(string cpfProcura)
            {
                string sql = "SELECT * FROM `Funcionario` WHERE `CPF` = @CPF LIMIT 1;";

                using (var cmd = new MySqlCommand(sql))
                {
                    cmd.Parameters.AddWithValue("@CPF", cpfProcura);

                    using (MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd))
                    {
                        Funcionario funcionario = null;

                        if (reader.Read())
                        {
                            int id = reader.GetInt32("Id");
                            string nome = validarCampo(reader["Nome"]);
                            string cpf = validarCampo(reader["CPF"]);
                            string cargo = validarCampo(reader["Cargo"]);

                            funcionario = new Funcionario(id, nome, cpf, cargo);
                        }

                        return funcionario;
                    }
                }
            }

            // ==========================================
            // 4. MÉTODOS AUXILIARES
            // ==========================================

            // Método estático para garantir que não ocorrem quebras (crashes) se houver valores nulos na base de dados
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
}
