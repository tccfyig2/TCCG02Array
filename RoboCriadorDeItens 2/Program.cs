﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using RoboCriadorDeItens_2.Geradores;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Client;

namespace RoboCriadorDeItens_2
{
    class Program
    {
        public static int quantidade = 1;
        static void Main(string[] args)
        {
            RoboDeCriacao.criacao();
            Console.WriteLine("Criado com Sucesso!");
            Console.ReadLine();
        }
    }
}