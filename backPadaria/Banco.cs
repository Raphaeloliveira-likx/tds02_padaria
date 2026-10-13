using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria
{
    public static class Banco
    {

        private static string stringDeConexao = "Server=10.91.45.55;Port=3306;Database=padaria_bd;Uid=Likx;Pwd=123456";

        public static void connectAndTeste()
        {
            using (var connection = new MySqlConnection(stringDeConexao))
            {
                // try - tentar 
                try
                {
                    Console.WriteLine("Tentando conectar ao banco de dados");
                    connection.Open();

                    Console.WriteLine("Conexão be, sucedida");

                    using(var command = new MySqlCommand("SELECT VERSION();", connection))
                    {
                        string? version = command.ExecuteScalar()?.ToString();
                        Console.WriteLine($"Versão do MariaDB {version}");
                    }

                }catch (MySqlException ex)
                {
                    Console.WriteLine("Erro ao conectar no banco de dados");
                    Console.WriteLine(ex.Message);
                }

            }
        }
    }
}
