using backPadaria.Model;
using MySqlConnector;
using System;
using BCrypt.Net;

namespace backPadaria.Model
{
    internal class LoginFuncionario
    {
        public int Id { get; set; }
        public int FuncionarioId { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Perfil { get; set; }

        public LoginFuncionario(int funcionarioId, string login, string senhaTextoLimpo, string perfil)
        {
            if (funcionarioId <= 0) throw new Exception("Funcionário inválido.");
            if (string.IsNullOrWhiteSpace(login)) throw new Exception("O login não pode ser vazio.");
            if (string.IsNullOrWhiteSpace(senhaTextoLimpo)) throw new Exception("A palavra-passe não pode ser vazia.");
            if (string.IsNullOrWhiteSpace(perfil)) throw new Exception("O perfil não pode ser vazio.");

            this.FuncionarioId = funcionarioId;
            this.Login = login;
            this.Perfil = perfil;
            // Gera o hash automaticamente com BCrypt
            this.Senha = BCrypt.Net.BCrypt.HashPassword(senhaTextoLimpo);
        }

        public static int Inserir(LoginFuncionario loginFuncionario)
        {
            using (var cmd = new MySqlCommand("sp_InserirLogin"))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_FuncionarioId", loginFuncionario.FuncionarioId);
                cmd.Parameters.AddWithValue("p_Login", loginFuncionario.Login);
                cmd.Parameters.AddWithValue("p_Senha", loginFuncionario.Senha);
                cmd.Parameters.AddWithValue("p_Perfil", loginFuncionario.Perfil);

                return Banco.ExecutarComando(cmd);
            }
        }

        // Retorna uma Tupla contendo o Funcionário logado e o seu Perfil de acesso
        public static (Funcionario? Funcionario, string Perfil) Autenticar(string loginDigitado, string senhaTextoLimpo)
        {
            using (var cmd = new MySqlCommand("sp_FazerLogin"))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Login", loginDigitado);

                MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd);

                Funcionario? funcionarioLogado = null;
                string perfilDoBanco = string.Empty;
                string hashDoBanco = string.Empty;

                if (reader.Read())
                {
                    int id = reader.GetInt32("Id");
                    string nome = reader["Nome"].ToString();
                    string cpf = reader["CPF"].ToString();
                    string cargo = reader["Cargo"].ToString();

                    hashDoBanco = reader["senha"].ToString();
                    perfilDoBanco = reader["Perfil"].ToString();

                    // Valida a palavra-passe com o BCrypt
                    if (BCrypt.Net.BCrypt.Verify(senhaTextoLimpo, hashDoBanco))
                    {
                        funcionarioLogado = new Funcionario(id, nome, cpf, cargo);
                        return (funcionarioLogado, perfilDoBanco);
                    }
                }

                return (null, string.Empty);
            }
        }
    }
}