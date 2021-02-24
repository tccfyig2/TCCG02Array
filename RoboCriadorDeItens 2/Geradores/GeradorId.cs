using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;

namespace RoboCriadorDeItens_2.Geradores
{
    class GeradorId : ListaPersonalizada
    {
        static List<ListaPersonalizada> IdClientePotencial;
        static EntityCollection RetornarMultiplo(CrmServiceClient serviceProxy)
        {
            QueryExpression queryExpression = new QueryExpression("lead");
            queryExpression.Criteria.AddCondition("leadid", ConditionOperator.NotNull);
            queryExpression.ColumnSet = new ColumnSet(true);
            EntityCollection colecaoEntidades = serviceProxy.RetrieveMultiple(queryExpression);

            IdClientePotencial = new List<ListaPersonalizada>();
            foreach (var item in colecaoEntidades.Entities)
            {
                IdClientePotencial.Add(new ListaPersonalizada(item["leadid"].ToString()));
            }
            Console.WriteLine(IdClientePotencial.Count);
            Console.WriteLine(IdClientePotencial[IdClientePotencial.Count - 1].Id);
            Console.WriteLine(IdClientePotencial[3].Id);
            return colecaoEntidades;
        }

    }
}
