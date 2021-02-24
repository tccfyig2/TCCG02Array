using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using RoboCriadorDeItens_2;
using System;
using RoboCriadorDeItens_2.Geradores;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RoboCriadorDeItens_2
{
    class Program
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

            Console.WriteLine("Fim!");
            Console.ReadLine();
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

                entidade.Attributes.Add("emailaddress1", GeradorEmail.geradorEmail());
                entidade.Attributes.Add("firstname", GeradorNome_Sobrenome.geradorNome());
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

                entidade.Attributes.Add("emailaddress1", GeradorEmail.geradorEmail());
                entidade.Attributes.Add("firstname", GeradorNome_Sobrenome.geradorNome());
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
            // Cliente Potencial(customerid) CAMPO PESQUISAVEL
            for (int i = 0; i < 1; i++)
            {
                var entidade = new Entity("salesorder");
                Guid registro = new Guid();
                
                entidade.Attributes.Add("name", $"Cliente nº: {i}");
                //entidade.Attributes.Add("cred2_codigo", GeradorOrdem.geradorCod());
                //entidade.Attributes.Add("ordernumber", GeradorOrdem.geradorOrdemNumber());

                registro = serviceProxy.Create(entidade);
            }
        }

    }
}