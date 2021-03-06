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
                Console.WriteLine("Deseja Criar, Importar, Plugin ou Sair? (C / I / P / S)");
                string resposta = Console.ReadLine().ToLower();
                Console.Clear();
                switch (resposta)
                {
                    case "c":
                        RoboDeCriacao.Criacao();
                        Console.WriteLine("Sucesso!");
                        break;
                    case "i":
                        RoboDeImportacao.Importacao();
                        Console.WriteLine("Sucesso!");
                        break;
                    case "p":
                        TestePlugin.Plugin();
                        Console.WriteLine("Sucesso!!");
                        break;
                    case "s":
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