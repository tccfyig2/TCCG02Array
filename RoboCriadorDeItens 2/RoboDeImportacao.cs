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
using Microsoft.Xrm.Sdk.Messages;

namespace RoboCriadorDeItens_2
    {
        class RoboDeImportacao
        {
            internal static void importacao()
            {
                Conexao conexao = new Conexao();
                CrmServiceClient serviceProxyOrigem = conexao.ObterConexaoCobaia();
                CrmServiceClient serviceProxyDestino = conexao.ObterConexaoApresentacao();


                //Gera Query em "contact"
                EntityCollection query = RetornarMultiplo(serviceProxyOrigem, "contact");
                //Importa Contato de um CRM e cria os mesmos dados em outro CRM
                ImportaContato(serviceProxyDestino, query);


                query = RetornarMultiplo(serviceProxyOrigem, "account");
                EntityCollection conta = ImportaConta(serviceProxyDestino, query);


                query = RetornarMultiplo(serviceProxyOrigem, "lead");
                ImportaContatoPotencial(serviceProxyDestino, query);

                query = RetornarMultiplo(serviceProxyOrigem, "salesorder");
                ImportaOrdem(serviceProxyDestino, query);


                query = RetornarMultiplo(serviceProxyOrigem, "salesorderdetail");
                ImportaProdutoOrdem(serviceProxyDestino, query);


                query = RetornarMultiplo(serviceProxyOrigem, "uom");
                ImportaUnidadePadrao(serviceProxyDestino, query);


                query = RetornarMultiplo(serviceProxyOrigem, "pricelevel");
                 ImportaListaPrecos(serviceProxyDestino, query);

                query = RetornarMultiplo(serviceProxyOrigem, "uomschedule");
                ImportaGrupoUnidades(serviceProxyDestino, query);

                query = RetornarMultiplo(serviceProxyOrigem, "product");
                ImportaProduto(serviceProxyDestino, query);


            }
                static void ImportaParaCrm(CrmServiceClient _serviceProxy, EntityCollection input)
                {
                    ExecuteMultipleRequest request = new ExecuteMultipleRequest()
                    {
                        Requests = new OrganizationRequestCollection(),
                        Settings = new ExecuteMultipleSettings
                        { ContinueOnError = false, ReturnResponses = true }
                    };

                    foreach (var entity in input.Entities)
                    {
                        CreateRequest createRequest = new CreateRequest { Target = entity };
                        request.Requests.Add(createRequest);
                    }

                    ExecuteMultipleResponse response = (ExecuteMultipleResponse)_serviceProxy.Execute(request);
                    Console.WriteLine("Pacotão criado!");

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
                    Console.WriteLine(cont);
                }





                static EntityCollection QueryExpression(CrmServiceClient serviceProxyOrigem, string entidade)
            {
                QueryExpression queryExpression = new QueryExpression(entidade);
                queryExpression.ColumnSet = new ColumnSet(true);

                //ConditionExpression condicao = new ConditionExpression("address1_city", ConditionOperator.Equal, "Natal");
                //queryExpression.Criteria.AddCondition(condicao);

                LinkEntity link = new LinkEntity("account", "contact", "primarycontactid", "contactid", JoinOperator.Inner);
                link.Columns = new ColumnSet(true);
                link.EntityAlias = "Contato";

                queryExpression.LinkEntities.Add(link);

                EntityCollection colecaoEntidades = serviceProxyOrigem.RetrieveMultiple(queryExpression);
                return colecaoEntidades;
                /*foreach (var entidade in colecaoEntidades.Entities)
                {
                    Console.WriteLine("Id: " + entidade.Id);
                    Console.WriteLine("Nome conta " + entidade["name"]);
                    Console.WriteLine("Nome Contato " + ((AliasedValue)entidade["Contato.firstname"]).Value);
                    Console.WriteLine("Sobrenome Contato " + ((AliasedValue)entidade["Contato.lastname"]).Value);
                }*/

            }

                static EntityCollection RetornarMultiplo(CrmServiceClient serviceProxyOrigem, string entidade)
                    {
                        QueryExpression queryExpression = new QueryExpression(entidade);

                        //queryExpression.Criteria.AddCondition("name", ConditionOperator.Equal, "teste");
                        queryExpression.ColumnSet = new ColumnSet(true);
                        EntityCollection colecaoEntidades = serviceProxyOrigem.RetrieveMultiple(queryExpression);
                        //foreach (var item in colecaoEntidades.Entities)
                        //{
                        //    Console.WriteLine(item["name"]);
                        //}

                        return colecaoEntidades;
                    }

                static EntityCollection ImportaContato(EntityCollection query)
                {
                    EntityCollection colecaoEntidades = new EntityCollection();
                    for (var i = 0; i < 200; i++)
                    {
                       var entidade = new Entity("contact");
                        entidade.Attributes.Add("firstname", query[i]["firstname"]);
                        entidade.Attributes.Add("lastname", query[i]["lastname"]);
                        entidade.Attributes.Add("cred2_cpf", query[i]["crb79_cpf"]);
                        entidade.Attributes.Add("mobilephone", query[i]["mobilephone"]);
                        entidade.Attributes.Add("emailaddress1", query[i]["address1_postalcode"]);
                        entidade.Attributes.Add("address1_postalcode", query[i]["address1_postalcode"]);
                        entidade.Attributes.Add("address1_line1", query[i]["address1_line1"]);
                        entidade.Attributes.Add("address1_line2", query[i]["address1_line2"]);
                        entidade.Attributes.Add("address1_line3", query[i]["address1_line3"]);
                        entidade.Attributes.Add("address1_city", query[i]["address1_city"]);
                        entidade.Attributes.Add("address1_stateorprovince", query[i]["address1_stateorprovince"]);
                        entidade.Attributes.Add("address1_country", "Brasil");
                        entidade.Id = query[i].Id;

                        colecaoEntidades.Entities.Add(entidade);

                    }
                    return colecaoEntidades;
                }
                static EntityCollection ImportaConta(EntityCollection query)
                {
                    EntityCollection colecaoEntidades = new EntityCollection();
                    for (var i = 0; i < 200; i++)
                    {
                        var entidade = new Entity("account");
                        entidade.Attributes.Add("name", query[i]["name"]);
                        //entidade.Attributes.Add("cred2_verificado","true");
                        entidade.Attributes.Add("cred2_cnpj", query[i]["crb79_cnpj"].ToString());
                        entidade.Attributes.Add("telephone1", query[i]["telephone1"].ToString());
                        entidade.Attributes.Add("emailaddress1", query[i]["address1_postalcode"].ToString());
                        entidade.Attributes.Add("address1_postalcode", query[i]["address1_postalcode"].ToString());
                        entidade.Attributes.Add("address1_line1", query[i]["address1_line1"].ToString());
                        entidade.Attributes.Add("address1_line2", query[i]["address1_line2"].ToString());
                        entidade.Attributes.Add("address1_line3", query[i]["address1_line3"].ToString());
                        entidade.Attributes.Add("address1_city", query[i]["address1_city"].ToString());
                        entidade.Attributes.Add("address1_stateorprovince", query[i]["address1_stateorprovince"].ToString());
                        entidade.Attributes.Add("address1_country", "Brasil");
                        //EntityCollection contact = RetornarMultiplo(serviceProxyOrigem, "contact");
                        entidade.Attributes.Add("primarycontactid", query[i]["primarycontactid"]);
                        entidade.Id = query[i].Id;

                        colecaoEntidades.Entities.Add(entidade);
                        //Console.WriteLine(new Guid(conta["primarycontactid"].ToString()));
                    }
                    return colecaoEntidades;
                }

                static EntityCollection ImportaContatoPotencial(CrmServiceClient serviceProxyDestino, EntityCollection query)
                {
                    EntityCollection colecaoEntidades = new EntityCollection();
                    for (var i = 0; i < 200; i++)
                    {
                        var entidade = new Entity("lead");
                        entidade.Attributes.Add("firstname", query[i]["firstname"]);
                        entidade.Attributes.Add("lastname", query[i]["lastname"]);
                        entidade.Attributes.Add("companyname", query[i]["companyname"]);
                        entidade.Attributes.Add("mobilephone", query[i]["mobilephone"]);
                        entidade.Attributes.Add("telephone1", query[i]["telephone1"]);
                        entidade.Attributes.Add("subject", query[i]["subject"]);
                        entidade.Attributes.Add("emailaddress1", query[i]["address1_postalcode"]);
                        entidade.Attributes.Add("address1_postalcode", query[i]["address1_postalcode"]);
                        entidade.Attributes.Add("address1_line1", query[i]["address1_line1"]);
                        entidade.Attributes.Add("address1_line2", query[i]["address1_line2"]);
                        entidade.Attributes.Add("address1_line3", query[i]["address1_line3"]);
                        entidade.Attributes.Add("address1_city", query[i]["address1_city"]);
                        entidade.Attributes.Add("address1_stateorprovince", query[i]["address1_stateorprovince"]);
                        entidade.Attributes.Add("address1_country", "Brasil");
                        entidade.Id = query[i].Id;

                        colecaoEntidades.Entities.Add(entidade);

                        //Console.WriteLine($"existem {i} Clientes Potenciais");
                    }
                    return colecaoEntidades;
                }

                static EntityCollection ImportaOrdem(CrmServiceClient serviceProxyDestino, EntityCollection contas)
                {
                    int i = 0;
                    foreach (var conta in contas.Entities)
                    {
                        var entidade = new Entity("salesorder");
                        entidade.Attributes.Add("name", conta["name"]);
                        entidade.Attributes.Add("cred2_codigo", conta["crb79_codigo"]);
                        entidade.Attributes.Add("customerid", conta["customerid"]);
                        entidade.Id = conta.Id;


                        serviceProxyDestino.Create(entidade);
                        i++;
                        //Console.WriteLine($"existem {i} Ordens");
                    }
                }

                static EntityCollection ImportaUnidadePadrao(CrmServiceClient serviceProxyDestino, EntityCollection contas)
                {

                    int i = 0;
                    foreach (var conta in contas.Entities)
                    {
                        var entidade = new Entity("uom");
                        entidade.Attributes.Add("name", conta["name"]);
                        entidade.Attributes.Add("uomscheduleid", conta["uomscheduleid"]);
                        entidade.Attributes.Add("quantity", conta["quantity"]);
                        entidade.Id = conta.Id;
                        serviceProxyDestino.Create(entidade);
                        i++;
                        //Console.WriteLine($"existem {i} Lista de Preços");
                    }
                }
                static EntityCollection ImportaListaPrecos(CrmServiceClient serviceProxyDestino, EntityCollection contas)
                {
    
                    int i = 0;
                    foreach (var conta in contas.Entities)
                    {
                        var entidade = new Entity("pricelevel");
                        entidade.Attributes.Add("name", conta["name"]);
                        entidade.Attributes.Add("begindate", DateTime.Today);
                        entidade.Attributes.Add("enddate", DateTime.Today.AddYears(1));
                        entidade.Id = conta.Id;
                        serviceProxyDestino.Create(entidade);
                        i++;
                        //Console.WriteLine($"existem {i} Lista de Preços");
                    }
                }

                static EntityCollection ImportaGrupoUnidades(CrmServiceClient serviceProxyDestino, EntityCollection contas)
                {
                
                    int i = 0;
                    foreach (var conta in contas.Entities)
                    {
                        var entidade = new Entity("uomschedule");
                        entidade.Attributes.Add("name", conta["name"]); 
                        entidade.Attributes.Add("baseuomname", conta["baseuomname"]);
                        entidade.Id = conta.Id;

                        serviceProxyDestino.Create(entidade);
                        i++;
                        //Console.WriteLine($"existem {i} Produtos da Ordem");
                    }
                }

                static EntityCollection ImportaProduto(CrmServiceClient serviceProxyDestino, EntityCollection contas)
                    {

                        int i = 0;
                        foreach (var conta in contas.Entities)
                        {
                            var entidade = new Entity("product");
                            entidade.Attributes.Add("name", conta["name"]);
                            entidade.Attributes.Add("productnumber", conta["productnumber"]);
                            entidade.Attributes.Add("defaultuomscheduleid", new EntityReference("uomschedule", new Guid("2e7091ef-c6da-4a3b-b657-89f057e3612e")));
                            entidade.Attributes.Add("defaultuomid", new EntityReference("uom", new Guid("70812c78-3a7a-eb11-a812-000d3a9d0d9a")));
                            entidade.Attributes.Add("quantitydecimal", conta["quantitydecimal"]);
                            entidade.Id = conta.Id;
                            serviceProxyDestino.Create(entidade);
                            i++;
                            //Console.WriteLine($"existem {i} Lista de Preços");
                        }
                    }

                static EntityCollection ImportaProdutoOrdem(CrmServiceClient serviceProxyDestino, EntityCollection contas)
                {
                        int i = 0;
                        foreach (var conta in contas.Entities)
                        {
                        var entidade = new Entity("salesorderdetail");
                        entidade.Attributes.Add("productid", conta["productid"]);
                        entidade.Attributes.Add("salesorderid", conta["salesorderid"]);
                        entidade.Attributes.Add("uomid", conta["uomid"]);
                        entidade.Id = conta.Id;

                        serviceProxyDestino.Create(entidade);
                        i++;
                        //Console.WriteLine($"existem {i} Produtos da Ordem");
                    }
                }


            }
    }

/*static void ImportaProduto(CrmServiceClient serviceProxyOrigem, CrmServiceClient serviceProxyDestino, EntityCollection contas)
{

    int i = 0;
    foreach (var conta in contas.Entities)
    {
        var entidade = new Entity("product");
        entidade.Attributes.Add("name", conta["name"]);
        entidade.Attributes.Add("productnumber", conta["productnumber"]);
        entidade.Attributes.Add("defaultuomscheduleid", conta["defaultuomscheduleid"]);
        entidade.Attributes.Add("defaultuomid", conta["defaultuomid"]);
        entidade.Attributes.Add("quantitydecimal", conta["quantitydecimal"]);
        entidade.Id = conta.Id;
        serviceProxyDestino.Create(entidade);
        i++;
        //Console.WriteLine($"existem {i} Lista de Preços");
    }
}*/