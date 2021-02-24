﻿using Microsoft.Xrm.Sdk;
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
            QueryExpression queryExpression = new QueryExpression("lead");
            queryExpression.Criteria.AddCondition("leadid", ConditionOperator.NotNull);
            queryExpression.ColumnSet = new ColumnSet(true);
            EntityCollection colecaoEntidades = serviceProxy.RetrieveMultiple(queryExpression);

            IdClientePotencial = new List<ListaPersonalizada>();
            foreach (var item in colecaoEntidades.Entities)
            {
                IdClientePotencial.Add(new ListaPersonalizada(item.Id));
            }
            var f =  IdClientePotencial[rnd.Next(IdClientePotencial.Count)].Id;
            return f;
           
        }

    }
}
