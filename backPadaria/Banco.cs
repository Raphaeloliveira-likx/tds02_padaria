using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Banco
{
    private static string stringConnection = "Server=10.91.45.55;Port=3306;Database=padaria_bd;Uid=admin;Pwd=12345678";

    public static void connectAndTest()
    {
        using (var connection = new MySqlConnection(stringConnection))
        {
            try
            {
                Console.WriteLine("Tentando conectar ao banco de dados...");
                connection.Open();

                using (var command = new MySqlCommand(
                    "SELECT VERSION();", connection))
                {
                    string? version = command.ExecuteScalar()?.ToString();
                    Console.WriteLine($"Versão do MariaDB: {version}");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao conectar no banco de dados!");
                Console.WriteLine(ex.Message);
            }
        }
    }

    //Declaração do método: público  e estático, retorna um número inteiro (linhas afetadas) e recebe um objeto MySqlCommand com a instrução SQL já configurada como parâmetro
    public static int ExecutarComando(MySqlCommand cmd)
    {
        //O bloco using garante que a coneção será automaticamente fechada e liberada da memória ao final do bloco, mesmo que ocorram erros no meio do processo.

        //Cria a conexão utilizando a variavel de ambiente
        using (var conexao = new MySqlConnection(stringConnection))
        {
            //Vinculação: Diz ao comando SQL qm qual conexão ele deve ser executado.
            cmd.Connection = conexao;

            //Tratamento de erros: Inicia um bloco de tentativa para proteger o programa de falhas abruptas.
            try
            {
                // Abertura da conexão: Estabelece efetivamente a comunicação com o servidor Mysql
                conexao.Open();

                // Executa o comando SQL (Insert, Update ou Delete) no banco de dados.
                // Retorna um número inteiro correspondente ao total de linhas que foram modificadas.
                return cmd.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro no Banco de dados: {ex.Message}");
                return -1;
            }
        }
    } // Metodo RetornoComId

    public static int ExecutarComandoComRetornoId(MySqlCommand cmd)
    {
        using (var conexao = new MySqlConnection(stringConnection))
        {
            cmd.Connection = conexao;

            try
            {
                conexao.Open();
                // O ExecuteScalar executa o comando e retorna APENAS a primeira coluna da primeira linha do resultado. E perfeito para pegar um unico valor (como um Id recem-gerado). Ele retorna um tipo obj porque nao sabe de antemao qual o tipo do dado do banco.
                object objReturno = cmd.ExecuteScalar();

                int retorno = 0;

                // Verificação de segurança: Checa se o banco realmete retornou algo.
                // Verifica tanto se o obj C# não é nulo (null) quanto se não é um nulo do proprio banco de dados (DbNull.Value).

                if (objReturno != null && objReturno != DBNull.Value)
                {

                    // Se passou na verificação, converte o obj (que veio como texto/objeto) para um numero inteiro (int)
                    retorno = int.Parse(objReturno.ToString());

                }

                return retorno;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro de banco de dados: {ex.Message}");
                return -1;
            }
        }
    }

    // Método central para executar comandos de consulta (SELECT) no banco de dados.
    // Ele retorna um MySqlDataReader, que é uma especie de ponteiro para ler os dados linha por linha
    public static MySqlDataReader ExecutarComRetornoDeDados(MySqlCommand cmd)
    {
        var conexao = new MySqlConnection(stringConnection);

        try
        {
            conexao.Open();

            cmd.Connection = conexao;

            // Executa a consulta e retorna o "leitor" de dados.
            // O CommandBehavior. CloseConnection e a peça-chave deste método: ele avisa ao leitor
            // que assim que ele (o leitor) for fechado ou descartado la na tela/classe que o chamou,
            // a conexao com o banco deve ser fechada automaticamente junto com ele.
            return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }
        catch (MySqlException ex)
        {
            // O Dispose() já executa o Close() e libera a memória.
            conexao.Dispose();

            Console.WriteLine($"Erro na leitura do banco de dados: {ex.Message}");

            // Ao inves de retornar null, relanca o erro (throw) para que o metodo que
            // chamou este ExecuteReader saiba que a pesquisa falhou e possa tratar a tela do usuário.
            throw;
        }
    }

} // class