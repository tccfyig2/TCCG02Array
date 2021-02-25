using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;

namespace RoboCriadorDeItens_2.Geradores
{
    class GeradorId : ListaPersonalizada
    {
        protected static Random rnd = new Random();
        protected static List<ListaPersonalizada> IdClientePotencial;
        internal static Guid BuscaId(CrmServiceClient serviceProxy)
        {
            QueryExpression queryExpression = new QueryExpression("account");
            queryExpression.Criteria.AddCondition("accountid", ConditionOperator.NotNull);
            queryExpression.ColumnSet = new ColumnSet(true);
            EntityCollection colecaoEntidades = serviceProxy.RetrieveMultiple(queryExpression);

            IdClientePotencial = new List<ListaPersonalizada>();
            foreach (var item in colecaoEntidades.Entities)
            {
                IdClientePotencial.Add(new ListaPersonalizada(item.Id));
            }
            return IdClientePotencial[rnd.Next(0, IdClientePotencial.Count)].Identidade;

        }

    }
}
