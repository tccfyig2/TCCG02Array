using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using RoboCriadorDeItens_2.DAL;

namespace RoboCriadorDeItens_2.MODEL
{
    class TestePlugin : Query
    {
        static CrmServiceClient serviceProxyDestino = Conexao.Apresentacao();
        internal static void PluginCPFInvalido()
        {
            Entity entidade = new Entity("contact");
            entidade.Attributes.Add("cred2_cpf", "38561393466");
            TesteDePlugin(serviceProxyDestino, entidade);
        }
        internal static void PluginCPFDuplicado()
        {
            Entity entidade = new Entity("contact");
            entidade.Attributes.Add("cred2_cpf", "38561393467");
            TesteDePlugin(serviceProxyDestino, entidade);
        }
        internal static void PluginCNPJInvalido()
        {
            Entity entidade = new Entity("account");
            entidade.Attributes.Add("cred2_cnpj", "70865518919501");
            TesteDePlugin(serviceProxyDestino, entidade);
        }
        internal static void PluginCNPJDuplicado()
        {
            Entity entidade = new Entity("account");
            entidade.Attributes.Add("cred2_cnpj", "70865518919509");
            TesteDePlugin(serviceProxyDestino, entidade);
        }
        internal static void PluginCodigoDuplicado()
        {
            Entity entidade = new Entity("salesorder");
            entidade.Attributes.Add("cred2_codigo", "cod-10000");
            TesteDePlugin(serviceProxyDestino, entidade);
        }
    }
}