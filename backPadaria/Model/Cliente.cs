using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Model
{
    internal class Cliente
    {
        private int id;
        private string nome;
        private string cpf;
        private string telefone;
        private string endereco;
        private int pontos;

        

        public Cliente(string nome, 
            string cpf, 
            string telefone, 
            string endereco, 
            int pontos)
        {

            if (string.IsNullOrWhiteSpace(nome)) throw new Exception("O nome do cliente nao pode estar vazio");
            this.Nome = nome;
            this.Cpf = cpf;
            this.Telefone = telefone;
            this.Endereco = endereco;
            this.Pontos = pontos;
        }

        

        public Cliente(int id,
            string nome, 
            string cpf, 
            string telefone, 
            string endereco, 
            int pontos)
        {
            this.Id = id;
            this.Nome = nome;
            this.Cpf = cpf;
            this.Telefone = telefone;
            this.Endereco = endereco;
            this.Pontos = pontos;
        }

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string Endereco { get => endereco; set => endereco = value; }
        public int Pontos { get => pontos; set => pontos = value; }

        public void AdicionarPontos(int pontos)
        {
            this.Pontos += pontos;
        }

        public int Inserir(Cliente cliente)
        {
            /* Instrução SQL: Define o comando de inserção (INSERT).
             * O uso de marcações com '@' (ex: @Nome) é uma pratica de segurança crucial chamada de "Parametrização".
             * 
             */
            string sql = "INSERT INTO 'Cliente' ('Nome','CPF','Telefone','Endereço','Pontos') Values (@Nome,@CPF,@Telefone,@Endereco,@Pontos)";

            using(var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@CPF", cliente.Cpf);
                cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                cmd.Parameters.AddWithValue("@Pontos", cliente.Pontos);


                return Banco.executarComando(cmd);

            }
        }
    }
}
