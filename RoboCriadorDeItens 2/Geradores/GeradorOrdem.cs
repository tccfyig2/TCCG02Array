using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboCriadorDeItens_2.Geradores
{
    class GeradorOrdem
    {
        public static Random rnd = new Random();
        internal static string geradorCod()
        {
            return "cod-" + rnd.Next(10000, 99999).ToString();
        }
        internal static string geradorOrdemNumber()
        {
            string[] ordernumber = new string[12];
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            for (int i = 0; i < ordernumber.Length; i++)
            {
                if (i >= 0 && i <= 5) { ordernumber[i] = rnd.Next(0, 10).ToString(); }
                if (i == 6) { ordernumber[i] = "-"; }
                if (i >= 7 && i <= 12) { ordernumber[i] = chars[rnd.Next(chars.Length)].ToString(); }
            }
            return "ORD-" + string.Join("", ordernumber);
        }
    }
}
