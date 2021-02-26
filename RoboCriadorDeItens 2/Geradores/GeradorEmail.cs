using System;

namespace RoboCriadorDeItens_2.Geradores
{
    class GeradorEmail
    {
        protected static Random rnd = new Random();
        internal static string geradorEmail(string nome = "exemplo")
        {
            
            string[] sufixos = { "live", "yahoo", "uol", "bol", "aol", "gmail", "ymail", "hotmail", "ig" };
            int index = rnd.Next(sufixos.Length);
            string email = $"{nome.ToLower()}_{rnd.Next(0, 1000)}@{sufixos[index]}.com";
            return email;
        }
    }
}
