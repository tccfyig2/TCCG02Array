using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using RoboCriadorDeItens_2.DAL;
using RoboCriadorDeItens_2.Geradores;
using System;
using System.Diagnostics;

namespace RoboCriadorDeItens_2.MODEL
{
    class RoboDeCriacao : Query
{
        static CrmServiceClient _serviceProxy = Conexao.Cobaia();
        static Random rnd = new Random();
        internal static void Criacao()
        {
            Stopwatch cronometro = new Stopwatch();

            // Especificações
            int tamanhoPacote = 50;
            // if (tamanho do pacote > numeroTotal) {numreoTotal = tamanhoPacote}
            Console.Write("Digite a quantidade de entidades por tabela: ");
            int totalEntidades = int.Parse(Console.ReadLine());
            int loop = (int)Math.Ceiling((float)totalEntidades / tamanhoPacote);

            //Cria Contatos!
            cronometro.Start();
            for (int i = 0; i < loop; i++)
            {
                EntityCollection contact = CriaContact(totalEntidades, tamanhoPacote, i);
                criaNoCrm(_serviceProxy, contact);
                Console.WriteLine($"Pacote nº: {i + 1} criado em contact!");
            }
            cronometro.Stop();
            Console.WriteLine("Tempo de criação: {0:hh\\:mm\\:ss}\n", cronometro.Elapsed);

            // Cria Contas!
            cronometro.Start();
            EntityCollection contatos = RetornaEntidades(_serviceProxy, "contact");
            for (int i = 0; i < loop; i++)
            {
                EntityCollection account = CriaAccount(contatos, tamanhoPacote, i);
                criaNoCrm(_serviceProxy, account);
                Console.WriteLine($"Pacote nº: {i + 1} criado em account!");
            }
            cronometro.Stop();
            Console.WriteLine("Tempo de criação: {0:hh\\:mm\\:ss}\n", cronometro.Elapsed);

            // Cria Clientes Potenciais!
            cronometro.Start();
            for (int i = 0; i < loop; i++)
            {
                EntityCollection lead = CriaLead(totalEntidades, tamanhoPacote, i);
                criaNoCrm(_serviceProxy, lead);
                Console.WriteLine($"Pacote nº: {i + 1} criado em lead!");
            }
            cronometro.Stop();
            Console.WriteLine("Tempo de criação: {0:hh\\:mm\\:ss}\n", cronometro.Elapsed);

            // Cria Ordens!
            cronometro.Start();
            EntityCollection contas = RetornaEntidades(_serviceProxy, "account");
            Console.Write("Digite o valor inicial do código da Ordem: ");
            int semente = int.Parse(Console.ReadLine());
            while (semente <= 15075 || semente > 19999)
            {
                Console.Write("Valor inicial do código da Ordem é inválido!\nDigite o valor inicial do código da Ordem: ");
                semente = int.Parse(Console.ReadLine());
            }
            for (int i = 0; i < loop; i++)
            {
                EntityCollection salesorder = CriaSalesorder(contas, tamanhoPacote, i, semente);
                criaNoCrm(_serviceProxy, salesorder);
                Console.WriteLine($"Pacote nº: {i + 1} criado em salesorder!");
            }
            cronometro.Stop();
            Console.WriteLine("Tempo de criação: {0:hh\\:mm\\:ss}\n", cronometro.Elapsed);

            // Cria Produtos da Ordem!
            cronometro.Start();
            EntityCollection ordens = RetornaEntidades(_serviceProxy, "salesorder");
            for (int i = 0; i < loop; i++)
            {
                EntityCollection salesorderdetail = CriaSalesorderdetail(ordens, tamanhoPacote, i);
                criaNoCrm(_serviceProxy, salesorderdetail);
                Console.WriteLine($"Pacote nº: {i + 1} de salesorderdetail!");
            }
            cronometro.Stop();
            Console.WriteLine("Tempo de criação: {0:hh\\:mm\\:ss}\n", cronometro.Elapsed);
        }
        static EntityCollection CriaContact(int totalEntidades, int tamanhoPacote, int contador)
        {
            contador *= tamanhoPacote;
            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < tamanhoPacote; i++)
            {
                if (contador == totalEntidades) { break; };
                Entity entidade = new Entity("contact");
                entidade.Attributes.Add("crb79_importado", false);
                string[] endereco = (GeradorForm.GeradorEndereco());
                entidade.Attributes.Add("address1_postalcode", endereco[0]);
                entidade.Attributes.Add("address1_line1", endereco[1]);
                entidade.Attributes.Add("address1_line2", endereco[2]);
                entidade.Attributes.Add("address1_line3", endereco[3]);
                entidade.Attributes.Add("address1_city", endereco[4]);
                entidade.Attributes.Add("address1_stateorprovince", endereco[5]);
                entidade.Attributes.Add("address1_country", "Brasil");
                string emailNome = GeradorForm.GeradorSobrenome();
                entidade.Attributes.Add("firstname", emailNome);
                entidade.Attributes.Add("emailaddress1", GeradorForm.GeradorEmail(emailNome));
                entidade.Attributes.Add("lastname", GeradorForm.GeradorSobrenome());
                entidade.Attributes.Add("crb79_cpf", GeradorCPF_CNPJ.GeradorCPF()); // cred2_cpf = Apresentação// crb79_cpf = Cobaia
                entidade.Attributes.Add("mobilephone", GeradorForm.GeredorTelefone(endereco[5]));
                colecaoEntidades.Entities.Add(entidade);
                contador++;
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
                string[] endereco = (GeradorForm.GeradorEndereco());
                entidade.Attributes.Add("address1_postalcode", endereco[0]);
                entidade.Attributes.Add("address1_line1", endereco[1]);
                entidade.Attributes.Add("address1_line2", endereco[2]);
                entidade.Attributes.Add("address1_line3", endereco[3]);
                entidade.Attributes.Add("address1_city", endereco[4]);
                entidade.Attributes.Add("address1_stateorprovince", endereco[5]);
                entidade.Attributes.Add("address1_country", "Brasil");
                entidade.Attributes.Add("primarycontactid", new EntityReference("contact", contact[contador].Id));
                string emailSobrenome = GeradorForm.GeradorSobrenome();
                entidade.Attributes.Add("emailaddress1", GeradorForm.GeradorEmail(emailSobrenome));
                entidade.Attributes.Add("name", $"{emailSobrenome} {numero} ltda.");
                entidade.Attributes.Add("crb79_cnpj", GeradorCPF_CNPJ.GeradorCNPJ()); // cred2_cnpj = Apresentação// crb79_cnpj = Cobaia
                entidade.Attributes.Add("telephone1", GeradorForm.GeredorTelefone(endereco[5]));
                colecaoEntidades.Entities.Add(entidade);
                contador++;
            }
            return colecaoEntidades;
        }
        static EntityCollection CriaLead(int totalEntidades, int tamanhoPacote, int contador)
        {
            contador *= tamanhoPacote;
            EntityCollection colecaoEntidades = new EntityCollection();
            for (int i = 0; i < tamanhoPacote; i++)
            {
                if (contador == totalEntidades) { break; };
                Entity entidade = new Entity("lead");
                entidade.Attributes.Add("crb79_importado", false);
                string[] endereco = (GeradorForm.GeradorEndereco());
                entidade.Attributes.Add("address1_postalcode", endereco[0]);
                entidade.Attributes.Add("address1_line1", endereco[1]);
                entidade.Attributes.Add("address1_line2", endereco[2]);
                entidade.Attributes.Add("address1_line3", endereco[3]);
                entidade.Attributes.Add("address1_city", endereco[4]);
                entidade.Attributes.Add("address1_stateorprovince", endereco[5]);
                entidade.Attributes.Add("address1_country", "Brasil");
                string emailNome = (GeradorForm.GeradorNome());
                entidade.Attributes.Add("emailaddress1", GeradorForm.GeradorEmail(emailNome));
                entidade.Attributes.Add("firstname", emailNome);
                string sobrenomeEmpresa = (GeradorForm.GeradorSobrenome());
                entidade.Attributes.Add("lastname", sobrenomeEmpresa);
                entidade.Attributes.Add("companyname", sobrenomeEmpresa + " ltda.");
                entidade.Attributes.Add("subject", GeradorForm.GeredorTopico());
                entidade.Attributes.Add("telephone1", GeradorForm.GeredorTelefone(endereco[5]));
                entidade.Attributes.Add("mobilephone", GeradorForm.GeredorTelefone(endereco[5]));
                colecaoEntidades.Entities.Add(entidade);
                contador++;
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