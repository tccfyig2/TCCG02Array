using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using RoboCriadorDeItens_2.DAL;
using System;
using System.Diagnostics;

namespace RoboCriadorDeItens_2
{
    class RoboDeImportacao
    {
        internal static void importacao()
        {
            Stopwatch cronometro = new Stopwatch();
            // Cria conexões!
            Conexao conexao = new Conexao();
            CrmServiceClient serviceProxyOrigem = conexao.ObterConexaoCobaia();
            CrmServiceClient serviceProxyDestino = conexao.ObterConexaoApresentacao();

            // Importa Contato!
            int tamanhoPacote = 50;
            cronometro.Start();
            EntityCollection contatos = Query.RetornaEntidades(serviceProxyOrigem, "contact");
            int loop = (int)Math.Ceiling((float)contatos.Entities.Count / tamanhoPacote);
            for (int i = 0; i < loop; i++)
            {
                EntityCollection contact = ImportaContact(contatos, tamanhoPacote, i);
                EntityCollection atualizar = Query.ImportaParaCrm(serviceProxyDestino, contact, "contact");
                Query.AtualizaCrmOrigem(serviceProxyOrigem, atualizar);
                Console.WriteLine($"Pacote nº: {i + 1} importado para contact!");
            }
            cronometro.Stop();
            Console.WriteLine("Tempo decorrido: {0:hh\\:mm\\:ss}", cronometro.Elapsed);

            // Importa Conta!
            cronometro.Start();
            EntityCollection contas = Query.QueryExpression(serviceProxyOrigem, "account");
            loop = (int)Math.Ceiling((float)contas.Entities.Count / tamanhoPacote);
            for (int i = 0; i < loop; i++)
            {
                EntityCollection account = ImportaAccount(contas, tamanhoPacote, i);
                EntityCollection atualizar = Query.ImportaParaCrm(serviceProxyDestino, account, "account");
                Query.AtualizaCrmOrigem(serviceProxyOrigem, atualizar);
                Console.WriteLine($"Pacote nº: {i + 1} importado para account!");
            }
            cronometro.Stop();
            Console.WriteLine("Tempo decorrido: {0:hh\\:mm\\:ss}", cronometro.Elapsed);

            // Importa Clientes Potenciais!
            cronometro.Start();
            EntityCollection clientesPotenciais = Query.RetornaEntidades(serviceProxyOrigem, "lead");
            loop = (int)Math.Ceiling((float)clientesPotenciais.Entities.Count / tamanhoPacote);
            for (int i = 0; i < loop; i++)
            {
                EntityCollection lead = ImportaLead(clientesPotenciais, tamanhoPacote, i);
                EntityCollection atualizar = Query.ImportaParaCrm(serviceProxyDestino, lead, "lead");
                Query.AtualizaCrmOrigem(serviceProxyOrigem, atualizar);
                Console.WriteLine($"Pacote nº: {i + 1} importado para lead!");
            }
            cronometro.Stop();
            Console.WriteLine("Tempo decorrido: {0:hh\\:mm\\:ss}", cronometro.Elapsed);

            //Importa Ordens!
            cronometro.Start();
            EntityCollection ordens = Query.RetornaEntidades(serviceProxyOrigem, "salesorder");
            loop = (int)Math.Ceiling((float)ordens.Entities.Count / tamanhoPacote);
            for (int i = 0; i < loop; i++)
            {
                EntityCollection salesorder = ImportaSalesorder(ordens, tamanhoPacote, i);
                EntityCollection atualizar = Query.ImportaParaCrm(serviceProxyDestino, salesorder, "salesorder");
                Query.AtualizaCrmOrigem(serviceProxyOrigem, atualizar);
                Console.WriteLine($"Pacote nº: {i + 1} importado para salesorder!");
            }
            cronometro.Stop();
            Console.WriteLine("Tempo decorrido: {0:hh\\:mm\\:ss}", cronometro.Elapsed);

            // Importa Produtos da Ordem!
            cronometro.Start();
            EntityCollection produtosDaOrdem = Query.RetornaEntidades(serviceProxyOrigem, "salesorderdetail");
            loop = (int)Math.Ceiling((float)produtosDaOrdem.Entities.Count / tamanhoPacote);
            for (int i = 0; i < loop; i++)
            {
                EntityCollection salesorderdetail = ImportaSalesorderdetail(produtosDaOrdem, tamanhoPacote, i);
                EntityCollection atualizar = Query.ImportaParaCrm(serviceProxyDestino, salesorderdetail, "salesorderdetail");
                Query.AtualizaCrmOrigem(serviceProxyOrigem, atualizar);
                Console.WriteLine($"Pacote nº: {i + 1} importado para salesorderdetail!");
            }
            cronometro.Stop();
            Console.WriteLine("Tempo decorrido: {0:hh\\:mm\\:ss}", cronometro.Elapsed);

            // Os abaixo não devem ser necessarios uma vez que ja foram importados!

            // Importa Unidade
            //EntityCollection unidade = Query.RetornaEntidadesCondicao(serviceProxyOrigem, "uom", "item");
            //EntityCollection uom = ImportaUom(serviceProxyOrigem, unidade);
            //Query.ImportaParaCrm(serviceProxyDestino, uom, "uom");
            //Console.WriteLine($"Pacote importado para uom!");

            // Importa Lista de Preços
            //EntityCollection listaDePreços = Query.RetornaEntidadesCondicao(serviceProxyOrigem, "pricelevel", "Default");
            //EntityCollection pricelevel = ImportaPricelevel(serviceProxyOrigem, listaDePreços);
            //Query.ImportaParaCrm(serviceProxyDestino, pricelevel, "pricelevel");
            //Console.WriteLine($"Pacote importado para pricelevel!");

            // Importa Grupo de Unidades
            //EntityCollection grupoDeUnidades = Query.RetornaEntidadesCondicao(serviceProxyOrigem, "uomschedule", "Default Unit - Sales Professional Business");
            //EntityCollection uomschedule = ImportaUomschedule(serviceProxyOrigem, grupoDeUnidades);
            //Query.ImportaParaCrm(serviceProxyDestino, pricelevel, "uomschedule");
            //Console.WriteLine($"Pacote importado para uomschedule!");

            //Importa Produtos
            //EntityCollection produtos = Query.RetornaEntidadesCondicao(serviceProxyOrigem, "product", "Notebook Lenovo");
            //EntityCollection product = ImportaProduct(serviceProxyOrigem, produtos);
            //Query.ImportaParaCrm(serviceProxyDestino, product, "product");
            //Console.WriteLine($"Pacote importado para product!");
        }
        static EntityCollection ImportaContact(EntityCollection query, int tamanhoPacote, int contador)
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
        static EntityCollection ImportaAccount(EntityCollection query, int tamanhoPacote, int contador)
        {
            contador *= tamanhoPacote;
            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < tamanhoPacote; i++)
            {
                if (contador == query.Entities.Count) { break; }
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
                if (contador == query.Entities.Count) { break; }
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
                if (contador == query.Entities.Count) { break; }
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
                if (contador == query.Entities.Count) { break; }
                Entity entidade = new Entity("salesorderdetail");
                entidade.Attributes.Add("productid", query[contador]["productid"]);
                entidade.Attributes.Add("salesorderid", query[contador]["salesorderid"]);
                entidade.Attributes.Add("uomid", new EntityReference("uom", new Guid("6f80721a-bf77-eb11-a812-000d3a1c6462")));
                entidade.Id = query[contador].Id;
                colecaoEntidades.Entities.Add(entidade);
                contador++;
            }
            return colecaoEntidades;
        }
        static EntityCollection ImportaUom(CrmServiceClient serviceProxyOrigem, EntityCollection query)
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
        static EntityCollection ImportaPricelevel(CrmServiceClient serviceProxyOrigem, EntityCollection query)
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
        static EntityCollection ImportaUomschedule(CrmServiceClient serviceProxyOrigem, EntityCollection query)
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
        static EntityCollection ImportaProduct(CrmServiceClient serviceProxyOrigem, EntityCollection query)
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
