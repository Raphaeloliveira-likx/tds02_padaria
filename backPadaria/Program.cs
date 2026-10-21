using backPadaria.Model;
using backPadaria.Services;
using MySqlConnector;
using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;

namespace backPadaria
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine("Digite o nome do cliente: ");
            //string nomeCliente = Console.ReadLine();

            //Console.WriteLine("Digite o endereço do cliente: ");
            //string enderecoCliente = Console.ReadLine();

            //Console.WriteLine("Digite o cpf do cliente: ");
            //string cpfCliente = Console.ReadLine();

            //Console.WriteLine("Digite o telefone do cliente: ");
            //string telefoneCliente = Console.ReadLine();

            //Console.WriteLine("Digite a quantidade de pontos");
            //int pontosCliente = int.Parse(Console.ReadLine());


            //Cliente cliente1 = new Cliente(
            //    nome: nomeCliente,
            //    cpf: cpfCliente,
            //    telefone: telefoneCliente,
            //    endereco: enderecoCliente,
            //    pontos: pontosCliente
            //);

            //Cliente clienteParaDeletar = new Cliente(5);

            //if (Cliente.Deletar(clienteParaDeletar) >= 1)
            //{
            //    Console.WriteLine("Cliente excluido com sucesso. Graças a Deus");
            //}
            //else
            //{
            //    Console.WriteLine("Deu bom não. Cliente presente!");
            //}

            //List<Cliente> clientes = Cliente.ListarTodos();

            //foreach(Cliente cliente in clientes)
            //{
            //    Console.WriteLine("-------------------");

            //    Console.WriteLine($"ID {cliente.Id}");
            //    Console.WriteLine($"Nome {cliente.Nome}");
            //    Console.WriteLine($"CPF {cliente.Cpf}");
            //    Console.WriteLine($"Telefone {cliente.Telefone}");
            //    Console.WriteLine($"Endereço {cliente.Endereco}");
            //    Console.WriteLine($"Pontos {cliente.Pontos}");

            //    Console.WriteLine("-------------------");
            //}

            //Cliente? cli01 = Cliente.listarPorId(7);

            //if(cli01 != null)
            //{
            //    Console.WriteLine("-------------------");


            //    Console.WriteLine($"ID {cli01.Id}");
            //    Console.WriteLine($"Nome {cli01.Nome}");
            //    Console.WriteLine($"CPF {cli01.Cpf}");
            //    Console.WriteLine($"Telefone {cli01.Telefone}");
            //    Console.WriteLine($"Endereço {cli01.Endereco}");
            //    Console.WriteLine($"Pontos {cli01.Pontos}");

            //    Console.WriteLine("-------------------");


            //}
            //else
            //{
            //    Console.WriteLine("O cliente não foi encontrado!!");
            //}



            //Cliente? meuCliente = Cliente.ListarPorID(2);

            //if (meuCliente != null)
            //{
            //    meuCliente.Endereco = "Rua Barão de Alagoas, 171";
            //    meuCliente.Telefone = "1197747-8585";
            //    int resultado = Cliente.Atualizar(meuCliente);

            //    if (resultado > 0)
            //    {
            //        Console.WriteLine("Cliente atualizado com sucesso");
            //    }


            //}

            //ClienteService.menuCliente();
            FuncionarioService.menuFuncionario();
        }
    }
}