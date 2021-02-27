using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace Plugin
{
    public class Valida_CNPJ_E_Duplicados : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory servicefactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = servicefactory.CreateOrganizationService(context.UserId);
            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                Entity entity = (Entity)context.InputParameters["Target"];
                if (context.Depth > 1)
                {
                    return;
                }
                if (entity.GetAttributeValue<string>("cred2_verificado").ToString() != "true")
                {
                    try
                    {
                        string campo = "cred2_cnpj";
                        if (!entity.Attributes.Contains(campo))
                        {
                            throw new InvalidPluginExecutionException("CNPJ é obigatório!");
                        }
                        else
                        {
                            string cnpj = entity.GetAttributeValue<string>(campo).ToString();
                            if (validacaoCNPJ(cnpj))
                            {
                                QueryExpression contactQuery = new QueryExpression("account");
                                contactQuery.ColumnSet = new ColumnSet(campo);
                                contactQuery.Criteria.AddCondition(campo, ConditionOperator.Equal, cnpj);
                                EntityCollection contactColl = service.RetrieveMultiple(contactQuery);
                                if (contactColl.Entities.Count > 0)
                                {
                                    throw new InvalidPluginExecutionException("CNPJ já cadastrado!");
                                }
                            }
                            else
                            {
                                throw new InvalidPluginExecutionException("CNPJ é Inválido!");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                }
            }
        }
        public static bool validacaoCNPJ(string cnpjCampo)
        {
            cnpjCampo = cnpjCampo.Trim();
            cnpjCampo = cnpjCampo.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpjCampo.Length != 14 ||
            cnpjCampo == "00000000000000" ||
            cnpjCampo == "11111111111111" ||
            cnpjCampo == "22222222222222" ||
            cnpjCampo == "33333333333333" ||
            cnpjCampo == "44444444444444" ||
            cnpjCampo == "55555555555555" ||
            cnpjCampo == "66666666666666" ||
            cnpjCampo == "77777777777777" ||
            cnpjCampo == "88888888888888" ||
            cnpjCampo == "99999999999999")
            {
                return false;
            }

            int[] multpDigito1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multpDigito2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] cnpj = new int[14];
            for (int i = 0; i < 12; i++)
            {
                cnpj[i] = (int)Char.GetNumericValue(cnpjCampo[i]);
            }

            // Calculo primeiro dígito
            int soma = 0;
            for (int i = 0; i < 12; i++)
            {
                soma += multpDigito1[i] * cnpj[i];
            }

            cnpj[12] = Resto(soma);

            // Calculo segundo dígito
            soma = 0;
            for (int i = 0; i < 13; i++)
            {
                soma += multpDigito2[i] * cnpj[i];
            }

            cnpj[13] = Resto(soma);

            if (cnpjCampo == string.Join("", cnpj))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int Resto(int soma)
        {
            int resto = soma % 11;
            if (resto < 2)
                return (resto = 0);
            else
                return (resto = 11 - resto);
        }
    }
}
