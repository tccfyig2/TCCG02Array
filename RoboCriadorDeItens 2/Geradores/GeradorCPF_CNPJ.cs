using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboCriadorDeItens_2.Geradores
{
    class GeradorCPF_CNPJ
    {
        protected static Random rnd = new Random();
        internal static string geradorCPF()
        {
            int[] multpDigito1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multpDigito2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] cpf = new int[11];
            for (int i = 0; i < 9; i++)
            {
                cpf[i] = rnd.Next(0, 10);
            }

            // Calculo primeiro dígito
            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += multpDigito1[i] * cpf[i];
            }

            cpf[9] = Resto(soma);

            // Calculo segundo dígito
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += multpDigito2[i] * cpf[i];
            }

            cpf[10] = Resto(soma);

            return string.Join("", cpf);
        }
        internal static string geradorCNPJ()
        {
            int[] multpDigito1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multpDigito2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] cnpj = new int[14];
            for (int i = 0; i < 12; i++)
            {
                cnpj[i] = rnd.Next(0, 10);
            }

            // Calculo primeiro dígito
            int soma = 0;
            for (int i = 0; i < 12; i++)
            {
                soma += multpDigito1[i] * cnpj[i];
            }

            cnpj[12] = Resto(soma);

            // Calculo segundo dígito
            soma = 0;
            for (int i = 0; i < 13; i++)
            {
                soma += multpDigito2[i] * cnpj[i];
            }

            cnpj[13] = Resto(soma);

            return string.Join("", cnpj);
        }
        public static int Resto(int soma)
        {
            int resto = soma % 11;
            if (resto < 2)
            {
                return (resto = 0);
            }
            else
            {
                return (resto = 11 - resto);
            }
        }
    }
}
