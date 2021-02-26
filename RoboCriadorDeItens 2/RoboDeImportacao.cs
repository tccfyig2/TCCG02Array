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
 
            static EntityCollection RetornarMultiplo(CrmServiceClient serviceProxyOrigem)
            {
                QueryExpression queryExpression = new QueryExpression("account");

                //queryExpression.Criteria.AddCondition("name", ConditionOperator.Equal, "teste");
                queryExpression.ColumnSet = new ColumnSet(true);
                EntityCollection colecaoEntidades = serviceProxyOrigem.RetrieveMultiple(queryExpression);
                foreach (var item in colecaoEntidades.Entities)
                {
                    Console.WriteLine(item["name"]);
                }

                return colecaoEntidades;
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
    }
    }

