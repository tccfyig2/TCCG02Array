using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;

namespace RoboCriadorDeItens_2.MODEL
{
    class TestePlugin
    {
        internal static void Plugin()
        {
            // Cria conexão!
            Conexao conexao = new Conexao();
            CrmServiceClient _serviceProxy = conexao.ObterConexaoApresentacao();

            // Start
            Console.Write("1 - Plugin CPF Invalido\n2 - Plugin CPF Duplicado\n3 - Plugin CNPJ Invalido\n4 - Plugin CNPJ Duplicado\nDigite o número da opção: ");
            string resposta = Console.ReadLine().ToLower();
            Console.Clear();
            switch (resposta)
            {
                case "1":
                    PluginCPFInvalido(_serviceProxy);
                    break;
                case "2":
                    PluginCPFDuplicado(_serviceProxy);
                    break;
                case "3":
                    PluginCNPJInvalido(_serviceProxy);
                    break;
                case "4":
                    PluginCNPJDuplicado(_serviceProxy);
                    break;
                default:
                    Console.WriteLine("Alternativa inválida!");
                    break;
            }
        }
        static void PluginCPFInvalido(CrmServiceClient _serviceProxy)
        {
            Entity entidade = new Entity("contact");
            entidade.Attributes.Add("cred2_cpf", "38561393466");
            _serviceProxy.Create(entidade);
        }
        static void PluginCPFDuplicado(CrmServiceClient _serviceProxy)
        {
            Entity entidade = new Entity("contact");
            entidade.Attributes.Add("cred2_cpf", "38561393467");
            _serviceProxy.Create(entidade);
        }
        static void PluginCNPJInvalido(CrmServiceClient _serviceProxy)
        {
            Entity entidade = new Entity("account");
            entidade.Attributes.Add("cred2_cnpj", "70865518919501");
            _serviceProxy.Create(entidade);
        }
        static void PluginCNPJDuplicado(CrmServiceClient _serviceProxy)
        {
            Entity entidade = new Entity("account");
            entidade.Attributes.Add("cred2_cnpj", "70865518919509");
            _serviceProxy.Create(entidade);
        }
    }
}
