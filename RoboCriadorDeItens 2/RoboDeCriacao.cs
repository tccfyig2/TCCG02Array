using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using RoboCriadorDeItens_2.Geradores;
using System.Diagnostics;
using System;

namespace RoboCriadorDeItens_2
{
    class RoboDeCriacao
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
            Stopwatch cronometro = new Stopwatch();
            // Cria Conexão
            Conexao conexao = new Conexao();
            CrmServiceClient _serviceProxy = conexao.ObterConexaoCobaia();

            int tamanhoPacote = 10;
            // loop de ser um número inteiro! Divisivel por tamanhoPacote!
            int loop = (int)Math.Ceiling((float)1 / tamanhoPacote);
            //Cria Contatos!
            cronometro.Start();
            for (int i = 0; i < loop; i++)
            {
                EntityCollection contact = CriaContact(tamanhoPacote);
                criaNoCrm(_serviceProxy, contact);
                Console.WriteLine($"Pacote nº: {i + 1} criado em contact!");
            }
            cronometro.Stop();
            Console.WriteLine("Tempo decorrido: {0:hh\\:mm\\:ss}", cronometro.Elapsed);

            // Cria Contas!
            cronometro.Start();
            EntityCollection contatos = retornaEntidades(_serviceProxy, "contact");
            for (int i = 0; i < loop; i++)
            {
                EntityCollection account = CriaAccount(contatos, tamanhoPacote, i);
                criaNoCrm(_serviceProxy, account);
                Console.WriteLine($"Pacote nº: {i + 1} criado em account!");
            }
            cronometro.Stop();
            Console.WriteLine("Tempo decorrido: {0:hh\\:mm\\:ss}", cronometro.Elapsed);

            // Cria Clientes Potenciais!
            cronometro.Start();
            for (int i = 0; i < loop; i++)
            {
                EntityCollection lead = CriaLead(tamanhoPacote);
                criaNoCrm(_serviceProxy, lead);
                Console.WriteLine($"Pacote nº: {i + 1} criado em lead!");
            }
            cronometro.Stop();
            Console.WriteLine("Tempo decorrido: {0:hh\\:mm\\:ss}", cronometro.Elapsed);

            // Cria Ordens!
            cronometro.Start();
            EntityCollection contas = retornaEntidades(_serviceProxy, "account");
            for (int i = 0; i < loop; i++)
            {
                Console.WriteLine("Digite o valor inicial do código da Ordem!");
                int semente = int.Parse(Console.ReadLine());
                EntityCollection salesorder = CriaSalesorder(contas, tamanhoPacote, i, semente);
                criaNoCrm(_serviceProxy, salesorder);
                Console.WriteLine($"Pacote nº: {i + 1} criado em salesorder!");
            }
            cronometro.Stop();
            Console.WriteLine("Tempo decorrido: {0:hh\\:mm\\:ss}", cronometro.Elapsed);

