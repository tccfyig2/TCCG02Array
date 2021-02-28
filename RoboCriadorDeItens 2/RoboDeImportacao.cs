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
        internal static void importacao()
        {
            Conexao conexao = new Conexao();
            CrmServiceClient serviceProxyOrigem = conexao.ObterConexaoCobaia();
            CrmServiceClient serviceProxyDestino = conexao.ObterConexaoApresentacao();


            //Gera Query em "contact"
            EntityCollection contas = RetornarMultiplo(serviceProxyOrigem, "contact");
            //Importa Contato de um CRM e cria os mesmos dados em outro CRM
            ImportaContato(serviceProxyOrigem, serviceProxyDestino, contas);

            //Gera Query em "account"
            contas = RetornarMultiplo(serviceProxyOrigem,"account");
            //Importa Conta de um CRM e cria os mesmos dados em outro CRM
            ImportaConta(serviceProxyOrigem,serviceProxyDestino,contas);

            //Gera Query em "salesorder"
            contas = RetornarMultiplo(serviceProxyOrigem, "lead");
            //Importa Ordem de um CRM e cria os mesmos dados em outro CRM
            ImportaContatoPotencial(serviceProxyOrigem, serviceProxyDestino, contas);

            //Gera Query em "salesorder"
            contas = RetornarMultiplo(serviceProxyOrigem, "salesorder");
            //Importa Ordem de um CRM e cria os mesmos dados em outro CRM
            ImportaOrdem(serviceProxyOrigem, serviceProxyDestino, contas);

            //Gera Query em "salesorderdetail"
            contas = RetornarMultiplo(serviceProxyOrigem, "salesorderdetail");
            //Importa Produto da Ordem de um CRM e cria os mesmos dados em outro CRM
            ImportaProdutoOrdem(serviceProxyOrigem, serviceProxyDestino, contas);

            //Gera Query em "pricelevel"
            contas = RetornarMultiplo(serviceProxyOrigem, "pricelevel");
            //Importa Lista de Preço de um CRM e cria os mesmos dados em outro CRM
            ImportaListaPrecos(serviceProxyOrigem, serviceProxyDestino, contas);


        }

        static void QueryExpression(CrmServiceClient serviceProxyOrigem)
        {
            QueryExpression queryExpression = new QueryExpression("account");
            queryExpression.ColumnSet = new ColumnSet(true);

            ConditionExpression condicao = new ConditionExpression("address1_city", ConditionOperator.Equal, "Natal");
            queryExpression.Criteria.AddCondition(condicao);

            LinkEntity link = new LinkEntity("account", "contact", "primarycontactid", "contactid", JoinOperator.Inner);
            link.Columns = new ColumnSet("firstname", "lastname");
            link.EntityAlias = "Contato";

            queryExpression.LinkEntities.Add(link);

            EntityCollection colecaoEntidades = serviceProxyOrigem.RetrieveMultiple(queryExpression);
            foreach (var entidade in colecaoEntidades.Entities)
            {
                Console.WriteLine("Id: " + entidade.Id);
                Console.WriteLine("Nome conta " + entidade["name"]);
                Console.WriteLine("Nome Contato " + ((AliasedValue)entidade["Contato.firstname"]).Value);
                Console.WriteLine("Sobrenome Contato " + ((AliasedValue)entidade["Contato.lastname"]).Value);
            }

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

            static void ImportaContato(CrmServiceClient serviceProxyOrigem, CrmServiceClient serviceProxyDestino, EntityCollection contas)
            {
                int i = 0;
                foreach (var conta in contas.Entities)
                {
                    var entidade = new Entity("contact");
                    entidade.Attributes.Add("firstname", conta["firstname"].ToString());
                    entidade.Attributes.Add("lastname", conta["lastname"].ToString());
                    entidade.Attributes.Add("crb79_cpf", conta["crb79_cpf"].ToString());
                    entidade.Attributes.Add("mobilephone", conta["mobilephone"].ToString());
                    entidade.Attributes.Add("emailaddress1", conta["address1_postalcode"].ToString());
                    entidade.Attributes.Add("address1_postalcode", conta["address1_postalcode"].ToString());
                    entidade.Attributes.Add("address1_line1", conta["address1_line1"].ToString());
                    entidade.Attributes.Add("address1_line2", conta["address1_line2"].ToString());
                    entidade.Attributes.Add("address1_line3", conta["address1_line3"].ToString());
                    entidade.Attributes.Add("address1_city", conta["address1_city"].ToString());
                    entidade.Attributes.Add("address1_stateorprovince", conta["address1_stateorprovince"].ToString());
                    entidade.Attributes.Add("address1_country", "Brasil");
                    
                    //serviceProxyDestino.Create(entidade);
                    i++;
                    Console.WriteLine($"existem {i} Contatos");
                }
            }
            static void ImportaConta(CrmServiceClient serviceProxyOrigem, CrmServiceClient serviceProxyDestino, EntityCollection contas)
            {
                int i = 0;
                foreach (var conta in contas.Entities)
                {
                    var entidade = new Entity("account");
                    entidade.Attributes.Add("name", conta["name"].ToString());
                    entidade.Attributes.Add("crb79_cnpj", conta["crb79_cnpj"].ToString());
                    entidade.Attributes.Add("telephone1", conta["telephone1"].ToString());
                    entidade.Attributes.Add("emailaddress1", conta["address1_postalcode"].ToString());
                    entidade.Attributes.Add("address1_postalcode", conta["address1_postalcode"].ToString());
                    entidade.Attributes.Add("address1_line1", conta["address1_line1"].ToString());
                    entidade.Attributes.Add("address1_line2", conta["address1_line2"].ToString());
                    entidade.Attributes.Add("address1_line3", conta["address1_line3"].ToString());
                    entidade.Attributes.Add("address1_city", conta["address1_city"].ToString());
                    entidade.Attributes.Add("address1_stateorprovince", conta["address1_stateorprovince"].ToString());
                    entidade.Attributes.Add("address1_country", "Brasil");
                    EntityCollection contact = RetornarMultiplo(serviceProxyOrigem, "contact");
                    //entidade.Attributes.Add("primarycontactid", new EntityReference("contact", (Guid) contact["contactid"]));

                    //serviceProxyDestino.Create(entidade);
                    i++;
                    Console.WriteLine($"existem {i} Contas");
                }
            }

            static void ImportaContatoPotencial(CrmServiceClient serviceProxyOrigem, CrmServiceClient serviceProxyDestino, EntityCollection contas)
            {
                int i = 0;
                foreach (var conta in contas.Entities)
                {
                    var entidade = new Entity("lead");
                    entidade.Attributes.Add("firstname", conta["firstname"].ToString());
                    entidade.Attributes.Add("lastname", conta["lastname"].ToString());
                    entidade.Attributes.Add("companyname", conta["companyname"].ToString());
                    entidade.Attributes.Add("mobilephone", conta["mobilephone"].ToString());
                    entidade.Attributes.Add("telephone1", conta["telephone1"].ToString());
                    entidade.Attributes.Add("subject", conta["subject"].ToString());
                    entidade.Attributes.Add("emailaddress1", conta["address1_postalcode"].ToString());
                    entidade.Attributes.Add("address1_postalcode", conta["address1_postalcode"].ToString());
                    entidade.Attributes.Add("address1_line1", conta["address1_line1"].ToString());
                    entidade.Attributes.Add("address1_line2", conta["address1_line2"].ToString());
                    entidade.Attributes.Add("address1_line3", conta["address1_line3"].ToString());
                    entidade.Attributes.Add("address1_city", conta["address1_city"].ToString());
                    entidade.Attributes.Add("address1_stateorprovince", conta["address1_stateorprovince"].ToString());
                    entidade.Attributes.Add("address1_country", "Brasil");

                    //serviceProxyDestino.Create(entidade);
                    i++;
                    Console.WriteLine($"existem {i} Clientes Potenciais");
                }
            }

            static void ImportaOrdem(CrmServiceClient serviceProxyOrigem, CrmServiceClient serviceProxyDestino, EntityCollection contas)
            {
                int i = 0;
                foreach (var conta in contas.Entities)
                {
                    var entidade = new Entity("salesorder");
                    entidade.Attributes.Add("name", conta["name"].ToString());
                    entidade.Attributes.Add("customerid", new EntityReference("account", (Guid)conta["accountId"]));



                    //serviceProxyDestino.Create(entidade);
                    i++;
                    Console.WriteLine($"existem {i} Ordens");
                }
            }


            static void ImportaProdutoOrdem(CrmServiceClient serviceProxyOrigem, CrmServiceClient serviceProxyDestino, EntityCollection contas)
            {
                    Guid prduct = new Guid("4190122b-0477-eb11-a812-000d3a1c6462");
                    Guid uomid = new Guid("5f753633-aa6e-eb11-b0b2-000d3a55dda2");
                    int i = 0;
                    foreach (var conta in contas.Entities)
                    {
                    var entidade = new Entity("salesorderdetail");
                    entidade.Attributes.Add("productid", new EntityReference("product", prduct));
                    entidade.Attributes.Add("salesorderid", new EntityReference("salesorder", (Guid)conta["salesorderid"]));
                    entidade.Attributes.Add("uomid", new EntityReference("businessunit", uomid));


                    //serviceProxyDestino.Create(entidade);
                    i++;
                    Console.WriteLine($"existem {i} Produtos da Ordem");
                }
            }


            static void ImportaListaPrecos(CrmServiceClient serviceProxyOrigem, CrmServiceClient serviceProxyDestino, EntityCollection contas)
            {
    
                int i = 0;
                foreach (var conta in contas.Entities)
                {
                    var entidade = new Entity("pricelevel");
                    entidade.Attributes.Add("name", conta["name"].ToString());
                    entidade.Attributes.Add("begindate", DateTime.Today);
                    entidade.Attributes.Add("enddate", DateTime.Today.AddYears(1));

                    //serviceProxyDestino.Create(entidade);
                    i++;
                    Console.WriteLine($"existem {i} Lista de Preços");
                }
            }
    }
    }

