using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using RoboCriadorDeItens_2.Geradores;
using System.Collections.Generic;
using System;

namespace RoboCriadorDeItens_2
{
    class RoboDeCriacao : listaId
    {
        public static int quantidade = 5;
        internal static void criacao()
        {
            Conexao conexao = new Conexao();

            //CRM de Origem
            var serviceProxyOrigem = conexao.ObterConexaoCobaia();



            //criaContato(serviceProxyOrigem);

            criaConta(serviceProxyOrigem);

            //criaClientePotencial(serviceProxyOrigem);

            //criaOrdem(serviceProxyOrigem);

            //criaListaPrecos(serviceProxyOrigem);

        }
        static void criaContato(CrmServiceClient serviceProxy)
        {
            for (int i = 0; i < quantidade; i++)
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

                string emailNome = GeradorNome_Sobrenome.geradorSobrenome();
                entidade.Attributes.Add("firstname", emailNome);
                entidade.Attributes.Add("emailaddress1", GeradorEmail.geradorEmail(emailNome));
                entidade.Attributes.Add("lastname", GeradorNome_Sobrenome.geradorSobrenome());
                entidade.Attributes.Add("crb79_cpf", GeradorCPF_CNPJ.geradorCPF()); // cred2_cpf = Apresentação// crb79_cpf = Cobaia
                entidade.Attributes.Add("mobilephone", GeradorTelefone_Topico.geredorTelefone(endereco[5]));

                registro = serviceProxy.Create(entidade);
                Console.Clear();
                Console.WriteLine($"Contato nº: {i + 1}");
            }
        }
        static void criaConta(CrmServiceClient serviceProxy)
        {
            List<listaId> contactId = new List<listaId>(GeradorId.BuscaId(serviceProxy, tabela: "contact", campo: "contactid"));
            Random rnd = new Random();
            for (int i = 0; i < quantidade; i++)
            {
                int index = rnd.Next(0, contactId.Count);

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

                entidade.Attributes.Add("primarycontactid", new EntityReference("contact", contactId[index].Id));
                string emailSobrenome = GeradorNome_Sobrenome.geradorSobrenome();
                entidade.Attributes.Add("emailaddress1", GeradorEmail.geradorEmail(emailSobrenome));
                entidade.Attributes.Add("name", emailSobrenome + " ltda.");
                entidade.Attributes.Add("crb79_cnpj", GeradorCPF_CNPJ.geradorCNPJ()); // cred2_cnpj = Apresentação// crb79_cnpj = Cobaia
                entidade.Attributes.Add("telephone1", GeradorTelefone_Topico.geredorTelefone(endereco[5]));

                registro = serviceProxy.Create(entidade);
                Console.Clear();
                Console.WriteLine($"Conta nº: {i + 1}");
            }
        }
        static void criaClientePotencial(CrmServiceClient serviceProxy)
        {
            for (int i = 0; i < quantidade; i++)
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
                string sobrenomeEmpresa = (GeradorNome_Sobrenome.geradorSobrenome());
                entidade.Attributes.Add("lastname", sobrenomeEmpresa);
                entidade.Attributes.Add("companyname", sobrenomeEmpresa + " ltda.");
                entidade.Attributes.Add("subject", GeradorTelefone_Topico.geredorTopico());
                entidade.Attributes.Add("telephone1", GeradorTelefone_Topico.geredorTelefone(endereco[5]));
                entidade.Attributes.Add("mobilephone", GeradorTelefone_Topico.geredorTelefone(endereco[5]));

                registro = serviceProxy.Create(entidade);
                Console.Clear();
                Console.WriteLine($"Cliente Petencial nº: {i + 1}");
            }
        }
        static void criaOrdem(CrmServiceClient serviceProxy)
        {
            List<listaId> accountId = new List<listaId>(GeradorId.BuscaId(serviceProxy, tabela: "account", campo: "accountid"));
            Random rnd = new Random();
            for (int i = 0; i < quantidade; i++)
            {
                int index = rnd.Next(0, accountId.Count);
                EntityReference accountIdGuid = new EntityReference("account", accountId[index].Id);
                Entity entidade = new Entity("salesorder");
                Guid registro = new Guid();

                entidade.Attributes.Add("name", $"Cliente nº: {i + 1}");
                entidade.Attributes.Add("customerid", accountIdGuid);

                registro = serviceProxy.Create(entidade);

                produtoOrdem(serviceProxy, registro);
                Console.Clear();
                Console.WriteLine($"Ordem nº: {i + 1}");
            }
        }
        static void produtoOrdem(CrmServiceClient serviceProxy, Guid registroOrdem)
        {
            for (int i = 0; i < quantidade; i++)
            {
                var entidade = new Entity("salesorderdetail");
                Guid registro = new Guid();
                Guid prduct = new Guid("4190122b-0477-eb11-a812-000d3a1c6462");
                Guid uomid = new Guid("5f753633-aa6e-eb11-b0b2-000d3a55dda2");

                entidade.Attributes.Add("productid", new EntityReference("product", prduct));
                entidade.Attributes.Add("salesorderid", new EntityReference("salesorder", registroOrdem));
                entidade.Attributes.Add("uomid", new EntityReference("businessunit", uomid));

                registro = serviceProxy.Create(entidade);
            }
        }
        static void criaListaPrecos(CrmServiceClient serviceProxy)
        {
            for (int i = 0; i < quantidade; i++)
            {
                var entidade = new Entity("pricelevel");
                Guid registro = new Guid();

                entidade.Attributes.Add("name", $"Produto {i + 1}");
                entidade.Attributes.Add("begindate", DateTime.Today);
                entidade.Attributes.Add("enddate", DateTime.Today.AddYears(1));

                registro = serviceProxy.Create(entidade);
                Console.Clear();
                Console.WriteLine($"Lista Precos nº: {i + 1}");
            }
        }
    }
}


