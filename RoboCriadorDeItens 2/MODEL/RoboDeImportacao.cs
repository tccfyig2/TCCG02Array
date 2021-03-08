using Microsoft.Xrm.Sdk;
using RoboCriadorDeItens_2.DAL;
using System;
using System.Diagnostics;

namespace RoboCriadorDeItens_2.MODEL
{
    class RoboDeImportacao : Query
    {
        internal static void Importacao()
        {
            Stopwatch cronometro = new Stopwatch();
            // Especificações
            int tamanhoPacote = 50;

            // Importa Contato!
            cronometro.Start();
            EntityCollection contatos = RetornaEntidades("contact");
            int loop = (int)Math.Ceiling((float)contatos.Entities.Count / tamanhoPacote);
            for (int i = 0; i < loop; i++)
            {
                EntityCollection contact = ImportaContact(contatos, tamanhoPacote, i);
                EntityCollection atualizar = ImportaParaCrm(contact, "contact");
                AtualizaCrmOrigem(atualizar);
                Console.WriteLine($"Pacote nº: {i + 1} importado para contact!");
            }
            cronometro.Stop();
            Console.WriteLine("Tempo de importação e atualização: {0:hh\\:mm\\:ss}\n", cronometro.Elapsed);

            // Importa Conta!
            cronometro.Start();
            EntityCollection contas = RetornaEntidadesLink("account");
            loop = (int)Math.Ceiling((float)contas.Entities.Count / tamanhoPacote);
            for (int i = 0; i < loop; i++)
            {
                EntityCollection account = ImportaAccount(contas, tamanhoPacote, i);
                EntityCollection atualizar = ImportaParaCrm(account, "account");
                AtualizaCrmOrigem(atualizar);
                Console.WriteLine($"Pacote nº: {i + 1} importado para account!");
            }
            cronometro.Stop();
            Console.WriteLine("Tempo de importação e atualização: {0:hh\\:mm\\:ss}\n", cronometro.Elapsed);

            // Importa Clientes Potenciais!
            cronometro.Start();
            EntityCollection clientesPotenciais = RetornaEntidades("lead");
            loop = (int)Math.Ceiling((float)clientesPotenciais.Entities.Count / tamanhoPacote);
            for (int i = 0; i < loop; i++)
            {
                EntityCollection lead = ImportaLead(clientesPotenciais, tamanhoPacote, i);
                EntityCollection atualizar = ImportaParaCrm(lead, "lead");
                AtualizaCrmOrigem(atualizar);
                Console.WriteLine($"Pacote nº: {i + 1} importado para lead!");
            }
            cronometro.Stop();
            Console.WriteLine("Tempo de importação e atualização: {0:hh\\:mm\\:ss}\n", cronometro.Elapsed);

            //Importa Ordens!
            cronometro.Start();
            EntityCollection ordens = RetornaEntidades("salesorder");
            loop = (int)Math.Ceiling((float)ordens.Entities.Count / tamanhoPacote);
            for (int i = 0; i < loop; i++)
            {
                EntityCollection salesorder = ImportaSalesorder(ordens, tamanhoPacote, i);
                EntityCollection atualizar = ImportaParaCrm(salesorder, "salesorder");
                AtualizaCrmOrigem(atualizar);
                Console.WriteLine($"Pacote nº: {i + 1} importado para salesorder!");
            }
            cronometro.Stop();
            Console.WriteLine("Tempo de importação e atualização: {0:hh\\:mm\\:ss}\n", cronometro.Elapsed);

            // Importa Produtos da Ordem!
            cronometro.Start();
            EntityCollection produtosDaOrdem = RetornaEntidades("salesorderdetail");
            loop = (int)Math.Ceiling((float)produtosDaOrdem.Entities.Count / tamanhoPacote);
            for (int i = 0; i < loop; i++)
            {
                EntityCollection salesorderdetail = ImportaSalesorderdetail(produtosDaOrdem, tamanhoPacote, i);
                EntityCollection atualizar = ImportaParaCrm(salesorderdetail, "salesorderdetail");
                AtualizaCrmOrigem(atualizar);
                Console.WriteLine($"Pacote nº: {i + 1} importado para salesorderdetail!");
            }
            cronometro.Stop();
            Console.WriteLine("Tempo de importação e atualização: {0:hh\\:mm\\:ss}\n", cronometro.Elapsed);

            // Os abaixo não devem ser necessarios uma vez que ja foram importados!

            //// Importa Unidade
            //EntityCollection unidade = RetornaEntidades("uom", "item");
            //EntityCollection uom = ImportaUom(unidade);
            //ImportaParaCrm(uom, "uom");
            //Console.WriteLine($"Pacote importado para uom!\n");

            ////Importa Lista de Preços
            //EntityCollection listaDePreços = RetornaEntidades("pricelevel", "Default");
            //EntityCollection pricelevel = ImportaPricelevel(listaDePreços);
            //ImportaParaCrm(pricelevel, "pricelevel");
            //Console.WriteLine($"Pacote importado para pricelevel!\n");

            ////Importa Grupo de Unidades
            //EntityCollection grupoDeUnidades = RetornaEntidades("uomschedule", "Default Unit - Sales Professional Business");
            //EntityCollection uomschedule = ImportaUomschedule(grupoDeUnidades);
            //ImportaParaCrm(uomschedule, "uomschedule");
            //Console.WriteLine($"Pacote importado para uomschedule!\n");

            ////Importa Produtos
            //EntityCollection produtos = RetornaEntidades("product", "Notebook Lenovo");
            //EntityCollection product = ImportaProduct(produtos);
            //ImportaParaCrm(product, "product");
            //Console.WriteLine($"Pacote importado para product!\n");
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
        static EntityCollection ImportaUom(EntityCollection query)
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
        static EntityCollection ImportaPricelevel(EntityCollection query)
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
        static EntityCollection ImportaUomschedule(EntityCollection query)
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
        static EntityCollection ImportaProduct(EntityCollection query)
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