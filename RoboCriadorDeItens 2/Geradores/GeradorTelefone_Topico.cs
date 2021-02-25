using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboCriadorDeItens_2.Geradores
{
    class GeradorTelefone_Topico
    {
        protected static Random rnd = new Random();
        internal static string geredorTelefone(string estado = "RS")
        {
            string telefone = null;
            int index;
            string[] ddd;

            switch (estado)
            {
                case "SP":
                    ddd = new string[] { "11", "12", "13", "14", "15", "16", "17", "18", "19" };
                    index = rnd.Next(ddd.Length);
                    telefone = ddd[index];
                    break;

                case "RJ":
                    ddd = new string[] { "21", "22", "24" };
                    index = rnd.Next(ddd.Length);
                    telefone = ddd[index];
                    break;

                case "ES":
                    ddd = new string[] { "27", "28" };
                    index = rnd.Next(ddd.Length);
                    telefone = ddd[index];
                    break;

                case "MG":
                    ddd = new string[] { "31", "32", "33", "34", "35", "37", "38" };
                    index = rnd.Next(ddd.Length);
                    telefone = ddd[index];
                    break;

                case "PR":
                    ddd = new string[] { "41", "42", "43", "44", "45", "46" };
                    index = rnd.Next(ddd.Length);
                    telefone = ddd[index];
                    break;

                case "SC":
                    ddd = new string[] { "47", "48", "49" };
                    index = rnd.Next(ddd.Length);
                    telefone = ddd[index];
                    break;

                case "RS":
                    ddd = new string[] { "51", "53", "54", "55" };
                    index = rnd.Next(ddd.Length);
                    telefone = ddd[index];
                    break;

                case "DF":
                    telefone = "61";
                    break;

                case "GO":
                    ddd = new string[] { "62", "64" };
                    index = rnd.Next(ddd.Length);
                    telefone = ddd[index];
                    break;

                case "TO":
                    telefone = "63";
                    break;

                case "MT":
                    ddd = new string[] { "65", "66" };
                    index = rnd.Next(ddd.Length);
                    telefone = ddd[index];
                    break;

                case "MS":
                    telefone = "67";
                    break;

                case "AC":
                    telefone = "68";
                    break;

                case "RO":
                    telefone = "69";
                    break;

                case "BA":
                    ddd = new string[] { "71", "73", "74", "75", "77" };
                    index = rnd.Next(ddd.Length);
                    telefone = ddd[index];
                    break;

                case "SE":
                    telefone = "79";
                    break;

                case "PE":
                    ddd = new string[] { "81", "87" };
                    index = rnd.Next(ddd.Length);
                    telefone = ddd[index];
                    break;

                case "AL":
                    telefone = "82";
                    break;

                case "PB":
                    telefone = "83";
                    break;

                case "RN":
                    telefone = "84";
                    break;

                case "CE":
                    ddd = new string[] { "85", "88" };
                    index = rnd.Next(ddd.Length);
                    telefone = ddd[index];
                    break;

                case "PI":
                    ddd = new string[] { "86", "89" };
                    index = rnd.Next(ddd.Length);
                    telefone = ddd[index];
                    break;

                case "PA":
                    ddd = new string[] { "91", "93", "94" };
                    index = rnd.Next(ddd.Length);
                    telefone = ddd[index];
                    break;

                case "AM":
                    ddd = new string[] { "92", "97" };
                    index = rnd.Next(ddd.Length);
                    telefone = ddd[index];
                    break;

                case "RR":
                    telefone = "95";
                    break;

                case "AP":
                    telefone = "96";
                    break;

                case "MA":
                    ddd = new string[] { "98", "99" };
                    index = rnd.Next(ddd.Length);
                    telefone = ddd[index];
                    break;
            }

            string[] operadora =
            {
                "67", "71", "72", "95", "96", "97", "98", "99", "68", "73", "74",
                "75", "76", "91", "92", "93", "94", "69", "79", "80", "81",
                "82", "83", "84", "85", "86", "87", "88", "89"
            };
            index = rnd.Next(operadora.Length);
            telefone += operadora[index];
            telefone += rnd.Next(1000000, 9999999);

            return telefone;
        }
        internal static string geredorTopico()
        {
            string[] topico =
            {
                "Acompanhamento com informações relacionadas a nossas promoções",
                "Loja em expansão - enviar novo material",
                "Respondeu com uma carta de interesse",
                "Nova loja aberta este ano - acompanhamento",
                "Interessado em loja apenas online",
                "Bom potencial",
                "Interessado em nossas ofertas mais recentes",
                "Algum interesse em nossos produtos",
                "Gosta de nossos produtos",
                "Nova loja aberta este ano - acompanhamento",
                "Agricultor investidor",
                "Padeiro contratodo",
                "Chef",
                "Dentista",
                "Maquinista",
                "Investidor externo",
                "Empresário com potencial",
                "Gestor de vendas",
                "Caixa",
                "Contato do Taxista",
                "Últimas novidades sobre loja",
                "Fórmula atual em loja",
                "loja: o que é e quais os benefícios para o seu celular",
                "Super Dica de comércio",
                "Quer saber de comércio",
                "Como ganhar comércio",
                "resolver problemas com facilidade",
                "Gestão de clientes",
            };
            int index = rnd.Next(topico.Length);
            return topico[index];
        }
    }
}
