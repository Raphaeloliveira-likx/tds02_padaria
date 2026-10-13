using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Model
{
    internal class Cliente
    {
        private int id;
        private string name;
        private string cpf;
        private string telefone;
        private string endereco;
        private int pontos;

        public Cliente(string name, 
            string cpf, 
            string telefone, 
            string endereco, 
            int pontos)
        {
            this.name = name;
            this.cpf = cpf;
            this.telefone = telefone;
            this.endereco = endereco;
            this.pontos = pontos;
        }

        public Cliente(int id,
            string name, 
            string cpf, 
            string telefone, 
            string endereco, 
            int pontos)
        {
            this.id = id;
            this.name = name;
            this.cpf = cpf;
            this.telefone = telefone;
            this.endereco = endereco;
            this.pontos = pontos;
        }

        public void AdicionarPontos(int pontos)
        {
            this.pontos += pontos;
        }
    }
}
