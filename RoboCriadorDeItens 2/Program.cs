using System;

namespace RoboCriadorDeItens_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Deseja Criar, Importar ou Sair? (C / I / S)");
            string resposta = Console.ReadLine().ToLower();
            bool start = true;
            while (start)
            {
                switch (resposta)
                {
                    case "c":
                        RoboDeCriacao.criacao();
                        Console.WriteLine("Sucesso!!!");
                        Console.WriteLine("Deseja Criar, Importar ou Sair? (C / I / S)");
                        resposta = Console.ReadLine().ToLower();
                        Console.Clear();
                        break;
                    case "i":
                        RoboDeImportacao.importacao();
                        Console.WriteLine("Sucesso!!!");
                        Console.WriteLine("Deseja Criar, Importar ou Sair? (C / I / S)");
                        resposta = Console.ReadLine().ToLower();
                        Console.Clear();
                        break;
                    case "s":
                        start = false;
                        break;
                    default:
                        Console.WriteLine("Digite uma alternativa válida!");
                        resposta = Console.ReadLine().ToLower();
                        Console.Clear();
                        break;
                }
            }
        }
    }
}
