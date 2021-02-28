using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using RoboCriadorDeItens_2.Geradores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoboCriadorDeItens_2
{
    class RoboDeCriacao : listaId
    {
        protected static Random rnd = new Random();
        internal static void criacao()
        {
            Conexao conexao = new Conexao();
            CrmServiceClient _serviceProxy = conexao.ObterConexaoCobaia();
            //criaContato(_serviceProxy);
            //criaConta(_serviceProxy);
            //criaClientePotencial(_serviceProxy);
            //criaOrdem(_serviceProxy);
            produtoOrdem(_serviceProxy);
        }
        static void criaContato(CrmServiceClient _serviceProxy)
        {
            ExecuteMultipleRequest request = new ExecuteMultipleRequest()
            {
                Requests = new OrganizationRequestCollection(),
                Settings = new ExecuteMultipleSettings
                {
                    ContinueOnError = false,
                    ReturnResponses = true
                }
            };
            for (int i = 0; i < 50; i++)
            {
                Entity entidade = new Entity("contact");

                string[] endereco = (GeradorEndereco.geradorEndereco());
                entidade.Attributes.Add("address1_postalcode", endereco[0]);
                entidade.Attributes.Add("address1_line1", endereco[1]);
                entidade.Attributes.Add("address1_line2", endereco[2]);
                entidade.Attributes.Add("address1_line3", endereco[3]);
                entidade.Attributes.Add("address1_city", endereco[4]);
                entidade.Attributes.Add("address1_stateorprovince", endereco[5]);
                entidade.Attributes.Add("address1_country", "Brasil");

                string emailNome = GeradorNome_Sobrenome.geradorSobrenome();
                entidade.Attributes.Add("firstname", emailNome);
                entidade.Attributes.Add("emailaddress1", GeradorEmail.geradorEmail(emailNome));
                entidade.Attributes.Add("lastname", GeradorNome_Sobrenome.geradorSobrenome());
                entidade.Attributes.Add("crb79_cpf", GeradorCPF_CNPJ.geradorCPF()); // cred2_cpf = Apresentação// crb79_cpf = Cobaia
                entidade.Attributes.Add("mobilephone", GeradorTelefone_Topico.geredorTelefone(endereco[5]));

                CreateRequest createRequest = new CreateRequest()
                {
                    Target = entidade
                };
                request.Requests.Add(createRequest);
            }
            ExecuteMultipleResponse response = (ExecuteMultipleResponse)_serviceProxy.Execute(request);
            Console.WriteLine("Pacotão de contatos criado!");

            int cont = 0;
            foreach (var item in response.Responses)
            {
                if (item.Response != null)
                {
                    cont++;
                }
                else if (item.Fault != null)
                {
                    Console.WriteLine(item.Fault);
                }
            }
            Console.WriteLine($"Foram criados {cont} contatos com sucesso!");
        }
        static void criaConta(CrmServiceClient _serviceProxy)
        {
            ExecuteMultipleRequest request = new ExecuteMultipleRequest()
            {
                Requests = new OrganizationRequestCollection(),
                Settings = new ExecuteMultipleSettings
                {
                    ContinueOnError = false,
                    ReturnResponses = true
                }
            };
            List<listaId> contactId = new List<listaId>(GeradorId.BuscaId(_serviceProxy, tabela: "contact", campo: "contactid"));
            for (int i = 0; i < 50; i++)
            {
                int index = rnd.Next(0, contactId.Count);
                Entity entidade = new Entity("account");

                string[] endereco = (GeradorEndereco.geradorEndereco());
                entidade.Attributes.Add("address1_postalcode", endereco[0]);
                entidade.Attributes.Add("address1_line1", endereco[1]);
                entidade.Attributes.Add("address1_line2", endereco[2]);
                entidade.Attributes.Add("address1_line3", endereco[3]);
                entidade.Attributes.Add("address1_city", endereco[4]);
                entidade.Attributes.Add("address1_stateorprovince", endereco[5]);
                entidade.Attributes.Add("address1_country", "Brasil");

                entidade.Attributes.Add("primarycontactid", new EntityReference("contact", contactId[index].Id));
                string emailSobrenome = GeradorNome_Sobrenome.geradorSobrenome();
                entidade.Attributes.Add("emailaddress1", GeradorEmail.geradorEmail(emailSobrenome));
                entidade.Attributes.Add("name", emailSobrenome + " ltda.");
                entidade.Attributes.Add("crb79_cnpj", GeradorCPF_CNPJ.geradorCNPJ()); // cred2_cnpj = Apresentação// crb79_cnpj = Cobaia
                entidade.Attributes.Add("telephone1", GeradorTelefone_Topico.geredorTelefone(endereco[5]));

                CreateRequest createRequest = new CreateRequest()
                {
                    Target = entidade
                };
                request.Requests.Add(createRequest);
            }
            ExecuteMultipleResponse response = (ExecuteMultipleResponse)_serviceProxy.Execute(request);
            Console.WriteLine("Pacotão de contas criado!");

            int cont = 0;
            foreach (var item in response.Responses)
            {
                if (item.Response != null)
                {
                    cont++;
                }
                else if (item.Fault != null)
                {
                    Console.WriteLine(item.Fault);
                }
            }
            Console.WriteLine($"Foram criados {cont} contas com sucesso!");
        }
        static void criaClientePotencial(CrmServiceClient _serviceProxy)
        {
            ExecuteMultipleRequest request = new ExecuteMultipleRequest()
            {
                Requests = new OrganizationRequestCollection(),
                Settings = new ExecuteMultipleSettings
                {
                    ContinueOnError = false,
                    ReturnResponses = true
                }
            };
            for (int i = 0; i < 50; i++)
            {
                Entity entidade = new Entity("lead");

                string[] endereco = (GeradorEndereco.geradorEndereco());
                entidade.Attributes.Add("address1_postalcode", endereco[0]);
                entidade.Attributes.Add("address1_line1", endereco[1]);
                entidade.Attributes.Add("address1_line2", endereco[2]);
                entidade.Attributes.Add("address1_line3", endereco[3]);
                entidade.Attributes.Add("address1_city", endereco[4]);
                entidade.Attributes.Add("address1_stateorprovince", endereco[5]);
                entidade.Attributes.Add("address1_country", "Brasil");

                string emailNome = (GeradorNome_Sobrenome.geradorNome());
                entidade.Attributes.Add("emailaddress1", GeradorEmail.geradorEmail(emailNome));
                entidade.Attributes.Add("firstname", emailNome);
                string sobrenomeEmpresa = (GeradorNome_Sobrenome.geradorSobrenome());
                entidade.Attributes.Add("lastname", sobrenomeEmpresa);
                entidade.Attributes.Add("companyname", sobrenomeEmpresa + " ltda.");
                entidade.Attributes.Add("subject", GeradorTelefone_Topico.geredorTopico());
                entidade.Attributes.Add("telephone1", GeradorTelefone_Topico.geredorTelefone(endereco[5]));
                entidade.Attributes.Add("mobilephone", GeradorTelefone_Topico.geredorTelefone(endereco[5]));

                CreateRequest createRequest = new CreateRequest()
                {
                    Target = entidade
                };
                request.Requests.Add(createRequest);
            }
            ExecuteMultipleResponse response = (ExecuteMultipleResponse)_serviceProxy.Execute(request);
            Console.WriteLine("Pacotão de clientes potenciais criado!");

            int cont = 0;
            foreach (var item in response.Responses)
            {
                if (item.Response != null)
                {
                    cont++;
                }
                else if (item.Fault != null)
                {
                    Console.WriteLine(item.Fault);
                }
            }
            Console.WriteLine($"Foram criados {cont} clientes potenciais com sucesso!");
        }
        static void criaOrdem(CrmServiceClient _serviceProxy)
        {
            ExecuteMultipleRequest request = new ExecuteMultipleRequest()
            {
                Requests = new OrganizationRequestCollection(),
                Settings = new ExecuteMultipleSettings
                {
                    ContinueOnError = false,
                    ReturnResponses = true
                }
            };
            List<listaId> accountId = new List<listaId>(GeradorId.BuscaId(_serviceProxy, tabela: "account", campo: "accountid"));
            List<listaId> pricelevelId = new List<listaId>(GeradorId.BuscaId(_serviceProxy, tabela: "pricelevel", campo: "pricelevelid"));

            for (int i = 0; i < 50; i++)
            {
                int index = rnd.Next(0, accountId.Count);
                Entity entidade = new Entity("salesorder");

                entidade.Attributes.Add("name", $"Cliente nº: {rnd.Next(0,5000)}");
                entidade.Attributes.Add("customerid", new EntityReference("account", accountId[index].Id));
                entidade.Attributes.Add("pricelevelid", new EntityReference("pricelevel", new Guid("68f3a59e-f779-eb11-a812-00224836bdf5")));
                
                CreateRequest createRequest = new CreateRequest()
                {
                    Target = entidade
                };
                request.Requests.Add(createRequest);
            }
            ExecuteMultipleResponse response = (ExecuteMultipleResponse)_serviceProxy.Execute(request);
            Console.WriteLine("Pacotão de ordens criado!");

            int cont = 0;
            foreach (var item in response.Responses)
            {
                if (item.Response != null)
                {
                    cont++;
                }
                else if (item.Fault != null)
                {
                    Console.WriteLine(item.Fault);
                }
            }
            Console.WriteLine($"Foram criados {cont} Ordens com sucesso!");
        }
        static void produtoOrdem(CrmServiceClient _serviceProxy)
        {
            ExecuteMultipleRequest request = new ExecuteMultipleRequest()
            {
                Requests = new OrganizationRequestCollection(),
                Settings = new ExecuteMultipleSettings
                {
                    ContinueOnError = false,
                    ReturnResponses = true
                }
            };
            List<listaId> OrdemId = new List<listaId>(GeradorId.BuscaId(_serviceProxy, tabela: "salesorder", campo: "salesorderid"));

            Guid prduct = new Guid("2bdc8b88-ef79-eb11-a812-00224836bdf5");
            Guid uomid = new Guid("03a4fff9-216e-eb11-b1ab-000d3ac1779c");
            for (int i = 0; i < 50; i++)
            {
                int index = rnd.Next(0, OrdemId.Count);
                Entity entidade = new Entity("salesorderdetail");

                entidade.Attributes.Add("productid", new EntityReference("product", prduct));
                entidade.Attributes.Add("quantity", decimal.Parse(rnd.Next(0, 5).ToString()));
                entidade.Attributes.Add("salesorderid", new EntityReference("salesorder", OrdemId[index].Id));
                entidade.Attributes.Add("uomid", new EntityReference("businessunit", uomid));

                CreateRequest createRequest = new CreateRequest()
                {
                    Target = entidade
                };
                request.Requests.Add(createRequest);
            }
            ExecuteMultipleResponse response = (ExecuteMultipleResponse)_serviceProxy.Execute(request);
            Console.WriteLine("Pacotão de produto da ordem criado!");

            int cont = 0;
            foreach (var item in response.Responses)
            {
                if (item.Response != null)
                {
                    cont++;
                }
                else if (item.Fault != null)
                {
                    Console.WriteLine(item.Fault);
                }
            }
            Console.WriteLine($"Foram criados {cont} produto da ordem com sucesso!");
        }
    }
}
