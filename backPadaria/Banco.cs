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

        // Declaração do método: público e estático, projetado especificamente para executar comandos 
         // (geralmente INSERTs seguidos de SELECT LAST_INSERT_ID()) e retornar o ID gerado pelo banco.
public static int executarComandoComRetornoId(MySqlCommand command)
        {

            using (var conexao = new MySqlConnection(stringDeConexao))
            {
                command.Connection = conexao;

                try
                {
                    conexao.Open();

                    /* O ExecuteScalar executa o comando e retorna APENAS a primeira coluna da primeira 
                     linha do resultado. É perfeito para pegar um único valor (como um ID recém-gerado).
                     Ele retorna um tipo 'object' porque não sabe de antemão qual o tipo do dado no banco.
                    */
                    object objRetorno = command.ExecuteScalar();

                    int retorno = 0;

                    /*  Verificação de segurança: Checa se o banco realmente retornou algo.
                        Verifica tanto se o objeto C# não é nulo (null) quanto se
                        não é um nulo do próprio banco de dados (DBNull.Value).
                    */
                    if (objRetorno != null && objRetorno != DBNull.Value)
                    {

                        /* Se passou na verificação, converte o objeto (que veio como texto/objeto)
                            para um número inteiro (int). 
                        */

                        retorno = int.Parse(objRetorno.ToString());
                    }

                    return retorno;
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Erro de banco de dados{ex.Message}");
                    return -1;
                }

            }

        } // Método central para executar comandos de consultas (SELECT) no banco de dados.

        // Ele retorna um MySqlDataReader, que é uma espécie de "ponteiro" para ler os dados linha por linha.

        public static MySqlDataReader executarComRetornoDeDados(MySqlCommand command)
        {


            //// Cria a conexão com o banco.
            // ATENÇÃO: Aqui NÃO usamos o bloco "using" para a conexão.
            // Se usássemos, a conexão seria fechada antes de retornar os dados,
            // e o programa quebraria ao tentar ler a tabela.
            var conexao = new MySqlConnection(stringDeConexao);

        command.Connection = conexao;

try
{
    // Abre a porta de comunicação com o MySQL
    conexao.Open();

    // Executa a consulta e retorna um "leitor" de dados.
    // O CommandBehavior.CloseConnection é a peça-chave deste método:
    // ele avisa ao leitor que assim que ele (o leitor) for fechado ou
    // descartado lá na tela/classe que o chamou,
    // a conexão com o banco deve ser fechada automaticamente junto com ele.
    return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
}
catch (MySqlException ex)
{
        //O Dispose() ja executa o close() e libera a memória.
        conexao.Dispose();

        Console.WriteLine($"Erro na Leitura do banco de dados: ex.Message");

        // Ao inves de retor null, relança o erro (throw) para que o método que 
        // chamou este ExecuteReader saiba que a pesquisa falhou e possa tratar a tela do usuario.
        throw;

       }
    }
  }
}