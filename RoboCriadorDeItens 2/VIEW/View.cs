using RoboCriadorDeItens_2.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboCriadorDeItens_2.VIEW
{
    class View
    {
        internal static void Interface()
        {
            bool start = true;
            while (start)
            {
                Console.Write("O que deseja fazer?\n" +
                    "\t1- Criar\n" +
                    "\t2- Importar\n" +
                    "\t3- Plugin\n" +
                    "\t4- Sair\n" +
                    "Digite o número da opção: ");
                string resposta = Console.ReadLine().ToLower();
                Console.Clear();
                switch (resposta)
                {
                    case "1":
                        RoboDeCriacao.Criacao();
                        Console.WriteLine("Sucesso!");
                        break;
                    case "2":
                        RoboDeImportacao.Importacao();
                        Console.WriteLine("Sucesso!");
                        break;
                    case "3":
                        bool startPlugin = true;
                        while (startPlugin)
                        {
                            Console.Write("O que deseja Testar?\n" +
                            "\t1 - Plugin CPF Inválido\n" +
                            "\t2 - Plugin CPF Duplicado\n" +
                            "\t3 - Plugin CNPJ Inválido\n" +
                            "\t4 - Plugin CNPJ Duplicado\n" +
                            "\t5 - Plugin código Duplicado\n" +
                            "\t6 - Voltar\n" +
                            "Digite o número da opção: ");
                            resposta = Console.ReadLine().ToLower();
                            Console.Clear();
                            switch (resposta)
                            {
                                case "1":
                                    TestePlugin.PluginCPFInvalido();
                                    break;
                                case "2":
                                    TestePlugin.PluginCPFDuplicado();
                                    break;
                                case "3":
                                    TestePlugin.PluginCNPJInvalido();
                                    break;
                                case "4":
                                    TestePlugin.PluginCNPJDuplicado();
                                    break;
                                case "5":
                                    TestePlugin.PluginCodigoDuplicado();
                                    break;
                                case "6":
                                    startPlugin = false;
                                    break;
                                default:
                                    Console.WriteLine("Alternativa inválida!");
                                    break;
                            }
                        }
                        break;
                    case "4":
                        start = false;
                        break;
                    default:
                        Console.WriteLine("Alternativa inválida!");
                        break;
                }
            }
            Console.WriteLine("FIM!");
        }
    }
}
