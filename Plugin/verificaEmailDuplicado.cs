using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace Plugin
{
    class verificaEmailDuplicado : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            IOrganizationServiceFactory servicefactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = servicefactory.CreateOrganizationService(context.UserId);

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                Entity entity = (Entity)context.InputParameters["Target"];
                if (entity.LogicalName != "account")
                {
                    return;
                }
                // Evita loop
                if (context.Depth > 1)
                {
                    return;
                }
                try
                {
                    if (entity.Attributes.Contains("emailaddress1"))
                    {
                        string email = entity.GetAttributeValue<string>("emailaddress1").ToString();
                        QueryExpression contactQuery = new QueryExpression("account");
                        contactQuery.ColumnSet = new ColumnSet("emailaddress1");
                        // Se conter cnpj, adicione email em contactQuery.
                        contactQuery.Criteria.AddCondition("emailaddress1", ConditionOperator.Equal, email);
                        // Transforma contactQuery em uma entidade.
                        EntityCollection contactColl = service.RetrieveMultiple(contactQuery);
                        // contactColl não deve conter nada.
                        if (contactColl.Entities.Count > 0)
                            throw new InvalidPluginExecutionException("E-Mail já consta no banco de dados!");
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }
    }
}
