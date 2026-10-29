using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Model
{
    internal class ItemCompra
    {
        private int id;
        private int compraId;
        private int produtoId;
        private int quantidade;
        private decimal precoCobrado;

        public int Id { get => id; set => id = value; }
        public int CompraId { get => compraId; set => compraId = value; }
        public int ProdutoId { get => produtoId; set => produtoId = value; }
        public int Quantidade { get => quantidade; set => quantidade = value; }
        public decimal PrecoCobrado { get => precoCobrado; set => precoCobrado = value; }

        // Construtor base para validação e criação de um novo item em memória
        public ItemCompra(int produtoId, int quantidade, decimal precoCobrado)
        {
            if (produtoId <= 0) throw new ArgumentOutOfRangeException("Produto Inválido");
            if (quantidade <= 0) throw new ArgumentOutOfRangeException("A quantidade deve ser maior que 0");
            if (precoCobrado <= 0) throw new ArgumentOutOfRangeException("O preço cobrado deve ser maior que 0");

            this.ProdutoId = produtoId;
            this.Quantidade = quantidade;
            this.PrecoCobrado = precoCobrado;
        }

        // Construtor completo (chamado ao ler da base de dados)
        public ItemCompra(int id, int compraId, int produtoId, int quantidade, decimal precoCobrado)
            : this(produtoId, quantidade, precoCobrado)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException("Id do item é inválido");
            if (compraId <= 0) throw new ArgumentOutOfRangeException("Id da compra inválido");

            this.Id = id;
            this.CompraId = compraId;
        }

        public decimal CalcularSubtotal()
        {
            return this.Quantidade * this.PrecoCobrado;
        }

        public static int Inserir(ItemCompra item)
        {
            using (var cmd = new MySqlCommand("sp_InserirItemCompra"))
            {
                // Configura o comando como StoredProcedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_CompraID", item.CompraId);
                cmd.Parameters.AddWithValue("p_ProdutoID", item.ProdutoId);
                cmd.Parameters.AddWithValue("p_Quantidade", item.Quantidade);
                cmd.Parameters.AddWithValue("p_PrecoCobrado", item.PrecoCobrado);

                return Banco.ExecutarComando(cmd);
            }

        }
    }
}