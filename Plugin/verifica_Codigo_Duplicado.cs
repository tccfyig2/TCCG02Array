using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace Plugin
{
    public class verifica_Codigo_Duplicado : IPlugin
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
                if (entity.LogicalName != "salesorder")
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
                    string campo = "cred2_codigo";
                    if (entity.Attributes.Contains(campo))
                    {
                        string email = entity.GetAttributeValue<string>(campo).ToString();
                        QueryExpression contactQuery = new QueryExpression("salesorder");
                        contactQuery.ColumnSet = new ColumnSet(campo);
                        // Se conter cnpj, adicione email em contactQuery.
                        contactQuery.Criteria.AddCondition(campo, ConditionOperator.Equal, email);
                        // Transforma contactQuery em uma entidade.
                        EntityCollection contactColl = service.RetrieveMultiple(contactQuery);
                        // contactColl não deve conter nada.
                        if (contactColl.Entities.Count > 0)
                            throw new InvalidPluginExecutionException("Código já existe no banco de dados!");
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