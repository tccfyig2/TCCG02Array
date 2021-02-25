using Microsoft.Xrm.Sdk;
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
        class RoboDeImportacao
    {
            public static int quantidade = 10;
            static void import(string[] args)
            {

                Conexao conexao = new Conexao();

                //CRM de Origem
                var serviceProxyOrigem = conexao.ObterConexaoOrigem();
                
                var serviceProxyDestino = conexao.ObterConexaoDestino();

                var contas = RetornarMultiplo(serviceProxyOrigem);
                migration(serviceProxyOrigem, serviceProxyDestino, contas);

                Console.WriteLine("Criado com Sucesso!");
                Console.ReadLine();
            }
            
            static EntityCollection RetornarMultiplo(CrmServiceClient serviceProxyOrigem)
            {
                QueryExpression queryExpression = new QueryExpression("account");

                //queryExpression.Criteria.AddCondition("name", ConditionOperator.Equal, "teste");
                queryExpression.ColumnSet = new ColumnSet(true);
                EntityCollection colecaoEntidades = serviceProxyOrigem.RetrieveMultiple(queryExpression);
                foreach (var item in colecaoEntidades.Entities)
                {
                    Console.WriteLine(item["name"]);
                }

                return colecaoEntidades;
            }

            static void migration(CrmServiceClient serviceProxyOrigem, CrmServiceClient serviceProxyDestino, EntityCollection contas)
            {
                int i = 0;
                foreach (var conta in contas.Entities)
                {
                    var c = new Entity("account");
                    c.Attributes.Add("name", conta["name"].ToString() + i);
                    c.Attributes.Add("name", conta["name"].ToString() + i);
                    c.Attributes.Add("name", conta["name"].ToString() + i);
                    serviceProxyDestino.Create(c);
                    i++;
                }
            }
        }
    }

