using backPadaria.Model;

namespace backPadaria
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Digite o nome do cliente: ");
            string nomeCliente = Console.ReadLine();

            Console.WriteLine("Digite o CPF do cliente: ");
            string cpfCliente = Console.ReadLine();

            Console.WriteLine("Digite o Telefone do cliente: ");
            string telefoneCliente = Console.ReadLine();

            Console.WriteLine("Digite o Endereço do cliente: ");
            string enderecoCliente = Console.ReadLine();

            Console.WriteLine("Digite os pontos do cliente: ");
            string pontosCliente = Console.ReadLine();

            Console.WriteLine();
            //Refatoração 
            Cliente cliente1 = new Cliente(
                nome: "Raphael Oliveira",
                cpf: "42684265426",
                telefone: "11539856247521",
                endereco: "Rua do Senac",
                pontos: 10
                );

            Cliente.Inserir(cliente1);
        }
    }
}
