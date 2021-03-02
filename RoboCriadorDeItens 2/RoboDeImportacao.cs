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

            //Importando Contato!

            int n = 1;
            EntityCollection contatos = RetornarMultiplo(serviceProxyOrigem, "contact");
            while (n <= 25)
            {
                Console.WriteLine(n);

                EntityCollection contact = ImportaContato(contatos, 200, n);
                ImportaParaCrm(serviceProxyDestino, contact);
                Console.WriteLine($"Pacote contact {n} importado!");

                n++;
            }

            //Importando Conta!

            //int n = 1;
            //EntityCollection conta = RetornarMultiplo(serviceProxyOrigem, "account");
            //while (n <= 25)
            //{
            //    Console.WriteLine(n);
            //
            //    EntityCollection account = ImportaConta(conta, 200, n);
            //    ImportaParaCrm(serviceProxyDestino, account);
            //    Console.WriteLine($"Pacote account {n} importado!");
            //
            //    n++;
            //}

            //Importando Contato Potencial!

            //int n = 1;
            //EntityCollection contatoPotencial = RetornarMultiplo(serviceProxyOrigem, "lead");
            //while (n <= 25)
            //{
            //    Console.WriteLine(n);
            //
            //    EntityCollection lead = ImportaContatoPotencial(contatoPotencial, 200, n);
            //    ImportaParaCrm(serviceProxyDestino, lead);
            //    Console.WriteLine($"Pacote lead {n} importado!");
            //
            //    n++;
            //}

            //Importando Ordem!

            //int n = 1;
            //EntityCollection ordem = RetornarMultiplo(serviceProxyOrigem, "salesorder");
            //while (n <= 25)
            //{
            //    Console.WriteLine(n);
            //
            //    EntityCollection salesorder = ImportaOrdem(ordem, 125, n);
            //    ImportaParaCrm(serviceProxyDestino, salesorder);
            //    Console.WriteLine($"Pacote salesorder {n} importado!");
            //
            //    n++;
            //}

            //Importando Produtos da Ordem!

            //int n = 1;
            //EntityCollection produtoOrdem = RetornarMultiplo(serviceProxyOrigem, "salesorderdetail");
            //while (n <= 25)
            //{
            //    Console.WriteLine(n);
            //
            //    EntityCollection salesorderdetail = ImportaProdutoOrdem(produtoOrdem, 125, n);
            //    ImportaParaCrm(serviceProxyDestino, salesorderdetail);
            //    Console.WriteLine($"Pacote contact {n} importado!");
            //
            //    n++;
            //}



            //Os abaixo não devem ser necessarios uma vez que ja foram importados!

            //query = RetornarMultiplo(serviceProxyOrigem, "uom");
            //ImportaUnidadePadrao(serviceProxyDestino, query);
            //
            //
            //query = RetornarMultiplo(serviceProxyOrigem, "pricelevel");
            // ImportaListaPrecos(serviceProxyDestino, query);
            //
            //query = RetornarMultiplo(serviceProxyOrigem, "uomschedule");
            //ImportaGrupoUnidades(serviceProxyDestino, query);
            //
            //query = RetornarMultiplo(serviceProxyOrigem, "product");
            //ImportaProduto(serviceProxyDestino, query);


        }
        static void ImportaParaCrm(CrmServiceClient serviceProxyDestino, EntityCollection input)
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

            ExecuteMultipleResponse response = (ExecuteMultipleResponse)serviceProxyDestino.Execute(request);
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
        static EntityCollection ImportaContato(EntityCollection query, int tamanhoPacote, int contador)
        {   
            contador *= tamanhoPacote;
            EntityCollection colecaoEntidades = new EntityCollection();
            for (var i = (contador-1); i < tamanhoPacote; contador++)
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
        static EntityCollection ImportaConta(EntityCollection query, int tamanhoPacote, int contador)
        {
            EntityCollection colecaoEntidades = new EntityCollection();
            for (var i = (contador - 1); i < tamanhoPacote; contador++)
            {
                contador *= tamanhoPacote;
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
        static EntityCollection ImportaContatoPotencial(EntityCollection query, int tamanhoPacote, int contador)
        {
            EntityCollection colecaoEntidades = new EntityCollection();
            for (var i = (contador - 1); i < tamanhoPacote; contador++)
            {
                contador *= tamanhoPacote;
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
        static EntityCollection ImportaOrdem(EntityCollection query, int tamanhoPacote, int contador)
        {
            contador *= tamanhoPacote;
            EntityCollection colecaoEntidades = new EntityCollection();
            for (var i = (contador - 1); i < tamanhoPacote; contador++)
            {
                var entidade = new Entity("salesorder");
                entidade.Attributes.Add("name", query[i]["name"]);
                entidade.Attributes.Add("cred2_codigo", query[i]["crb79_codigo"]);
                entidade.Attributes.Add("customerid", query[i]["customerid"]);
                entidade.Attributes.Add("pricelevelid", query[i]["pricelevelid"]);
                entidade.Id = query[i].Id;


                colecaoEntidades.Entities.Add(entidade); ;
                //Console.WriteLine($"existem {i} Ordens");
            }
            return colecaoEntidades;
        }
        static EntityCollection ImportaUnidadePadrao(EntityCollection query, int tamanhoPacote, int contador)
        {
            contador *= tamanhoPacote;
            EntityCollection colecaoEntidades = new EntityCollection();
            for (var i = (contador - 1); i < tamanhoPacote; contador++)
            {
                var entidade = new Entity("uom");
                entidade.Attributes.Add("name", query[i]["name"]);
                entidade.Attributes.Add("uomscheduleid", query[i]["uomscheduleid"]);
                entidade.Attributes.Add("quantity", query[i]["quantity"]);
                entidade.Id = query[i].Id;
                colecaoEntidades.Entities.Add(entidade);
                //Console.WriteLine($"existem {i} Lista de Preços");
            }
            return colecaoEntidades;
        }
        static EntityCollection ImportaListaPrecos(EntityCollection query, int tamanhoPacote, int contador)
        {
            contador *= tamanhoPacote;
            EntityCollection colecaoEntidades = new EntityCollection();
            for (var i = (contador - 1); i < tamanhoPacote; contador++)
            {
                var entidade = new Entity("pricelevel");
                entidade.Attributes.Add("name", query[i]["name"]);
                entidade.Attributes.Add("begindate", DateTime.Today);
                entidade.Attributes.Add("enddate", DateTime.Today.AddYears(1));
                entidade.Id = query[i].Id;

                colecaoEntidades.Entities.Add(entidade);
            }
            return colecaoEntidades;
        }
        static EntityCollection ImportaGrupoUnidades(EntityCollection query, int tamanhoPacote, int contador)
        {
            contador *= tamanhoPacote;
            EntityCollection colecaoEntidades = new EntityCollection();
            for (var i = (contador - 1); i < tamanhoPacote; contador++)
            {
                var entidade = new Entity("uomschedule");
                entidade.Attributes.Add("name", query[i]["name"]); 
                entidade.Attributes.Add("baseuomname", query[i]["baseuomname"]);
                entidade.Id = query[i].Id;

                colecaoEntidades.Entities.Add(entidade);

                //Console.WriteLine($"existem {i} Produtos da Ordem");
            }
            return colecaoEntidades;
        }
        static EntityCollection ImportaProduto(EntityCollection query, int tamanhoPacote, int contador)
        {
            EntityCollection colecaoEntidades = new EntityCollection();
            for (var i = (contador - 1); i < tamanhoPacote; contador++)
            {
                contador *= tamanhoPacote;
                var entidade = new Entity("product");
                entidade.Attributes.Add("name", query[i]["name"]);
                entidade.Attributes.Add("productnumber", query[i]["productnumber"]);
                entidade.Attributes.Add("defaultuomscheduleid", new EntityReference("uomschedule", new Guid("2e7091ef-c6da-4a3b-b657-89f057e3612e")));
                entidade.Attributes.Add("defaultuomid", new EntityReference("uom", new Guid("70812c78-3a7a-eb11-a812-000d3a9d0d9a")));
                entidade.Attributes.Add("quantitydecimal", query[i]["quantitydecimal"]);
                entidade.Id = query[i].Id;
                colecaoEntidades.Entities.Add(entidade);
                //Console.WriteLine($"existem {i} Lista de Preços");
            }
            return colecaoEntidades;
        }
        static EntityCollection ImportaProdutoOrdem(EntityCollection query, int tamanhoPacote, int contador)
        {
            EntityCollection colecaoEntidades = new EntityCollection();
            for (var i = (contador - 1); i < tamanhoPacote; contador++)
            {
                contador *= tamanhoPacote;
                var entidade = new Entity("salesorderdetail");
                entidade.Attributes.Add("productid", query[i]["productid"]);
                entidade.Attributes.Add("salesorderid", query[i]["salesorderid"]);
                entidade.Attributes.Add("uomid", query[i]["uomid"]);
                entidade.Id = query[i].Id;

                colecaoEntidades.Entities.Add(entidade);
                //Console.WriteLine($"existem {i} Produtos da Ordem");
            }
            return colecaoEntidades;
        }
    }
}