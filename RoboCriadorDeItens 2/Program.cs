using System;

namespace RoboCriadorDeItens_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Deseja Cria ou Importar? C/I");
            string resposta = Console.ReadLine().ToLower();
            bool start = true;
            while (start)
            {
                switch (resposta)
                {
                    case "c":
                        RoboDeCriacao.criacao();
                        start = false;
                        break;

                    case "i":
                        RoboDeImportacao.importacao();
                        start = false;
                        break;
                    default:
                        Console.WriteLine("Digite uma alternativa válida!");
                        start = true;
                        resposta = Console.ReadLine().ToLower();
                        break;
                }
            }
            Console.WriteLine("Sucesso!!!");
            Console.ReadLine();
        }
    }
}
