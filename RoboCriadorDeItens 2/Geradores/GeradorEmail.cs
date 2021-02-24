using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboCriadorDeItens_2.Geradores
{
    class GeradorEmail : GeradorNome_Sobrenome
    {
        internal static string geradorEmail()
        {
            Random rnd = new Random();
            string sobrenome = geradorSobrenome().ToLower();
            string nome = geradorNome().ToLower();
            string[] sufixos = { "org", "yahoo", "uol", "bol", "aol", "gmail", "gov", "hotmail" };
            int index = rnd.Next(sufixos.Length);
            string email = $"{sobrenome}.{nome}@{sufixos[index]}.com";
            return email;
        }
    }
}
