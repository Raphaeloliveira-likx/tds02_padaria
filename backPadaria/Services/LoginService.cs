using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using backPadaria.Model;
using System;

namespace backPadaria.Service
{
    internal static class LoginService
    {
        // Retorna o utilizador logado e o perfil para que o menu principal saiba quais permissões conceder
        public static (Funcionario? Funcionario, string Perfil) FazerLoginMenu()
        {
            Console.Clear();
            Console.WriteLine("===== SISTEMA DE AUTENTICAÇÃO =====");

            Console.Write("Login: ");
            string login = Console.ReadLine();

            Console.Write("Palavra-passe: ");
            string senha = LerSenhaOculta();

            try
            {
                // Chama o Model para validar as credenciais e o Hash do BCrypt
                var resultado = LoginFuncionario.Autenticar(login, senha);

                if (resultado.Funcionario != null)
                {
                    Console.WriteLine($"\n\nLogin efetuado com sucesso!");
                    Console.WriteLine($"Bem-vindo(a), {resultado.Funcionario.Nome} | Perfil: {resultado.Perfil}");
                    Console.WriteLine("\nPressione uma tecla para aceder ao sistema...");
                    Console.ReadKey();

                    return resultado;
                }
                else
                {
                    Console.WriteLine("\n\nLogin ou palavra-passe incorretos. Acesso negado.");
                    Console.WriteLine("Pressione uma tecla para tentar novamente...");
                    Console.ReadKey();

                    return (null, string.Empty);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n\n[ERRO DE AUTENTICAÇÃO]: {ex.Message}");
                Console.ReadKey();
                return (null, string.Empty);
            }
        }

        // Método auxiliar para ler a palavra-passe sem exibir os caracteres no ecrã
        private static string LerSenhaOculta()
        {
            string senha = "";
            while (true)
            {
                ConsoleKeyInfo tecla = Console.ReadKey(true); // O 'true' impede que a tecla seja exibida

                if (tecla.Key == ConsoleKey.Enter)
                {
                    break; // Termina a leitura ao pressionar Enter
                }
                else if (tecla.Key == ConsoleKey.Backspace)
                {
                    if (senha.Length > 0)
                    {
                        senha = senha.Substring(0, senha.Length - 1);
                        Console.Write("\b \b"); // Apaga o asterisco da consola
                    }
                }
                else if (tecla.KeyChar != '\u0000') // Verifica se é um caractere válido
                {
                    senha += tecla.KeyChar;
                    Console.Write("*"); // Exibe o asterisco no lugar do caractere
                }
            }
            return senha;
        }
    }
}