            // Cria Produtos da Ordem!
            cronometro.Start();
            EntityCollection ordens = retornaEntidades(_serviceProxy, "salesorder");
            for (int i = 0; i < loop; i++)
            {
                EntityCollection salesorderdetail = CriaSalesorderdetail(ordens, tamanhoPacote, i);
                criaNoCrm(_serviceProxy, salesorderdetail);
                Console.WriteLine($"Pacote nº: {i + 1} de salesorderdetail!");
            }
            cronometro.Stop();
            Console.WriteLine("Tempo decorrido: {0:hh\\:mm\\:ss}", cronometro.Elapsed);
        }
        static EntityCollection retornaEntidades(CrmServiceClient _serviceProxy, string entidade)
        {
            QueryExpression queryExpression = new QueryExpression(entidade);
            queryExpression.Criteria.AddCondition("crb79_importado", ConditionOperator.Equal, false);   // Evita pegar entidaes que ja foram pa o novo ambiente.
            queryExpression.ColumnSet = new ColumnSet(true);

            EntityCollection itens = new EntityCollection();
            queryExpression.PageInfo = new Microsoft.Xrm.Sdk.Query.PagingInfo();
            queryExpression.PageInfo.PageNumber = 1;
            bool moreData = true;
            while (moreData)
            {
                EntityCollection result = _serviceProxy.RetrieveMultiple(queryExpression);
                itens.Entities.AddRange(result.Entities);
                moreData = result.MoreRecords;
                queryExpression.PageInfo.PageNumber++;
                queryExpression.PageInfo.PagingCookie = result.PagingCookie;
            }
            return itens;
        }
        static void criaNoCrm(CrmServiceClient _serviceProxy, EntityCollection colecaoEntidades)
        {
            ExecuteMultipleRequest request = new ExecuteMultipleRequest()
            {
                Requests = new OrganizationRequestCollection(),
                Settings = new ExecuteMultipleSettings
                { ContinueOnError = false, ReturnResponses = true }
            };

            foreach (var entidade in colecaoEntidades.Entities)
            {
                CreateRequest createRequest = new CreateRequest { Target = entidade };
                request.Requests.Add(createRequest);
            }
            ExecuteMultipleResponse response = (ExecuteMultipleResponse)_serviceProxy.Execute(request);
            int cont = 0;
            foreach (var item in response.Responses)
            {
                if (item.Response != null)
                {
                    //Console.WriteLine($"Entidade nº: {cont} criado!");
                }
                else if (item.Fault != null)
                {
                    Console.WriteLine($"ERRO na entidade nº: {cont}!\n{item.Fault}");
                }
                cont++;
            }
            Console.WriteLine($"{cont} entidades criadas!");
        }
        static EntityCollection CriaContact(int tamanhoPacote)
        {
            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < tamanhoPacote; i++)
            {
                Entity entidade = new Entity("contact");
                entidade.Attributes.Add("crb79_importado", false);
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
        static EntityCollection CriaAccount(EntityCollection contact, int tamanhoPacote, int contador)
        {
            contador *= tamanhoPacote;
            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < tamanhoPacote; i++)
            {
                if (contador == contact.Entities.Count) { break; };
                Entity entidade = new Entity("account");
                int numero = rnd.Next(0, 999);
                entidade.Attributes.Add("crb79_importado", false);
                string[] endereco = (GeradorEndereco.geradorEndereco());
                entidade.Attributes.Add("address1_postalcode", endereco[0]);
                entidade.Attributes.Add("address1_line1", endereco[1]);
                entidade.Attributes.Add("address1_line2", endereco[2]);
                entidade.Attributes.Add("address1_line3", endereco[3]);
                entidade.Attributes.Add("address1_city", endereco[4]);
                entidade.Attributes.Add("address1_stateorprovince", endereco[5]);
                entidade.Attributes.Add("address1_country", "Brasil");
                entidade.Attributes.Add("primarycontactid", new EntityReference("contact", contact[contador].Id));
                string emailSobrenome = GeradorNome_Sobrenome.geradorSobrenome();
                entidade.Attributes.Add("emailaddress1", GeradorEmail.geradorEmail(emailSobrenome));
                entidade.Attributes.Add("name", $"{emailSobrenome} {numero} ltda.");
                entidade.Attributes.Add("crb79_cnpj", GeradorCPF_CNPJ.geradorCNPJ()); // cred2_cnpj = Apresentação// crb79_cnpj = Cobaia
                entidade.Attributes.Add("telephone1", GeradorTelefone_Topico.geredorTelefone(endereco[5]));
                colecaoEntidades.Entities.Add(entidade);
                contador++;
            }
            return colecaoEntidades;
        }
        static EntityCollection CriaLead(int tamanhoPacote)
        {
            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < tamanhoPacote; i++)
            {
                Entity entidade = new Entity("lead");
                entidade.Attributes.Add("crb79_importado", false);
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
        static EntityCollection CriaSalesorder(EntityCollection account, int tamanhoPacote, int contador, int semente)
        {
            contador *= tamanhoPacote;
            EntityCollection colecaoEntidades = new EntityCollection();
            Guid pricelevelid = new Guid("68f3a59e-f779-eb11-a812-00224836bdf5");
            for (int i = 0; i < tamanhoPacote; i++)
            {
                if (contador == account.Entities.Count) { break; };
                Entity entidade = new Entity("salesorder");
                entidade.Attributes.Add("crb79_importado", false);
                string codigo = $"cod-{semente + contador}";  // Semente: VALOR INICIAL DO CONTADOR
                entidade.Attributes.Add("name", $"Ordem {codigo}");
                entidade.Attributes.Add("crb79_codigo", codigo);
                entidade.Attributes.Add("customerid", new EntityReference("account", account[contador].Id));
                entidade.Attributes.Add("pricelevelid", new EntityReference("pricelevel", pricelevelid));
                colecaoEntidades.Entities.Add(entidade);
                contador++;
            }
            return colecaoEntidades;
        }
        static EntityCollection CriaSalesorderdetail(EntityCollection salesorder, int tamanhoPacote, int contador)
        {
            contador *= tamanhoPacote;
            EntityCollection colecaoEntidades = new EntityCollection();
            Guid productid = new Guid("2bdc8b88-ef79-eb11-a812-00224836bdf5");
            Guid uomid = new Guid("03a4fff9-216e-eb11-b1ab-000d3ac1779c");
            for (int i = 0; i < tamanhoPacote; i++)
            {
                if (contador == salesorder.Entities.Count) { break; };
                Entity entidade = new Entity("salesorderdetail");
                entidade.Attributes.Add("crb79_importado", false);
                entidade.Attributes.Add("productid", new EntityReference("product", productid));
                entidade.Attributes.Add("quantity", decimal.Parse(rnd.Next(1, 5).ToString()));
                entidade.Attributes.Add("salesorderid", new EntityReference("salesorder", salesorder[contador].Id));
                entidade.Attributes.Add("uomid", new EntityReference("businessunit", uomid));
                colecaoEntidades.Entities.Add(entidade);
                contador++;
            }
            return colecaoEntidades;
        }
    }
}
