using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Tooling.Connector;
using RoboCriadorDeItens_2.Geradores;
using System;
using System.Collections.Generic;

namespace RoboCriadorDeItens_2
{
    class RoboDeCriacao : listaId
    {
        protected static Random rnd = new Random();
        internal static void criacao()
        {
            /*
            * contact
            * account
            * lead
            * Unidade Padrão = Primary unit - Business
            * Lista de Preços = Default
            * Produto = Notebook Lenovo
            * Item da lista de preços = Notebook Lenovo
            * salesorder
            * salesorderdetail
            */
            Conexao conexao = new Conexao();
            CrmServiceClient _serviceProxy = conexao.ObterConexaoCobaia();
            //List<listaId> accountid = new List<listaId>(GeradorId.BuscaId(_serviceProxy, tabela: "account", campo: "accountid"));
            List<listaId> salesorderid = new List<listaId>(GeradorId.BuscaId(_serviceProxy, tabela: "salesorder", campo: "salesorderid"));

            int n = 0;
            //while (n < 25)
            //{
            //    Console.WriteLine(n);

            //    EntityCollection contact = CriaContact();
            //    criaNoCrm(_serviceProxy, contact);
            //    Console.WriteLine(" contact criado!");

            //    n++;
            //}

            //n = 0;
            //while (n < 25)
            //{
            //    Console.WriteLine(n);

            //    EntityCollection account = CriaAccount(accountid);
            //    criaNoCrm(_serviceProxy, account);
            //    Console.WriteLine(" account criado!");

            //    n++;
            //}

            //n = 0;
            //while (n < 25)
            //{
            //    Console.WriteLine(n);

            //    EntityCollection lead = CriaLead();
            //    criaNoCrm(_serviceProxy, lead);
            //    Console.WriteLine(" lead criado!");

            //    n++;
            //}

            //n = 0;
            //while (n < 40)
            //{
            //    Console.WriteLine(n);

            //    EntityCollection salesorder = CriaSalesorder(accountid);
            //    criaNoCrm(_serviceProxy, salesorder);
            //    Console.WriteLine(" salesorder criado!");

            //    n++;
            //}

            n = 0;
            while (n < 40)
            {
                Console.WriteLine(n);

                int contador = n;
                if (contador * 125 >= salesorderid.Count)
                {
                    return;
                }
                EntityCollection salesorderdetail = CriaSalesorderdetail(salesorderid);
                criaNoCrm(_serviceProxy, salesorderdetail);
                Console.WriteLine(" salesorderdetail criado!");

                n++;
            }
        }
        static void criaNoCrm(CrmServiceClient _serviceProxy, EntityCollection input)
        {
            ExecuteMultipleRequest request = new ExecuteMultipleRequest()
            {
                Requests = new OrganizationRequestCollection(),
                Settings = new ExecuteMultipleSettings
                { ContinueOnError = false, ReturnResponses = true }
            };

            foreach (var entity in input.Entities)
            {
                CreateRequest createRequest = new CreateRequest { Target = entity };
                request.Requests.Add(createRequest);
            }

            ExecuteMultipleResponse response = (ExecuteMultipleResponse)_serviceProxy.Execute(request);
            Console.WriteLine("Pacotão criado!");

            int cont = 0;
            foreach (var item in response.Responses)
            {
                if (item.Response != null)
                {
                    cont++;
                }
                else if (item.Fault != null)
                {
                    Console.WriteLine(item.Fault);
                }
            }
            Console.Write(cont);
        }
        static EntityCollection CriaContact()
        {
            EntityCollection colecaoEntidades = new EntityCollection();
            for (int a = 0; a < 200; a++)
            {
                Entity entidade = new Entity("contact");

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

                colecaoEntidades.Entities.Add(entidade);
            }
            return colecaoEntidades;
        }
        static EntityCollection CriaAccount(List<listaId> accountid)
        {
            EntityCollection colecaoEntidades = new EntityCollection();

            for (int b = 0; b < 200; b++)
            {
                int index = rnd.Next(0, accountid.Count);
                Entity entidade = new Entity("account");

                string[] endereco = (GeradorEndereco.geradorEndereco());
                entidade.Attributes.Add("address1_postalcode", endereco[0]);
                entidade.Attributes.Add("address1_line1", endereco[1]);
                entidade.Attributes.Add("address1_line2", endereco[2]);
                entidade.Attributes.Add("address1_line3", endereco[3]);
                entidade.Attributes.Add("address1_city", endereco[4]);
                entidade.Attributes.Add("address1_stateorprovince", endereco[5]);
                entidade.Attributes.Add("address1_country", "Brasil");

                entidade.Attributes.Add("primarycontactid", new EntityReference("contact", accountid[index].Id));
                string emailSobrenome = GeradorNome_Sobrenome.geradorSobrenome();
                entidade.Attributes.Add("emailaddress1", GeradorEmail.geradorEmail(emailSobrenome));
                entidade.Attributes.Add("name", emailSobrenome + " ltda.");
                entidade.Attributes.Add("crb79_cnpj", GeradorCPF_CNPJ.geradorCNPJ()); // cred2_cnpj = Apresentação// crb79_cnpj = Cobaia
                entidade.Attributes.Add("telephone1", GeradorTelefone_Topico.geredorTelefone(endereco[5]));

                colecaoEntidades.Entities.Add(entidade);
            }
            return colecaoEntidades;
        }
        static EntityCollection CriaLead()
        {
            EntityCollection colecaoEntidades = new EntityCollection();

            for (int c = 0; c < 200; c++)
            {
                Entity entidade = new Entity("lead");

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

                colecaoEntidades.Entities.Add(entidade);
            }
            return colecaoEntidades;
        }
        static EntityCollection CriaSalesorder(List<listaId> accountid)
        {
            EntityCollection colecaoEntidades = new EntityCollection();
            Guid pricelevelid = new Guid("68f3a59e-f779-eb11-a812-00224836bdf5");

            for (int d = 0; d < 125; d++)
            {
                int index = rnd.Next(0, accountid.Count);
                Entity entidade = new Entity("salesorder");

                entidade.Attributes.Add("name", $"Ordem NUM-{GeradorTelefone_Topico.criadorCodigo()}");
                entidade.Attributes.Add("customerid", new EntityReference("account", accountid[index].Id));
                entidade.Attributes.Add("pricelevelid", new EntityReference("pricelevel", pricelevelid));

                colecaoEntidades.Entities.Add(entidade);
            }
            return colecaoEntidades;
        }
        static EntityCollection CriaSalesorderdetail(List<listaId> salesorderid, int ultimo = 0)
        {
            int contador = ultimo;

            EntityCollection colecaoEntidades = new EntityCollection();
            Guid productid = new Guid("2bdc8b88-ef79-eb11-a812-00224836bdf5");
            Guid uomid = new Guid("03a4fff9-216e-eb11-b1ab-000d3ac1779c");
            for (int e = 0; e < 125; e++)
            {
                Entity entidade = new Entity("salesorderdetail");

                entidade.Attributes.Add("productid", new EntityReference("product", productid));
                entidade.Attributes.Add("quantity", decimal.Parse(rnd.Next(1, 5).ToString()));
                entidade.Attributes.Add("salesorderid", new EntityReference("salesorder", salesorderid[contador].Id));
                entidade.Attributes.Add("uomid", new EntityReference("businessunit", uomid));

                colecaoEntidades.Entities.Add(entidade);
                contador++;
            }
            return colecaoEntidades;
        }
    }
}