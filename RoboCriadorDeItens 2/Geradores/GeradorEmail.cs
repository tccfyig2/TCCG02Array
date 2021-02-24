using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboCriadorDeItens_2.Geradores
{
    class GeradorEmail
    {
        protected static Random rnd = new Random();
        internal static string geradorEmail(string nome = "exemplo")
        {
            
            string[] sufixos = { "org", "yahoo", "uol", "bol", "aol", "gmail", "gov", "hotmail" };
            int index = rnd.Next(sufixos.Length);
            string email = $"{nome.ToLower()}_{rnd.Next(0, 1000)}@{sufixos[index]}.com";
            return email;
        }
    }
}
