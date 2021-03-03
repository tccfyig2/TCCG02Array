using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;

namespace RoboCriadorDeItens_2
{
    class RoboDeImportacao
    {
        internal static void importacao()
        {
            // Cria conexões!
            Conexao conexao = new Conexao();
            CrmServiceClient serviceProxyOrigem = conexao.ObterConexaoCobaia();
            CrmServiceClient serviceProxyDestino = conexao.ObterConexaoApresentacao();

            //// Importa Contato!
            int n = 0;
            int tamanhoPacote = 50;
            //EntityCollection contatos = RetornaEntidades(serviceProxyOrigem, "contact");
            //int loop = contatos.Entities.Count / tamanhoPacote;
            //while (n < loop)
            //{
            //    EntityCollection contact = ImportaContact(contatos, tamanhoPacote, n);
            //    ImportaParaCrm(serviceProxyDestino, contact);
            //    Console.WriteLine($"Pacote nº: {n} importado para contact!");
            //    n++;
            //}

            //// Importa Conta!
            //n = 0;
            //EntityCollection contas = QueryExpression(serviceProxyOrigem, "account");
            //loop = contas.Entities.Count / tamanhoPacote;
            //while (n < loop)
            //{
            //    EntityCollection account = ImportaAccount(contas, tamanhoPacote, n);
            //    ImportaParaCrm(serviceProxyDestino, account);
            //    Console.WriteLine($"Pacote nº: {n} importado para account!");
            //    n++;
            //}

            //// Importa Clientes Potenciais!
            //n = 0;
            //EntityCollection clientesPotenciais = RetornaEntidades(serviceProxyOrigem, "lead");
            //loop = clientesPotenciais.Entities.Count / tamanhoPacote;
            //while (n < loop)
            //{
            //    EntityCollection lead = ImportaLead(clientesPotenciais, tamanhoPacote, n);
            //    ImportaParaCrm(serviceProxyDestino, lead);
            //    Console.WriteLine($"Pacote nº: {n} importado para lead!");
            //    n++;
            //}

            // Importa Ordens!
            n = 0;
            EntityCollection ordens = RetornaEntidades(serviceProxyOrigem, "salesorder");
            int loop = ordens.Entities.Count / tamanhoPacote;
            while (n < loop)
            {
                EntityCollection salesorder = ImportaSalesorder(ordens, tamanhoPacote, n);
                ImportaParaCrm(serviceProxyDestino, salesorder);
                Console.WriteLine($"Pacote nº: {n} importado para salesorder!");
                n++;
            }

            // Importa Produtos da Ordem!
            n = 0;
            EntityCollection produtosDaOrdem = RetornaEntidades(serviceProxyOrigem, "salesorderdetail");
            loop = produtosDaOrdem.Entities.Count / tamanhoPacote;
            while (n < loop)
            {
                EntityCollection salesorderdetail = ImportaSalesorderdetail(produtosDaOrdem, tamanhoPacote, n);
                ImportaParaCrm(serviceProxyDestino, salesorderdetail);
                Console.WriteLine($"Pacote nº: {n} importado para salesorderdetail!");
                n++;
            }

            //Os abaixo não devem ser necessarios uma vez que ja foram importados!

            //FILIPE não entendi oq vc quis dizer com o negocio do criteria la pq a maneira que eu pensei de colocar foi só se fizesse duas query oq não fez muito sentido na minha cabeça, de qualquer forma esses ai devem rodar com talvez alguns caso as tabelas não estejam vazias!

            EntityCollection uom = ImportaUnidadePadrao(serviceProxyOrigem);
            ImportaParaCrm(serviceProxyDestino, uom);
            Console.WriteLine($"Pacote importado para uom!");

            EntityCollection pricelevel = ImportaListaPrecos(serviceProxyOrigem);
            ImportaParaCrm(serviceProxyDestino, pricelevel);
            Console.WriteLine($"Pacote importado para pricelevel!");

            EntityCollection uomschedule = ImportaGrupoUnidades(serviceProxyOrigem);
            ImportaParaCrm(serviceProxyDestino, pricelevel);
            Console.WriteLine($"Pacote importado para uomschedule!");

            EntityCollection product = ImportaProduto(serviceProxyOrigem);
            ImportaParaCrm(serviceProxyDestino, product);
            Console.WriteLine($"Pacote importado para product!");
            
        }
        static EntityCollection RetornaEntidades(CrmServiceClient serviceProxyOrigem, string entidade)
        {
            QueryExpression queryExpression = new QueryExpression(entidade);
            queryExpression.ColumnSet = new ColumnSet(true);
            EntityCollection colecaoEntidades = serviceProxyOrigem.RetrieveMultiple(queryExpression);

            return colecaoEntidades;
        }
        static EntityCollection RetornaEntidadesComCondicao(CrmServiceClient serviceProxyOrigem, string entidade, string campo, string condicao)
        {
            QueryExpression queryExpression = new QueryExpression(entidade);
            queryExpression.Criteria.AddCondition(campo, ConditionOperator.Equal, condicao);
            queryExpression.ColumnSet = new ColumnSet(true);
            EntityCollection colecaoEntidades = serviceProxyOrigem.RetrieveMultiple(queryExpression);

            return colecaoEntidades;
        }
        static EntityCollection QueryExpression(CrmServiceClient serviceProxyOrigem, string entidade)
        {
            QueryExpression queryExpression = new QueryExpression(entidade);
            queryExpression.ColumnSet = new ColumnSet(true);

            LinkEntity link = new LinkEntity("account", "contact", "primarycontactid", "contactid", JoinOperator.Inner);
            link.Columns = new ColumnSet(true);
            link.EntityAlias = "Contato";

            queryExpression.LinkEntities.Add(link);

            EntityCollection colecaoEntidades = serviceProxyOrigem.RetrieveMultiple(queryExpression);
            return colecaoEntidades;
        }
        static void ImportaParaCrm(CrmServiceClient serviceProxyDestino, EntityCollection colecaoEntidades)
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
            Console.WriteLine($"{cont} entidades importadas!");
        }
        static EntityCollection ImportaContact(EntityCollection query, int tamanhoPacote, int contador)
        {
            contador *= tamanhoPacote;
            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < tamanhoPacote; i++)
            {
                if (contador > query.Entities.Count) { break; }
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
        static EntityCollection ImportaAccount(EntityCollection query, int tamanhoPacote, int contador)
        {
            contador *= tamanhoPacote;
            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < tamanhoPacote; i++)
            {
                if (contador > query.Entities.Count) { break; }
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
        static EntityCollection ImportaLead(EntityCollection query, int tamanhoPacote, int contador)
        {
            contador *= tamanhoPacote;
            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < tamanhoPacote; i++)
            {
                if (contador > query.Entities.Count) { break; }
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
        static EntityCollection ImportaSalesorder(EntityCollection query, int tamanhoPacote, int contador)
        {
            contador *= tamanhoPacote;
            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < tamanhoPacote; i++)
            {
                if (contador > query.Entities.Count) { break; }
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
        static EntityCollection ImportaSalesorderdetail(EntityCollection query, int tamanhoPacote, int contador)
        {
            contador *= tamanhoPacote;
            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < tamanhoPacote; i++)
            {
                if (contador > query.Entities.Count) { break; }
                Entity entidade = new Entity("salesorderdetail");
                entidade.Attributes.Add("productid", query[contador]["productid"]);
                entidade.Attributes.Add("salesorderid", query[contador]["salesorderid"]);
                entidade.Attributes.Add("uomid", query[contador]["uomid"]);
                entidade.Id = query[contador].Id;
                colecaoEntidades.Entities.Add(entidade);
            }
            return colecaoEntidades;
        }
        static EntityCollection ImportaUnidadePadrao(CrmServiceClient serviceProxyOrigem)
        {
            QueryExpression queryExpression = new QueryExpression("uom");
            //queryExpression.Criteria.AddCondition(campo, ConditionOperator.Equal, condicao);
            queryExpression.ColumnSet = new ColumnSet(true);
            EntityCollection unidades = serviceProxyOrigem.RetrieveMultiple(queryExpression);

            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < unidades.Entities.Count; i++)
            {
                Entity entidade = new Entity("uom");
                entidade.Attributes.Add("name", unidades[i]["name"]);
                entidade.Attributes.Add("uomscheduleid", unidades[i]["uomscheduleid"]);
                entidade.Attributes.Add("quantity", unidades[i]["quantity"]);
                entidade.Id = unidades[i].Id;
                colecaoEntidades.Entities.Add(entidade);
            }
            return colecaoEntidades;
        }
        static EntityCollection ImportaListaPrecos(CrmServiceClient serviceProxyOrigem)
        {
            QueryExpression queryExpression = new QueryExpression("pricelevel");
            //queryExpression.Criteria.AddCondition(campo, ConditionOperator.Equal, condicao);
            queryExpression.ColumnSet = new ColumnSet(true);
            EntityCollection listaPrecos = serviceProxyOrigem.RetrieveMultiple(queryExpression);

            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < listaPrecos.Entities.Count; i++)
            {
                Entity entidade = new Entity("pricelevel");
                entidade.Attributes.Add("name", listaPrecos[i]["name"]);
                entidade.Attributes.Add("begindate", DateTime.Today);
                entidade.Attributes.Add("enddate", DateTime.Today.AddYears(1));
                entidade.Id = listaPrecos[i].Id;
                colecaoEntidades.Entities.Add(entidade);
            }
            return colecaoEntidades;
        }
        static EntityCollection ImportaGrupoUnidades(CrmServiceClient serviceProxyOrigem)
        {
            QueryExpression queryExpression = new QueryExpression("uomschedule");
            //queryExpression.Criteria.AddCondition(campo, ConditionOperator.Equal, condicao);
            queryExpression.ColumnSet = new ColumnSet(true);
            EntityCollection grupoUnidades = serviceProxyOrigem.RetrieveMultiple(queryExpression);

            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < grupoUnidades.Entities.Count; i++)
            {
                Entity entidade = new Entity("uomschedule");
                entidade.Attributes.Add("name", grupoUnidades[i]["name"]);
                entidade.Attributes.Add("baseuomname", grupoUnidades[i]["baseuomname"]);
                entidade.Id = grupoUnidades[i].Id;
                colecaoEntidades.Entities.Add(entidade);
            }
            return colecaoEntidades;
        }
        static EntityCollection ImportaProduto(CrmServiceClient serviceProxyOrigem)
        {
            QueryExpression queryExpression = new QueryExpression("product");
            //queryExpression.Criteria.AddCondition(campo, ConditionOperator.Equal, condicao);
            queryExpression.ColumnSet = new ColumnSet(true);
            EntityCollection produtos = serviceProxyOrigem.RetrieveMultiple(queryExpression);

            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < produtos.Entities.Count; i++)
            {
                Entity entidade = new Entity("product");
                entidade.Attributes.Add("name", produtos[i]["name"]);
                entidade.Attributes.Add("productnumber", produtos[i]["productnumber"]);
                entidade.Attributes.Add("defaultuomscheduleid", new EntityReference("uomschedule", new Guid("2e7091ef-c6da-4a3b-b657-89f057e3612e")));
                entidade.Attributes.Add("defaultuomid", new EntityReference("uom", new Guid("70812c78-3a7a-eb11-a812-000d3a9d0d9a")));
                entidade.Attributes.Add("quantitydecimal", produtos[i]["quantitydecimal"]);
                entidade.Id = produtos[i].Id;
                colecaoEntidades.Entities.Add(entidade);
            }
            return colecaoEntidades;
        }
    }
}