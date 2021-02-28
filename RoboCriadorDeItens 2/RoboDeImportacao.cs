﻿using Microsoft.Xrm.Sdk;
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
            CrmServiceClient serviceProxyDestino = conexao.ObterConexaoCobaia();


            //Gera Query em "contact"
            EntityCollection contas = RetornarMultiplo(serviceProxyOrigem, "contact");
            //Importa Contato de um CRM e cria os mesmos dados em outro CRM
            ImportaContato(serviceProxyOrigem, serviceProxyDestino, contas);

            //Gera Query em "account"
            contas = RetornarMultiplo(serviceProxyOrigem,"account");
            //Importa Conta de um CRM e cria os mesmos dados em outro CRM
            ImportaContato(serviceProxyOrigem,serviceProxyDestino,contas);

            //Gera Query em "salesorder"
            contas = RetornarMultiplo(serviceProxyOrigem, "salesorder");
            //Importa Ordem de um CRM e cria os mesmos dados em outro CRM
            ImportaContato(serviceProxyOrigem, serviceProxyDestino, contas);

            //Gera Query em "salesorderdetail"
            contas = RetornarMultiplo(serviceProxyOrigem, "salesorderdetail");
            //Importa Produto da Ordem de um CRM e cria os mesmos dados em outro CRM
            ImportaContato(serviceProxyOrigem, serviceProxyDestino, contas);

            //Gera Query em "pricelevel"
            contas = RetornarMultiplo(serviceProxyOrigem, "pricelevel");
            //Importa Lista de Preço de um CRM e cria os mesmos dados em outro CRM
            ImportaContato(serviceProxyOrigem, serviceProxyDestino, contas);


        }





        static EntityCollection RetornarMultiplo(CrmServiceClient serviceProxyOrigem, string entidade)
            {
                QueryExpression queryExpression = new QueryExpression(entidade);

                //queryExpression.Criteria.AddCondition("name", ConditionOperator.Equal, "teste");
                queryExpression.ColumnSet = new ColumnSet(true);
                EntityCollection colecaoEntidades = serviceProxyOrigem.RetrieveMultiple(queryExpression);
                foreach (var item in colecaoEntidades.Entities)
                {
                    Console.WriteLine(item["name"]);
                }

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
                    
                    serviceProxyDestino.Create(entidade);
                    i++;
                }
            }
            static void ImportaConta(CrmServiceClient serviceProxyOrigem, CrmServiceClient serviceProxyDestino, EntityCollection contas)
            {
                int i = 0;
                foreach (var conta in contas.Entities)
                {
                    var entidade = new Entity("account");
                    entidade.Attributes.Add("name", conta["name"].ToString());
                    entidade.Attributes.Add("crb79_cnpj", conta["cred2_cnpj"].ToString());
                    entidade.Attributes.Add("telephone1", conta["telephone1"].ToString());
                    entidade.Attributes.Add("emailaddress1", conta["address1_postalcode"].ToString());
                    entidade.Attributes.Add("address1_postalcode", conta["address1_postalcode"].ToString());
                    entidade.Attributes.Add("address1_line1", conta["address1_line1"].ToString());
                    entidade.Attributes.Add("address1_line2", conta["address1_line2"].ToString());
                    entidade.Attributes.Add("address1_line3", conta["address1_line3"].ToString());
                    entidade.Attributes.Add("address1_city", conta["address1_city"].ToString());
                    entidade.Attributes.Add("address1_stateorprovince", conta["address1_stateorprovince"].ToString());
                    entidade.Attributes.Add("address1_country", "Brasil");
                    entidade.Attributes.Add("primarycontactid", new EntityReference("contact", (Guid) conta["contactId"]));

                    serviceProxyDestino.Create(entidade);
                    i++;
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
                   
                    serviceProxyDestino.Create(entidade);
                    i++;
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



                    serviceProxyDestino.Create(entidade);
                    i++;
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


                serviceProxyDestino.Create(entidade);
                    i++;
                }
            }


            static void ImportaListaPrecos(CrmServiceClient serviceProxyOrigem, CrmServiceClient serviceProxyDestino, EntityCollection contas)
            {
    
                int i = 0;
                foreach (var conta in contas.Entities)
                {
                    var entidade = new Entity("pricelevel");
                    entidade.Attributes.Add("name", conta["name"].ToString());
                    entidade.Attributes.Add("begindate", conta["begindate"].ToString());
                    entidade.Attributes.Add("enddate", conta["enddate"].ToString());

                    serviceProxyDestino.Create(entidade);
                    i++;
                }
            }
    }
    }

