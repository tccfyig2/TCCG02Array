using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Tooling.Connector;
using RoboCriadorDeItens_2.Geradores;
using System;
using System.Collections.Generic;

namespace RoboCriadorDeItens_2
{
    class RoboDeCriacao : listaId
    {
        public static int quantidade = 5;
        public static Random rnd = new Random();

        public static Conexao conexao = new Conexao();

        public static CrmServiceClient service = conexao.ObterConexaoCobaia();

        public static ExecuteMultipleRequest request = new ExecuteMultipleRequest()
        {
            Requests = new OrganizationRequestCollection(),
            Settings = new ExecuteMultipleSettings
            {
                ContinueOnError = false,
                ReturnResponses = true
            }
        };
        internal static void criacao()
        {
            criaContato();

            criaContato();

            criaClientePotencial();

            criaOrdem();

            criaListaPrecos();

        }
        static void criaContato()
        {
            try
            {
                for (int i = 0; i < quantidade; i++)
                {
                    var entidade = new Entity("contact");

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

                    var createRequest = new CreateRequest()
                    {
                        Target = entidade
                    };
                    request.Requests.Add(createRequest);
                }

                var response = (ExecuteMultipleResponse)service.Execute(request);
                Console.WriteLine("Pacotão Criado!");

                int cont = 0;
                foreach (var r in response.Responses)
                {
                    cont++;
                    if (r.Response != null)
                    {
                        Console.WriteLine($"Contact nº: { cont + 1}");
                        Console.Clear();
                    }
                    else if (r.Fault != null)
                        Console.WriteLine(r.Fault);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        static void criaConta()
        {
            List<listaId> contactId = new List<listaId>(GeradorId.BuscaId(service, tabela: "contact", campo: "contactid"));
            try
            {
                for (int i = 0; i < quantidade; i++)
                {
                    int index = rnd.Next(0, contactId.Count);

                    var entidade = new Entity("account");

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

                    var createRequest = new CreateRequest()
                    {
                        Target = entidade
                    };
                    request.Requests.Add(createRequest);
                }
                var response = (ExecuteMultipleResponse)service.Execute(request);
                Console.WriteLine("Pacotão Criado!");

                int cont = 0;
                foreach (var r in response.Responses)
                {
                    cont++;
                    if (r.Response != null)
                    {
                        Console.WriteLine($"Contact nº: { cont + 1}");
                        Console.Clear();
                    }
                    else if (r.Fault != null)
                        Console.WriteLine(r.Fault);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        static void criaClientePotencial()
        {
            try
            {
                for (int i = 0; i < quantidade; i++)
                {
                    var entidade = new Entity("lead");

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

                    var response = (ExecuteMultipleResponse)service.Execute(request);
                    Console.WriteLine("Pacotão Criado!");

                    int cont = 0;
                    foreach (var r in response.Responses)
                    {
                        cont++;
                        if (r.Response != null)
                        {
                            Console.WriteLine($"Lead nº: { cont + 1}");
                        }
                        else if (r.Fault != null)
                            Console.WriteLine(r.Fault);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        static void criaOrdem()
        {
            List<listaId> accountId = new List<listaId>(GeradorId.BuscaId(service, tabela: "account", campo: "accountid"));
            try
            {
                for (int i = 0; i < quantidade; i++)
                {
                    int index = rnd.Next(0, accountId.Count);
                    Entity entidade = new Entity("salesorder");

                    entidade.Attributes.Add("name", $"Cliente nº: {i + 1}");
                    entidade.Attributes.Add("customerid", new EntityReference("account", accountId[index].Id));

                    var response = (ExecuteMultipleResponse)service.Execute(request);
                    Console.WriteLine("Pacotão Criado!");

                    int cont = 0;
                    foreach (var r in response.Responses)
                    {
                        cont++;
                        if (r.Response != null)
                        {
                            Console.WriteLine($"Salesorder nº: { cont + 1}");
                        }
                        else if (r.Fault != null)
                            Console.WriteLine(r.Fault);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        static void produtoOrdem()
        {
            List<listaId> OrdemId = new List<listaId>(GeradorId.BuscaId(service, tabela: "salesorder", campo: "salesorderid"));
            Guid prduct = new Guid("4190122b-0477-eb11-a812-000d3a1c6462");
            Guid uomid = new Guid("5f753633-aa6e-eb11-b0b2-000d3a55dda2");
            try
            {
                for (int i = 0; i < quantidade; i++)
                {
                    int index = rnd.Next(0, OrdemId.Count);
                    var entidade = new Entity("salesorderdetail");

                    entidade.Attributes.Add("productid", new EntityReference("product", prduct));
                    entidade.Attributes.Add("salesorderid", new EntityReference("salesorder", OrdemId[index].Id));
                    entidade.Attributes.Add("uomid", new EntityReference("businessunit", uomid));

                    var response = (ExecuteMultipleResponse)service.Execute(request);
                    Console.WriteLine("Pacotão Criado!");

                    int cont = 0;
                    foreach (var r in response.Responses)
                    {
                        cont++;
                        if (r.Response != null)
                        {
                            Console.WriteLine($"Salesorder nº: { cont + 1}");
                        }
                        else if (r.Fault != null)
                            Console.WriteLine(r.Fault);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        static void criaListaPrecos()
        {
            try
            {
                for (int i = 0; i < quantidade; i++)
                {
                    var entidade = new Entity("pricelevel");

                    entidade.Attributes.Add("name", $"Produto {i + 1}");
                    entidade.Attributes.Add("begindate", DateTime.Today);
                    entidade.Attributes.Add("enddate", DateTime.Today.AddYears(1));

                    var response = (ExecuteMultipleResponse)service.Execute(request);
                    Console.WriteLine("Pacotão Criado!");

                    int cont = 0;
                    foreach (var r in response.Responses)
                    {
                        cont++;
                        if (r.Response != null)
                        {
                            Console.WriteLine($"Salesorder nº: { cont + 1}");
                        }
                        else if (r.Fault != null)
                            Console.WriteLine(r.Fault);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}


