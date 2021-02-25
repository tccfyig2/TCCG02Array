using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;

namespace RoboCriadorDeItens_2.Geradores
{
    public class listaId
    {
        public listaId() { }

        public Guid Id { get; set; }

        public listaId(Guid id)
        {
            this.Id = id;
        }
    }
    class GeradorId : ListaId
    {
        protected static Random rnd = new Random();
        protected static List<listaId> accountId = new List<listaId>();
        internal static Guid BuscaId(CrmServiceClient serviceProxy)
        {
            QueryExpression queryExpression = new QueryExpression("account");
            queryExpression.Criteria.AddCondition("accountid", ConditionOperator.NotNull);
            queryExpression.ColumnSet = new ColumnSet(true);
            EntityCollection colecaoEntidades = serviceProxy.RetrieveMultiple(queryExpression);

            foreach (var item in colecaoEntidades.Entities)
            {
                accountId.Add(new listaId(item.Id));
            }
            int index = rnd.Next(0, accountId.Count);
            return accountId[index].Id;
        }
    }
}
