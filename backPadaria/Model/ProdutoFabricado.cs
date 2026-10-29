using MySqlConnector;
using System;
using System.Collections.Generic;
using backPadaria.Model;


namespace backPadaria.Model
{
    // A classe herda de Produto (Especialização do diagrama)
    internal class ProdutoFabricado : Produto
    {
        private int produtoId;
        private string descricaoProducao;

        // Construtor completo: Recebe os atributos do Pai (Produto) e do Filho (ProdutoFabricado)
        // O "base(...)" envia os dados correspondentes para o construtor da classe Produto
        public ProdutoFabricado(int id, string nome, decimal preco, string categoria, bool fabricado, int marcaId, int produtoId, string descricaoProducao)
            : base(id, nome, preco, categoria, fabricado, marcaId)
        {
            if (string.IsNullOrWhiteSpace(descricaoProducao))
                throw new Exception("A descrição de produção não pode ser vazia");

            this.ProdutoId = produtoId;
            this.DescricaoProducao = descricaoProducao;
        }

        // Construtor apenas com os dados do filho (útil para inserções onde o Produto já existe)
        public ProdutoFabricado(int produtoId, string descricaoProducao) : base(produtoId)
        {
            this.ProdutoId = produtoId;
            this.DescricaoProducao = descricaoProducao;
        }

        public int ProdutoId { get => produtoId; set => produtoId = value; }
        public string DescricaoProducao { get => descricaoProducao; set => descricaoProducao = value; }

        public static int Inserir(ProdutoFabricado produtoFabricado)
        {
            // Nota: Este método assume que o Produto já foi criado na tabela 'Produto'
            // e estamos apenas a registar a sua especialização na tabela 'ProdutoFabricado'.
            string sql = "INSERT INTO `ProdutoFabricado` (`ProdutoId`, `DescricaoProducao`) VALUES (@ProdutoId, @DescricaoProducao)";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@ProdutoId", produtoFabricado.ProdutoId);
                cmd.Parameters.AddWithValue("@DescricaoProducao", produtoFabricado.DescricaoProducao);

                return Banco.ExecutarComando(cmd);
            }
        }

        public static int Deletar(ProdutoFabricado produtoFabricado)
        {
            // Apaga apenas o registo de fabrico (a especialização)
            string sql = "DELETE FROM `ProdutoFabricado` WHERE `ProdutoId` = @ProdutoId";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@ProdutoId", produtoFabricado.ProdutoId);
                return Banco.ExecutarComando(cmd);
            }
        }

        public static List<ProdutoFabricado> ListarTodos()
        {
            // O INNER JOIN junta as propriedades da classe Pai (Produto) com a classe Filho (ProdutoFabricado)
            string sql = @"
                SELECT 
                    p.Id, p.Nome, p.Preco, p.Categoria, p.Fabricado, p.MarcaId,
                    pf.ProdutoId, pf.DescricaoProducao
                FROM 
                    ProdutoFabricado pf
                INNER JOIN 
                    Produto p ON pf.ProdutoId = p.Id";

            List<ProdutoFabricado> listaFabricados = new();

            using (var cmd = new MySqlCommand(sql))
            {
                using (MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd))
                {
                    while (reader.Read())
                    {
                        // Extrai dados do Produto (Pai)
                        int id = reader.GetInt32("Id");
                        string nome = validarCampo(reader["Nome"]);
                        decimal preco = Convert.ToDecimal(reader["Preco"]);
                        string categoria = validarCampo(reader["Categoria"]);
                        bool fabricado = Convert.ToBoolean(reader["Fabricado"]);
                        int marcaId = Convert.ToInt32(reader["MarcaId"]);

                        // Extrai dados do ProdutoFabricado (Filho)
                        int produtoId = reader.GetInt32("ProdutoId");
                        string descricaoProducao = validarCampo(reader["DescricaoProducao"]);

                        // Cria o objeto completo
                        ProdutoFabricado prodFabricado = new ProdutoFabricado(id, nome, preco, categoria, fabricado, marcaId, produtoId, descricaoProducao);
                        listaFabricados.Add(prodFabricado);
                    }
                }
                return listaFabricados;
            }
        }

        public static ProdutoFabricado? listarPorId(int produtoIdPesquisa)
        {
            string sql = @"
                SELECT 
                    p.Id, p.Nome, p.Preco, p.Categoria, p.Fabricado, p.MarcaId,
                    pf.ProdutoId, pf.DescricaoProducao
                FROM 
                    ProdutoFabricado pf
                INNER JOIN 
                    Produto p ON pf.ProdutoId = p.Id
                WHERE 
                    pf.ProdutoId = @ProdutoId";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@ProdutoId", produtoIdPesquisa);
                MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd);

                ProdutoFabricado? prodFabricado = null;

                while (reader.Read())
                {
                    int id = reader.GetInt32("Id");
                    string nome = validarCampo(reader["Nome"]);
                    decimal preco = Convert.ToDecimal(reader["Preco"]);
                    string categoria = validarCampo(reader["Categoria"]);
                    bool fabricado = Convert.ToBoolean(reader["Fabricado"]);
                    int marcaId = Convert.ToInt32(reader["MarcaId"]);

                    int produtoId = reader.GetInt32("ProdutoId");
                    string descricaoProducao = validarCampo(reader["DescricaoProducao"]);

                    prodFabricado = new ProdutoFabricado(id, nome, preco, categoria, fabricado, marcaId, produtoId, descricaoProducao);
                }
                return prodFabricado;
            }
        }

        public static int Atualizar(ProdutoFabricado produtoFabricado)
        {
            // Atualiza apenas os dados específicos da especialização
            string sql = "UPDATE `ProdutoFabricado` SET `DescricaoProducao` = @DescricaoProducao WHERE `ProdutoId` = @ProdutoId";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@DescricaoProducao", produtoFabricado.DescricaoProducao);
                cmd.Parameters.AddWithValue("@ProdutoId", produtoFabricado.ProdutoId);

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