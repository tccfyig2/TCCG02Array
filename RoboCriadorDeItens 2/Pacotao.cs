using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using RoboCriadorDeItens_2.Geradores;
using System;

namespace RoboCriadorDeItens_2
{
    class Pacotao
    {
        internal static void pacote()
        {
            try
            {
                for (int i = 0; i < 20; i++)
                {
                    var entidade = new Entity("contact");

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

                    var createRequest = new CreateRequest()
                    {
                        Target = entidade
                    };
                    request.Requests.Add(createRequest);
                }

                Console.WriteLine("Before execute");
                var response = (ExecuteMultipleResponse)service.Execute(request);
                Console.WriteLine("After execute");

                foreach (var r in response.Responses)
                {
                    if (r.Response != null)
                        Console.WriteLine("Success");
                    else if (r.Fault != null)
                        Console.WriteLine(r.Fault);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
