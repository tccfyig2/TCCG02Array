using System;
using System.Diagnostics;

namespace RoboCriadorDeItens_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch cronometro = new Stopwatch();
            cronometro.Start();

            RoboDeCriacao.criacao();
            //RoboDeImportacao.importacao();
            //Console.WriteLine("Sucesso!!!");
            cronometro.Stop();
            Console.WriteLine($"O processo foi concluido após {cronometro.Elapsed.Minutes} minutos e {cronometro.Elapsed.Seconds} segundos");
            Console.ReadLine();
        }
    }
}
