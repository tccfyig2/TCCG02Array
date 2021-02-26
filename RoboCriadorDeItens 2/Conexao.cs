using Microsoft.Xrm.Tooling.Connector;
using System.Net;

namespace RoboCriadorDeItens_2
{
    class Conexao
    {
        private static CrmServiceClient crmServiceClientOrigem;
        private static CrmServiceClient crmServiceClientDestino;

        public CrmServiceClient ObterConexaoApresentacao()
        {
            var connectionStringCRM = @"AuthType=OAuth;
            Username = admin@fyitcc.onmicrosoft.com;
            Password = Mortadela2; SkipDiscovery = True;
            AppId = 51f81489-12ee-4a9e-aaae-a2591f45987d;
            RedirectUri = app://58145B91-0C36-4500-8554-080854F2AC97;
            Url = https://fyitccv1.api.crm.dynamics.com/XRMServices/2011/Organization.svc;";

            if (crmServiceClientOrigem == null)
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                crmServiceClientOrigem = new CrmServiceClient(connectionStringCRM);
            }
            return crmServiceClientOrigem;
        }

        public CrmServiceClient ObterConexaoCobaia()
        {
            var connectionStringCRM = @"AuthType=OAuth;
                Username = adminrobo@adminrobo.onmicrosoft.com;
                Password = TCCabc123321*; SkipDiscovery = True;
                AppId = 51f81489-12ee-4a9e-aaae-a2591f45987d;
                RedirectUri = app://58145B91-0C36-4500-8554-080854F2AC97;
                Url = https://tccg03.api.crm2.dynamics.com/XRMServices/2011/Organization.svc;";

            if (crmServiceClientDestino == null)
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                crmServiceClientDestino = new CrmServiceClient(connectionStringCRM);
            }
            return crmServiceClientDestino;
        }
    }
}