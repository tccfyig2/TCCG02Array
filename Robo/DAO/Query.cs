using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Windows;

namespace Robo.DAO
{
    class Query
    {
        static CrmServiceClient serviceProxyOrigem = Conexao.Cobaia();
        static CrmServiceClient serviceProxyDestino = Conexao.Apresentacao();
        protected static EntityCollection RetornaEntidades(string entidade, string condicao = null)
        {
            QueryExpression queryExpression = new QueryExpression(entidade);
            if (condicao != null)
            {
                queryExpression.Criteria.AddCondition("name", ConditionOperator.Equal, condicao);
            }
            else
            {
                queryExpression.Criteria.AddCondition("crb79_importado", ConditionOperator.Equal, false);
            }
            queryExpression.ColumnSet = new ColumnSet(true);

            EntityCollection itens = new EntityCollection();
            queryExpression.PageInfo = new PagingInfo();
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
        protected static EntityCollection RetornaEntidadesLink(string entidade)
        {
            QueryExpression queryExpression = new QueryExpression(entidade);
            queryExpression.Criteria.AddCondition("crb79_importado", ConditionOperator.Equal, false);
            queryExpression.ColumnSet = new ColumnSet(true);

            LinkEntity link = new LinkEntity("account", "contact", "primarycontactid", "contactid", JoinOperator.Inner);
            link.Columns = new ColumnSet(true);
            link.EntityAlias = "Contato";
            queryExpression.LinkEntities.Add(link);

            EntityCollection itens = new EntityCollection();
            queryExpression.PageInfo = new PagingInfo();
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
        protected static EntityCollection ImportaParaCrm(EntityCollection colecaoEntidades, string tabela)
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
            ExecuteMultipleResponse resposta = (ExecuteMultipleResponse)serviceProxyDestino.Execute(request);
            EntityCollection atualizar = new EntityCollection();
            int cont = 0;
            foreach (var item in resposta.Responses)
            {
                if (item.Fault != null)
                {
                    Console.WriteLine($"ERRO na entidade nº: {cont}!\n{item.Fault}");
                }
                else
                {
                    Entity entidade = new Entity(tabela);
                    entidade.Id = (Guid)item.Response.Results["id"];
                    entidade.Attributes.Add("crb79_importado", true);
                    atualizar.Entities.Add(entidade);
                }
                cont++;
            }
            Console.WriteLine($"{cont} entidades importadas!");
            return atualizar;
        }
        protected static void AtualizaCrmOrigem(EntityCollection colecaoEntidades)
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
            ExecuteMultipleResponse resposta = (ExecuteMultipleResponse)serviceProxyOrigem.Execute(request);
            int cont = 0;
            foreach (var item in resposta.Responses)
            {
                if (item.Fault != null)
                {
                    Console.WriteLine($"ERRO na entidade nº: {cont}!\n{item.Fault}");
                }
                cont++;
            }
            Console.WriteLine($"{cont} entidades atualizadas na origem!");
        }
        protected static EntityCollection criaNoCrm(EntityCollection colecaoEntidades, string tabela = null)
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
            ExecuteMultipleResponse resposta = (ExecuteMultipleResponse)serviceProxyOrigem.Execute(request);
            EntityCollection memoria = new EntityCollection();
            int cont = 0;
            foreach (var item in resposta.Responses)
            {
                if (item.Fault != null)
                {
                    Console.WriteLine($"ERRO na entidade nº: {cont}!\n{item.Fault}");
                }
                else if (tabela != null)
                {
                    Entity entidade = new Entity(tabela);
                    entidade.Id = (Guid)item.Response.Results["id"];
                    memoria.Entities.Add(entidade);
                }
                cont++;
            }
            Console.WriteLine($"{cont} entidades criadas!");
            return memoria;
        }
        protected static void TesteDePlugin(Entity entidade)
        {
            try
            {
                serviceProxyDestino.Create(entidade);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected static int LastSalesorderNumber()
        {
            QueryExpression queryExpression = new QueryExpression("salesorder");
            queryExpression.ColumnSet = new ColumnSet(true);
            queryExpression.AddOrder("crb79_codigo", OrderType.Descending);
            queryExpression.TopCount = 1;
            EntityCollection entidade = serviceProxyOrigem.RetrieveMultiple(queryExpression);
            string cod = entidade[0]["crb79_codigo"].ToString().Replace("cod-", "");
            int codigo = int.Parse(cod) + 1;
            return codigo;
        }
    }
}