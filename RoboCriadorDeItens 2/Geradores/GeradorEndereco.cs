﻿using System;
using DotCEP;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboCriadorDeItens_2.Geradores
{
    class GeradorEndereco
    {
        protected static Random rnd = new Random();
        internal static string[] geradorEndereco()
        {
            string[] arrayCEP = // 200
            {
                "57040575", "68903869", "54160280", "29302039", "54410376", "58052130", "89256520", "64218370", "69305095", "77403110",
                "40243520", "58057025", "78553252", "69084225", "29169666", "69060190", "67146406", "68908199", "74953370",
                "57020240", "69914726", "76828018", "75094380", "40220650", "95098370", "69903815", "57081005", "91540424", "65060624",
                "54518445", "53220101", "79003250", "94185230", "81530290", "76801198", "65054330", "72503320", "59012185", "60874355",
                "89280244", "83210043", "68741560", "38055150", "38041199", "65067886", "66813500", "35164311", "92030796", "38412622",
                "69316192", "72250202", "24420560", "56302180", "97578040", "79310136", "49043200", "88812759", "77809410", "79002190",
                "29901288", "69911875", "24465260", "77824010", "79041190", "68906903", "69309240", "54160351", "74393690", "60526470",
                "79105460", "79023111", "29700554", "65058729", "57015227", "40301516", "44050482", "58407272", "29190484", "29143510",
                "79830903", "83606495", "69909600", "40810450", "55038600", "69316049", "65604875", "68907540", "57063080", "29707140",
                "57307360", "29900073", "72430400", "55644826", "77828080", "59146845", "17523100", "69089230", "79042871", "76824167",
                "76913029", "79116133", "69908190", "86078330", "72833646", "58045509", "79841460", "29106120", "69072126", "69307420",
                "72428725", "29306327", "14784323", "77425170", "69151462", "33855660", "72860403", "79003172", "56320280", "64052904",
                "69317300", "69065055", "69911490", "68906620", "68903335", "93222200", "50781590", "76824197", "91900230", "58033530",
                "57084021", "57305220", "72323502", "79006761", "57075540", "86802766", "64011165", "60743180", "61600530", "72545235",
                "69910380", "64058080", "58401288", "79092122", "69152076", "69151716", "78080518", "52020020", "38406148", "77440750",
                "24732230", "81330050", "60310480", "68903129", "54300082", "79037805", "78720089", "64045780", "78098664", "02126050",
                "72548200", "58410050", "23060180", "69078610", "64007828", "57080540", "83707741", "49052040", "33015320", "57086144",
                "69901450", "59073283", "78149482", "69309593", "74964470", "93220490", "74685590", "86047720", "68901420", "58059426",
                "35164241", "77825786", "49080641", "79015410", "72220049", "77015516", "68908300", "69316051", "33125160", "58082008",
                "69086180", "41253070", "77445070", "69088200", "53550510", "57017195", "37060700", "69314398", "78117352", "84050110"
            };
            int index = rnd.Next(arrayCEP.Length);
            string cep = arrayCEP[index];

            try
            {
                Endereco enderecoBase = new Endereco();
                enderecoBase = Consultas.ObterEnderecoCompleto(cep);

                string[] endereco = new string[6];
                endereco[0] = enderecoBase.cep.Replace("-", "");
                endereco[1] = enderecoBase.logradouro;
                endereco[2] = rnd.Next(1, 2000).ToString();
                endereco[3] = enderecoBase.bairro;
                endereco[4] = enderecoBase.localidade;
                endereco[5] = enderecoBase.uf;
                return endereco;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
