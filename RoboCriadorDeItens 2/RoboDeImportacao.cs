using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;

namespace RoboCriadorDeItens_2
{
    class RoboDeImportacao
    {
        internal static void importacao()   // MODEL
        {
            // Cria conexões!
            Conexao conexao = new Conexao();
            CrmServiceClient serviceProxyOrigem = conexao.ObterConexaoCobaia();
            CrmServiceClient serviceProxyDestino = conexao.ObterConexaoApresentacao();

            // Importa Contato!
            int tamanhoPacote = 2;
            EntityCollection contatos = RetornaEntidades(serviceProxyOrigem, "contact");
            for (int i = 0; i < (contatos.Entities.Count / tamanhoPacote); i++)
            {
                EntityCollection contact = ImportaContact(contatos, tamanhoPacote, i);
                EntityCollection atualizar = ImportaParaCrm(serviceProxyDestino, contact, "contact");
                AtualizaCrmOrigem(serviceProxyOrigem, atualizar);
                Console.WriteLine($"Pacote nº: {i} importado para contact!");
            }

            // Importa Conta!
            //EntityCollection contas = QueryExpression(serviceProxyOrigem, "account");
            //for (int i = 0; i < (contas.Entities.Count / tamanhoPacote); i++)
            //{
            //    EntityCollection account = ImportaAccount(contas, tamanhoPacote, i);
            //    EntityCollection atualizar = ImportaParaCrm(serviceProxyDestino, account, "account");
            //    AtualizaCrmOrigem(serviceProxyOrigem, atualizar);
            //    Console.WriteLine($"Pacote nº: {i} importado para account!");
            //}

            // Importa Clientes Potenciais!
            //EntityCollection clientesPotenciais = RetornaEntidades(serviceProxyOrigem, "lead");
            //for (int i = 0; i < (clientesPotenciais.Entities.Count / tamanhoPacote); i++)
            //{
            //    EntityCollection lead = ImportaLead(clientesPotenciais, tamanhoPacote, i);
            //    EntityCollection atualizar = ImportaParaCrm(serviceProxyDestino, lead, "lead");
            //    Console.WriteLine($"Pacote nº: {i} importado para lead!");
            //}

            //Importa Ordens!
            //EntityCollection ordens = RetornaEntidades(serviceProxyDestino, "salesorder");
            //for (int i = 0; i < (ordens.Entities.Count / tamanhoPacote); i++)
            //{
            //    EntityCollection salesorder = ImportaSalesorder(ordens, tamanhoPacote, i);
            //    EntityCollection atualizar = ImportaParaCrm(serviceProxyDestino, salesorder, "salesorder");
            //    Console.WriteLine($"Pacote nº: {i} importado para salesorder!");
            //}

            // Importa Produtos da Ordem!
            //EntityCollection produtosDaOrdem = RetornaEntidades(serviceProxyOrigem, "salesorderdetail");
            //for (int i = 0; i < (produtosDaOrdem.Entities.Count / tamanhoPacote); i++)
            //{
            //    EntityCollection salesorderdetail = ImportaSalesorderdetail(produtosDaOrdem, tamanhoPacote, i);
            //    EntityCollection atualizar = ImportaParaCrm(serviceProxyDestino, salesorderdetail, "salesorderdetail");
            //    Console.WriteLine($"Pacote nº: {i} importado para salesorderdetail!");
            //}

            // Os abaixo não devem ser necessarios uma vez que ja foram importados!

            // Importa Unidade
            // EntityCollection unidade = RetornaEntidadesCondicao(serviceProxyOrigem, "uom", "item");
            // EntityCollection uom = ImportaUom(serviceProxyOrigem, unidade);
            // ImportaParaCrm(serviceProxyDestino, uom);
            // Console.WriteLine($"Pacote importado para uom!");

            // Importa Lista de Preços
            // EntityCollection listaDePreços = RetornaEntidadesCondicao(serviceProxyOrigem, "pricelevel", "Default");
            // EntityCollection pricelevel = ImportaPricelevel(serviceProxyOrigem, listaDePreços);
            // ImportaParaCrm(serviceProxyDestino, pricelevel);
            // Console.WriteLine($"Pacote importado para pricelevel!");

            // Importa Grupo de Unidades
            // EntityCollection grupoDeUnidades = RetornaEntidadesCondicao(serviceProxyOrigem, "uomschedule", "Default Unit - Sales Professional Business");
            // EntityCollection uomschedule = ImportaUomschedule(serviceProxyOrigem, grupoDeUnidades);
            // ImportaParaCrm(serviceProxyDestino, pricelevel);
            // Console.WriteLine($"Pacote importado para uomschedule!");

            //Importa Produtos
            // EntityCollection produtos = RetornaEntidadesCondicao(serviceProxyOrigem, "product", "Notebook Lenovo");
            // EntityCollection product = ImportaProduct(serviceProxyOrigem, produtos);
            // ImportaParaCrm(serviceProxyDestino, product);
            // Console.WriteLine($"Pacote importado para product!");
        }
        static EntityCollection RetornaEntidades(CrmServiceClient serviceProxyOrigem, string entidade)  //DAL
        {
            EntityCollection itens = new EntityCollection();
            QueryExpression queryExpression = new QueryExpression(entidade);
            queryExpression.Criteria.AddCondition("crb79_importado", ConditionOperator.Equal, false);    // Alterar
            queryExpression.ColumnSet = new ColumnSet(true);

            queryExpression.PageInfo = new Microsoft.Xrm.Sdk.Query.PagingInfo();
            queryExpression.PageInfo.PageNumber = 1;
            bool moreData = true;
            while (moreData)
            {
                EntityCollection result = serviceProxyOrigem.RetrieveMultiple(queryExpression);
                itens.Entities.AddRange(result.Entities);
                moreData = result.MoreRecords;
                queryExpression.PageInfo.PageNumber++;
                queryExpression.PageInfo.PagingCookie = result.PagingCookie;
            }
            return itens;
        }
        static EntityCollection RetornaEntidadesCondicao(CrmServiceClient serviceProxyOrigem, string entidade, string condicao)  //DAL
        {
            QueryExpression queryExpression = new QueryExpression(entidade);
            queryExpression.Criteria.AddCondition("name", ConditionOperator.Equal, condicao);   // Alterar
            queryExpression.ColumnSet = new ColumnSet(true);
            EntityCollection colecaoEntidades = serviceProxyOrigem.RetrieveMultiple(queryExpression);

            return colecaoEntidades;
        }
        static EntityCollection QueryExpression(CrmServiceClient serviceProxyOrigem, string entidade)  //DAL
        {
            QueryExpression queryExpression = new QueryExpression(entidade);
            queryExpression.Criteria.AddCondition("crb79_importado", ConditionOperator.Equal, false);    // Alterar
            queryExpression.ColumnSet = new ColumnSet(true);
            LinkEntity link = new LinkEntity("account", "contact", "primarycontactid", "contactid", JoinOperator.Inner);
            link.Columns = new ColumnSet(true);
            link.EntityAlias = "Contato";
            queryExpression.LinkEntities.Add(link);
            EntityCollection colecaoEntidades = serviceProxyOrigem.RetrieveMultiple(queryExpression);

            return colecaoEntidades;
        }
        static EntityCollection ImportaParaCrm(CrmServiceClient serviceProxyDestino, EntityCollection colecaoEntidades, string tabela)  //DAL
        {
            ExecuteMultipleRequest request = new ExecuteMultipleRequest()
            {
                Requests = new OrganizationRequestCollection(),
                Settings = new ExecuteMultipleSettings
                { ContinueOnError = false, ReturnResponses = true }
            };
            foreach (var entidade in colecaoEntidades.Entities)
            {
                CreateRequest createRequest = new CreateRequest { Target = entidade };
                request.Requests.Add(createRequest);
            }
            ExecuteMultipleResponse response = (ExecuteMultipleResponse)serviceProxyDestino.Execute(request);
            Console.WriteLine("Pacote de entidades criado e enviado!");

            EntityCollection atualizar = new EntityCollection();
            int cont = 0;
            foreach (var item in response.Responses)
            {
                if (item.Fault != null)
                {
                    Console.WriteLine($"ERRO na entidade nº: {cont}!");
                }
                else
                {
                    // Quando passar a entidade para crm DESINO:
                    // retornar id para uma lista e atualizar COMPO bool crm ORIGEM.
                    Entity entidade = new Entity(tabela);    // RECEBER TABELA QUE DEVE SER ATUALIZADA!!!!!!
                    // Entidade tem que receber o Id do item que foi criado.
                    Console.WriteLine(item.Response);
                    string id = (item.Response.Results["id"].ToString());
                    Console.WriteLine(id);
                    entidade.Attributes.Add("crb79_importado", true);    // COMPO bool crm ORIGEM.
                    atualizar.Entities.Add(entidade);
                }
                cont++;
            }
            Console.WriteLine($"{cont} entidades importadas!");
            return atualizar;
        }
        static void AtualizaCrmOrigem(CrmServiceClient serviceProxyOrigem, EntityCollection colecaoEntidades)  //DAL
        {
            ExecuteMultipleRequest request = new ExecuteMultipleRequest()
            {
                Requests = new OrganizationRequestCollection(),
                Settings = new ExecuteMultipleSettings
                { ContinueOnError = false, ReturnResponses = true }
            };

            foreach (var entidade in colecaoEntidades.Entities)
            {
                UpdateRequest updateRequest = new UpdateRequest { Target = entidade };
                request.Requests.Add(updateRequest);
            }
            ExecuteMultipleResponse response = (ExecuteMultipleResponse)serviceProxyOrigem.Execute(request);
            Console.WriteLine("Pacote de ATUALIZAÇÃO de entidades criado e enviado!");
            int cont = 0;
            foreach (var item in response.Responses)
            {
                if (item.Response != null)
                {
                    //Console.WriteLine($"Entidade nº: {cont} criado!");
                }
                else if (item.Fault != null)
                {
                    Console.WriteLine($"ERRO na entidade nº: {cont}!\n{item.Fault}");
                }
                cont++;
            }
            Console.WriteLine($"{cont} entidades ATUALIZADAS!");
        }
        static EntityCollection ImportaContact(EntityCollection query, int tamanhoPacote, int contador) // MODEL
        {
            contador *= tamanhoPacote;
            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < tamanhoPacote; i++)
            {
                if (contador == query.Entities.Count) { break; }
                Entity entidade = new Entity("contact");
                entidade.Attributes.Add("firstname", query[contador]["firstname"]);
                entidade.Attributes.Add("lastname", query[contador]["lastname"]);
                entidade.Attributes.Add("cred2_cpf", query[contador]["crb79_cpf"]);
                entidade.Attributes.Add("cred2_verificado", "true");
                entidade.Attributes.Add("mobilephone", query[contador]["mobilephone"]);
                entidade.Attributes.Add("emailaddress1", query[contador]["emailaddress1"]);
                entidade.Attributes.Add("address1_postalcode", query[contador]["address1_postalcode"]);
                entidade.Attributes.Add("address1_line1", query[contador]["address1_line1"]);
                entidade.Attributes.Add("address1_line2", query[contador]["address1_line2"]);
                entidade.Attributes.Add("address1_line3", query[contador]["address1_line3"]);
                entidade.Attributes.Add("address1_city", query[contador]["address1_city"]);
                entidade.Attributes.Add("address1_stateorprovince", query[contador]["address1_stateorprovince"]);
                entidade.Attributes.Add("address1_country", "Brasil");
                entidade.Id = query[contador].Id;
                colecaoEntidades.Entities.Add(entidade);
                contador++;
            }
            return colecaoEntidades;
        }
        static EntityCollection ImportaAccount(EntityCollection query, int tamanhoPacote, int contador) // MODEL
        {
            contador *= tamanhoPacote;
            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < tamanhoPacote; i++)
            {
                Entity entidade = new Entity("account");
                entidade.Attributes.Add("name", query[contador]["name"]);
                entidade.Attributes.Add("cred2_verificado", "true");
                entidade.Attributes.Add("cred2_cnpj", query[contador]["crb79_cnpj"]);
                entidade.Attributes.Add("telephone1", query[contador]["telephone1"]);
                entidade.Attributes.Add("emailaddress1", query[contador]["emailaddress1"]);
                entidade.Attributes.Add("address1_postalcode", query[contador]["address1_postalcode"]);
                entidade.Attributes.Add("address1_line1", query[contador]["address1_line1"]);
                entidade.Attributes.Add("address1_line2", query[contador]["address1_line2"]);
                entidade.Attributes.Add("address1_line3", query[contador]["address1_line3"]);
                entidade.Attributes.Add("address1_city", query[contador]["address1_city"]);
                entidade.Attributes.Add("address1_stateorprovince", query[contador]["address1_stateorprovince"]);
                entidade.Attributes.Add("address1_country", "Brasil");
                entidade.Attributes.Add("primarycontactid", query[contador]["primarycontactid"]);
                entidade.Id = query[contador].Id;
                colecaoEntidades.Entities.Add(entidade);
                contador++;
            }
            return colecaoEntidades;
        }
        static EntityCollection ImportaLead(EntityCollection query, int tamanhoPacote, int contador) // MODEL
        {
            contador *= tamanhoPacote;
            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < tamanhoPacote; i++)
            {
                Entity entidade = new Entity("lead");
                entidade.Attributes.Add("firstname", query[contador]["firstname"]);
                entidade.Attributes.Add("lastname", query[contador]["lastname"]);
                entidade.Attributes.Add("companyname", query[contador]["companyname"]);
                entidade.Attributes.Add("mobilephone", query[contador]["mobilephone"]);
                entidade.Attributes.Add("telephone1", query[contador]["telephone1"]);
                entidade.Attributes.Add("subject", query[contador]["subject"]);
                entidade.Attributes.Add("emailaddress1", query[contador]["emailaddress1"]);
                entidade.Attributes.Add("address1_postalcode", query[contador]["address1_postalcode"]);
                entidade.Attributes.Add("address1_line1", query[contador]["address1_line1"]);
                entidade.Attributes.Add("address1_line2", query[contador]["address1_line2"]);
                entidade.Attributes.Add("address1_line3", query[contador]["address1_line3"]);
                entidade.Attributes.Add("address1_city", query[contador]["address1_city"]);
                entidade.Attributes.Add("address1_stateorprovince", query[contador]["address1_stateorprovince"]);
                entidade.Attributes.Add("address1_country", "Brasil");
                entidade.Id = query[contador].Id;
                colecaoEntidades.Entities.Add(entidade);
                contador++;
            }
            return colecaoEntidades;
        }
        static EntityCollection ImportaSalesorder(EntityCollection query, int tamanhoPacote, int contador) // MODEL
        {
            contador *= tamanhoPacote;
            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < tamanhoPacote; i++)
            {
                Entity entidade = new Entity("salesorder");
                entidade.Attributes.Add("name", query[contador]["name"]);
                entidade.Attributes.Add("cred2_codigo", query[contador]["crb79_codigo"]);
                entidade.Attributes.Add("customerid", query[contador]["customerid"]);
                entidade.Attributes.Add("pricelevelid", query[contador]["pricelevelid"]);
                entidade.Id = query[contador].Id;
                colecaoEntidades.Entities.Add(entidade);
                contador++;
            }
            return colecaoEntidades;
        }
        static EntityCollection ImportaSalesorderdetail(EntityCollection query, int tamanhoPacote, int contador) // MODEL
        {
            contador *= tamanhoPacote;
            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < tamanhoPacote; i++)
            {
                Entity entidade = new Entity("salesorderdetail");
                entidade.Attributes.Add("productid", query[contador]["productid"]);
                entidade.Attributes.Add("salesorderid", query[contador]["salesorderid"]);
                entidade.Attributes.Add("uomid", query[contador]["uomid"]);
                entidade.Id = query[contador].Id;
                colecaoEntidades.Entities.Add(entidade);
                contador++;
            }
            return colecaoEntidades;
        }
        static EntityCollection ImportaUom(CrmServiceClient serviceProxyOrigem, EntityCollection query) // MODEL
        {
            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < query.Entities.Count; i++)
            {
                Entity entidade = new Entity("uom");
                entidade.Attributes.Add("name", query[i]["name"]);
                entidade.Attributes.Add("uomscheduleid", query[i]["uomscheduleid"]);
                entidade.Attributes.Add("quantity", query[i]["quantity"]);
                entidade.Id = query[i].Id;
                colecaoEntidades.Entities.Add(entidade);
            }
            return colecaoEntidades;
        }
        static EntityCollection ImportaPricelevel(CrmServiceClient serviceProxyOrigem, EntityCollection query) // MODEL
        {
            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < query.Entities.Count; i++)
            {
                Entity entidade = new Entity("pricelevel");
                entidade.Attributes.Add("name", query[i]["name"]);
                entidade.Attributes.Add("begindate", DateTime.Today);
                entidade.Attributes.Add("enddate", DateTime.Today.AddYears(1));
                entidade.Id = query[i].Id;
                colecaoEntidades.Entities.Add(entidade);
            }
            return colecaoEntidades;
        }
        static EntityCollection ImportaUomschedule(CrmServiceClient serviceProxyOrigem, EntityCollection query) // MODEL
        {
            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < query.Entities.Count; i++)
            {
                Entity entidade = new Entity("uomschedule");
                entidade.Attributes.Add("name", query[i]["name"]);
                entidade.Attributes.Add("baseuomname", query[i]["baseuomname"]);
                entidade.Id = query[i].Id;
                colecaoEntidades.Entities.Add(entidade);
            }
            return colecaoEntidades;
        }
        static EntityCollection ImportaProduct(CrmServiceClient serviceProxyOrigem, EntityCollection query) // MODEL
        {
            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < query.Entities.Count; i++)
            {
                Entity entidade = new Entity("product");
                entidade.Attributes.Add("name", query[i]["name"]);
                entidade.Attributes.Add("productnumber", query[i]["productnumber"]);
                entidade.Attributes.Add("defaultuomscheduleid", new EntityReference("uomschedule", new Guid("2e7091ef-c6da-4a3b-b657-89f057e3612e")));
                entidade.Attributes.Add("defaultuomid", new EntityReference("uom", new Guid("70812c78-3a7a-eb11-a812-000d3a9d0d9a")));
                entidade.Attributes.Add("quantitydecimal", query[i]["quantitydecimal"]);
                entidade.Id = query[i].Id;
                colecaoEntidades.Entities.Add(entidade);
            }
            return colecaoEntidades;
        }
    }
}
