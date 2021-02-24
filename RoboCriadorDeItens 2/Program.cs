using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using RoboCriadorDeItens_2.Geradores;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Client;


namespace RoboCriadorDeItens_2
{
    class Program : ListaPersonalizada
    {
        static void Main(string[] args)
        {
            Conexao conexao = new Conexao();

            //CRM de Origem
            var serviceProxyOrigem = conexao.Obter();

            //criaContato(serviceProxyOrigem);

            //criaConta(serviceProxyOrigem);

            //criaClientePetencial(serviceProxyOrigem);

            criaOrdem(serviceProxyOrigem);

            //criaProduto(serviceProxyOrigem);

            //RetornaPriceLevelId(serviceProxyOrigem);

            //criaListaPrecos(serviceProxyOrigem);

            //RetornaLeadId(serviceProxyOrigem);
            Console.WriteLine("Fim!");
            Console.ReadLine();
        }

        static EntityCollection RetornaPriceLevelId(CrmServiceClient serviceProxyOrigem)
        {
            QueryExpression queryExpression = new QueryExpression("pricelevel");

            queryExpression.Criteria.AddCondition("pricelevelid", ConditionOperator.NotNull);
            queryExpression.ColumnSet = new ColumnSet(true);
            EntityCollection colecaoEntidades = serviceProxyOrigem.RetrieveMultiple(queryExpression);
            foreach (var item in colecaoEntidades.Entities)
            {
                Console.WriteLine(item["pricelevelid"]);
            }

            return colecaoEntidades;
        }
        static void criaContato(CrmServiceClient serviceProxy)
        {
            for (int i = 0; i < 1; i++)
            {
                var entidade = new Entity("contact");
                Guid registro = new Guid();

                string[] endereco = (GeradorEndereco.geradorEndereco());
                entidade.Attributes.Add("address1_postalcode", endereco[0]);
                entidade.Attributes.Add("address1_line1", endereco[1]);
                entidade.Attributes.Add("address1_line2", endereco[2]);
                entidade.Attributes.Add("address1_line3", endereco[3]);
                entidade.Attributes.Add("address1_city", endereco[4]);
                entidade.Attributes.Add("address1_stateorprovince", endereco[5]);
                entidade.Attributes.Add("address1_country", "Brasil");

                string emailNome = (GeradorNome_Sobrenome.geradorNome());
                entidade.Attributes.Add("emailaddress1", GeradorEmail.geradorEmail(emailNome));
                entidade.Attributes.Add("firstname", emailNome);
                entidade.Attributes.Add("lastname", GeradorNome_Sobrenome.geradorSobrenome());
                entidade.Attributes.Add("cred2_cpf", GeradorCPF_CNPJ.geradorCPF());
                entidade.Attributes.Add("telephone1", GeradorTelefone_Topico.geredorTelefone(endereco[5]));
                entidade.Attributes.Add("mobilephone", GeradorTelefone_Topico.geredorTelefone(endereco[5]));

                registro = serviceProxy.Create(entidade);
            }
        }
        static void criaConta(CrmServiceClient serviceProxy)
        {
            for (int i = 0; i < 1; i++)
            {
                var entidade = new Entity("account");
                Guid registro = new Guid();

                string[] endereco = (GeradorEndereco.geradorEndereco());
                entidade.Attributes.Add("address1_postalcode", endereco[0]);
                entidade.Attributes.Add("address1_line1", endereco[1]);
                entidade.Attributes.Add("address1_line2", endereco[2]);
                entidade.Attributes.Add("address1_line3", endereco[3]);
                entidade.Attributes.Add("address1_city", endereco[4]);
                entidade.Attributes.Add("address1_stateorprovince", endereco[5]);
                entidade.Attributes.Add("address1_country", "Brasil");

                entidade.Attributes.Add("name" + " ltda.", GeradorNome_Sobrenome.geradorSobrenome());
                entidade.Attributes.Add("cred2_cnpj", GeradorCPF_CNPJ.geradorCNPJ());
                entidade.Attributes.Add("telephone1", GeradorTelefone_Topico.geredorTelefone(endereco[5]));

                registro = serviceProxy.Create(entidade);
            }
        }
        static void criaClientePetencial(CrmServiceClient serviceProxy)
        {
            for (int i = 0; i < 1; i++)
            {
                var entidade = new Entity("lead");
                Guid registro = new Guid();

                string[] endereco = (GeradorEndereco.geradorEndereco());
                entidade.Attributes.Add("address1_postalcode", endereco[0]);
                entidade.Attributes.Add("address1_line1", endereco[1]);
                entidade.Attributes.Add("address1_line2", endereco[2]);
                entidade.Attributes.Add("address1_line3", endereco[3]);
                entidade.Attributes.Add("address1_city", endereco[4]);
                entidade.Attributes.Add("address1_stateorprovince", endereco[5]);
                entidade.Attributes.Add("address1_country", "Brasil");

                string emailNome = (GeradorNome_Sobrenome.geradorNome());
                entidade.Attributes.Add("emailaddress1", GeradorEmail.geradorEmail(emailNome));
                entidade.Attributes.Add("firstname", emailNome);
                entidade.Attributes.Add("lastname", GeradorNome_Sobrenome.geradorSobrenome());
                entidade.Attributes.Add("companyname", GeradorNome_Sobrenome.geradorSobrenome() + " ltda.");
                entidade.Attributes.Add("subject", GeradorTelefone_Topico.geredorTopico());
                entidade.Attributes.Add("telephone1", GeradorTelefone_Topico.geredorTelefone(endereco[5]));
                entidade.Attributes.Add("mobilephone", GeradorTelefone_Topico.geredorTelefone(endereco[5]));

                registro = serviceProxy.Create(entidade);
            }
        }
        static void criaOrdem(CrmServiceClient serviceProxy)
        {
            // Lista de Preços(pricelevelid) CAMPO PESQUISAVEL
            for (int i = 0; i < 1; i++)
            {
                var entidade = new Entity("salesorder");
                Guid registro = new Guid();

                entidade.Attributes.Add("name", $"Cliente nº: {i}");
                entidade.Attributes.Add("customerid", new EntityReference("lead", GeradorId.BuscaId(serviceProxy)));
                //entidade.Attributes.Add("cred2_codigo", GeradorOrdem.geradorCod());
                //entidade.Attributes.Add("ordernumber", GeradorOrdem.geradorOrdemNumber());

                registro = serviceProxy.Create(entidade);
            }
        }

        static void criaProduto(CrmServiceClient serviceProxy)
        {
            // OBG: Nome(name)
            // ID do produto(productnumber)
            // Grupo de Unidades(defaultuomscheduleid
            // Unidade Padrão(defaultuomid)
            // Oferece Suporte a Decimais(quantitydecimal)
            // Recomendado: Lista de Preços Padrão(pricelevelid)

            for (int i = 0; i < 1; i++)
            {
                var entidade = new Entity("salesorder");
                Guid registro = new Guid();

                entidade.Attributes.Add("name", $"Cliente nº: {i}");

                registro = serviceProxy.Create(entidade);
            }
        }
        static void criaListaPrecos(CrmServiceClient serviceProxy)
        {
            for (int i = 0; i < 2; i++)
            {
                var entidade = new Entity("pricelevel");
                Guid registro = new Guid();

                entidade.Attributes.Add("name", $"Produto {i}");
                entidade.Attributes.Add("begindate", DateTime.Today);
                entidade.Attributes.Add("enddate", DateTime.Today.AddYears(1));


                registro = serviceProxy.Create(entidade);
            }
        }

    }
}
