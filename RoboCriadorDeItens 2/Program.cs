using RoboCriadorDeItens_2.MODEL;
using System;

namespace RoboCriadorDeItens_2
{
    class Program
    {
        static void Main(string[] args)
        {
            bool start = true;
            while (start)
            {
                Console.Write("1- Criar\n2- Importar\n3- Plugin\n4- Sair\nDigite o número da opção: ");
                string resposta = Console.ReadLine().ToLower();
                Console.Clear();
                switch (resposta)
                {
                    case "1":
                        RoboDeCriacao.Criacao();
                        Console.WriteLine("Sucesso!");
                        break;
                    case "2":
                        RoboDeImportacao.Importacao();
                        Console.WriteLine("Sucesso!");
                        break;
                    case "3":
                        TestePlugin.Plugin();
                        Console.WriteLine("Sucesso!!");
                        break;
                    case "4":
                        start = false;
                        break;
                    default:
                        Console.WriteLine("Alternativa inválida!");
                        break;
                }
            }
            Console.WriteLine("FIM!");
        }
    }
}