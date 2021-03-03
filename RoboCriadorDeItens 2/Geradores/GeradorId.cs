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
    class GeradorId : listaId
    {
        protected static List<listaId> listId = new List<listaId>();
        internal static List<listaId> BuscaId(
            CrmServiceClient _serviceProxy,
            string tabela,
            string campo
            )
        {
            QueryExpression queryExpression = new QueryExpression(tabela);
            queryExpression.ColumnSet = new ColumnSet(true);
            EntityCollection colecaoEntidades = _serviceProxy.RetrieveMultiple(queryExpression);

            foreach (var item in colecaoEntidades.Entities)
            {
                listId.Add(new listaId(item.Id));
            }
            return listId;
        }
    }
}
