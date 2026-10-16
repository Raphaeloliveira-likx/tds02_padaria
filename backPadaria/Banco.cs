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

                    using (var command = new MySqlCommand("SELECT VERSION();", connection))
                    {
                        string? version = command.ExecuteScalar()?.ToString();
                        Console.WriteLine($"Versão do MariaDB {version}");
                    }

                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Erro ao conectar no banco de dados");
                    Console.WriteLine(ex.Message);
                }

            }
        }

        //Declaração do metodo: publico e estatico, retorna um numero inteiro (linhas afetadas)
        // e receber um objeto MysqlCommand com a instrução SQL ja configurada como parametro
        public static int executarComando(MySqlCommand command)
        {
            /* O bloco'using' garante que a conexao sera automaticamente fechada e liberada
             * da memoria ao final do bloco , mesmo que ocorram erros no meio do processo.
             * 
             * cria uma conexao utilizando a variavel de ambiente
             */
            using (var conexao = new MySqlConnection(stringDeConexao))
            {
                //Vinculação: Diz ao comando SQL em qual conexao ele deve ser executado.
                command.Connection = conexao;

                try
                {
                    // Abertura da conexao: Estabelece efetivamente a comunicação com o servidor MySQL
                    conexao.Open();
                    // Execução: o ExecuteNoQuery dispara comandos de ação (INSERT, UPDATE, DELETE).
                    //ELE nao retorna registros, mas sim o numero inteiro de linhas que foram alteradas,
                    // e o 'return' devolve esse numero para quem o chamou o metodo.
                    return command.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Erro de Banco de dados {ex.Message}");

                    return -1;
                }
            }
        }
    }
}