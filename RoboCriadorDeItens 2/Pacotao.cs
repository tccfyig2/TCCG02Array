using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;

namespace RoboCriadorDeItens_2
{
    class Pacotao
    {
        internal static void pacote()
        {
            try
            {
                Conexao conexao = new Conexao();

                var serviceProxyOrigem = conexao.ObterConexaoCobaia();

                IOrganizationService service;
                service = (IOrganizationService)serviceProxyOrigem.OrganizationWebProxyClient != null ? (IOrganizationService)serviceProxyOrigem.OrganizationWebProxyClient : (IOrganizationService)serviceProxyOrigem.OrganizationServiceProxy;

                var request = new ExecuteMultipleRequest()
                {
                    Requests = new OrganizationRequestCollection(),
                    Settings = new ExecuteMultipleSettings
                    {
                        ContinueOnError = false,
                        ReturnResponses = true
                    }
                };

                for (int i = 0; i < 5; i++)
                {
                    Entity tabela = new Entity("account");
                    tabela["name"] = "Bob " + i.ToString();

                    var createRequest = new CreateRequest()
                    {
                        Target = tabela
                    };
                    request.Requests.Add(createRequest);
                }

                Console.WriteLine("Before execute");
                var response = (ExecuteMultipleResponse)service.Execute(request);
                Console.WriteLine("After execute");

                foreach (var r in response.Responses)
                {
                    if (r.Response != null)
                        Console.WriteLine("Success");
                    else if (r.Fault != null)
                        Console.WriteLine(r.Fault);
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
