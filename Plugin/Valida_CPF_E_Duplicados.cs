using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace Plugin
{
    public class Valida_CPF_E_Duplicados : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            IOrganizationServiceFactory servicefactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = servicefactory.CreateOrganizationService(context.UserId);

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                Entity entity = (Entity)context.InputParameters["Target"];
                // Evita loop
                if (context.Depth > 1)
                {
                    return;
                }
                // Se verificação já foi feira pelo JavaScript não refaça as verificações.
                if (entity.GetAttributeValue<string>("cred2_verificado").ToString() != "true")
                {
                    string campo = "cred2_cpf";
                    try
                    {
                        if (!entity.Attributes.Contains(campo))
                        {
                            throw new InvalidPluginExecutionException("CPF é obigatório!");
                        }
                        else
                        {
                            string cpf = entity.GetAttributeValue<string>(campo).ToString();
                            if (validacaoCPF(cpf))
                            {
                                QueryExpression contactQuery = new QueryExpression("contact");
                                contactQuery.ColumnSet = new ColumnSet(campo);
                                contactQuery.Criteria.AddCondition(campo, ConditionOperator.Equal, cpf);
                                EntityCollection contactColl = service.RetrieveMultiple(contactQuery);
                                if (contactColl.Entities.Count > 0)
                                {
                                    throw new InvalidPluginExecutionException("CPF já cadastrado!");
                                }
                            }
                            else
                            {
                                throw new InvalidPluginExecutionException("CPF é Inválido!");
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
        public static bool validacaoCPF(string cpfCampo)
        {
            cpfCampo = cpfCampo.Trim();
            cpfCampo = cpfCampo.Replace(".", "").Replace("-", "");
            if (cpfCampo.Length != 11 ||
                cpfCampo == "00000000000" ||
                cpfCampo == "11111111111" ||
                cpfCampo == "22222222222" ||
                cpfCampo == "33333333333" ||
                cpfCampo == "44444444444" ||
                cpfCampo == "55555555555" ||
                cpfCampo == "66666666666" ||
                cpfCampo == "77777777777" ||
                cpfCampo == "88888888888" ||
                cpfCampo == "99999999999")
            {
                return false;
            }

            int[] multpDigito1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multpDigito2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] cpf = new int[11];
            for (int i = 0; i < 9; i++)
            {
                cpf[i] = (int)Char.GetNumericValue(cpfCampo[i]);
            }

            // Calculo primeiro dígito
            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += multpDigito1[i] * cpf[i];
            }

            cpf[9] = Resto(soma);

            // Calculo segundo dígito
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += multpDigito2[i] * cpf[i];
            }

            cpf[10] = Resto(soma);

            if (cpfCampo == string.Join("", cpf))
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