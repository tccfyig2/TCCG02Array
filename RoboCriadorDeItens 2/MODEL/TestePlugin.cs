using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using RoboCriadorDeItens_2.DAL;
using System;

namespace RoboCriadorDeItens_2.MODEL
{
    class TestePlugin : Query
    {
        internal static void Plugin()
        {
            // Cria conexão
            Console.WriteLine("Conectando...");
            Conexao conexao = new Conexao();
            CrmServiceClient _serviceProxy = conexao.ObterConexaoApresentacao();
            Console.Clear();

            // Interface
            bool start = true;
            while (start)
            {
                Console.Write("O que deseja Testar?\n" +
                "\t1 - Plugin CPF Inválido\n" +
                "\t2 - Plugin CPF Duplicado\n" +
                "\t3 - Plugin CNPJ Inválido\n" +
                "\t4 - Plugin CNPJ Duplicado\n" +
                "\t5 - Plugin código Duplicado\n" +
                "\t6 - Voltar\n" +
                "Digite o número da opção: ");
                string resposta = Console.ReadLine().ToLower();
                Console.Clear();
                switch (resposta)
                {
                    case "1":
                        TesteDePlugin(_serviceProxy, PluginCPFInvalido());
                        break;
                    case "2":
                        TesteDePlugin(_serviceProxy, PluginCPFDuplicado());
                        break;
                    case "3":
                        TesteDePlugin(_serviceProxy, PluginCNPJInvalido());
                        break;
                    case "4":
                        TesteDePlugin(_serviceProxy, PluginCNPJDuplicado());
                        break;
                    case "5":
                        TesteDePlugin(_serviceProxy, PluginCodigoDuplicado());
                        break;
                    case "6":
                        start = false;
                        break;
                    default:
                        Console.WriteLine("Alternativa inválida!");
                        break;
                }
            }
        }
        static Entity PluginCPFInvalido()
        {
            Entity entidade = new Entity("contact");
            entidade.Attributes.Add("cred2_cpf", "38561393466");
            return entidade;
        }
        static Entity PluginCPFDuplicado()
        {
            Entity entidade = new Entity("contact");
            entidade.Attributes.Add("cred2_cpf", "38561393467");
            return entidade;
        }
        static Entity PluginCNPJInvalido()
        {
            Entity entidade = new Entity("account");
            entidade.Attributes.Add("cred2_cnpj", "70865518919501");
            return entidade;
        }
        static Entity PluginCNPJDuplicado()
        {
            Entity entidade = new Entity("account");
            entidade.Attributes.Add("cred2_cnpj", "70865518919509");
            return entidade;
        }
        static Entity PluginCodigoDuplicado()
        {
            Entity entidade = new Entity("salesorder");
            entidade.Attributes.Add("cred2_codigo", "cod-10000");
            return entidade;
        }
    }
}