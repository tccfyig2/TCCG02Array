using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboCriadorDeItens_2.Geradores
{
    class GeradorNome_Sobrenome
    {
        protected static Random rnd = new Random();
        internal static string geradorNome()
        {
            string[] Homem = //100
            {
                "Rafael", "Bruno", "André", "Luiz", "Tiago", "Felipe", "Guilherme", "Daniel", "Lucas", "Rodrigo",
                "Fernando", "João", "Pedro", "Fábio", "Marcelo", "Victor", "Eduardo", "Gustavo", "Gabriel", "Paulo",
                "Ricardo", "José", "Carlos", "Leonardo", "Alexandre", "Leandro", "Vinícius", "Renato", "Marcos", "Caio",
                "Mateus", "Diego", "Henrique", "Danilo", "Arthur", "Renan", "Maurício", "Antônio", "Márcio", "Flávio",
                "William", "Marco", "Anderson", "Roberto", "Julio", "Francisco", "Diogo", "Douglas", "Sérgio", "César",
                "Murilo", "Igor", "Adriano", "Alan", "Eric", "Alex", "Filipe", "Ivan", "Mário", "Hugo",
                "Marcel", "Rodolfo", "Rogério", "Marcus", "Luciano", "Jorge", "Edson", "Fabrício", "Claudio", "Denis",
                "Augusto", "David", "Frederico", "Samuel", "Yuri", "Otávio", "Juliano", "Davi", "Wagner", "Michel",
                "Jefferson", "Celso", "Ronaldo", "Robson", "Mauro", "Fabiano", "Alberto", "Heitor", "Rubens", "Thomas",
                "Alessandro", "Emerson", "Cristiano", "Everton", "Jonas", "Breno", "Cássio", "Wellington", "Thales", "Evandro"
            };
            string[] Mulher = //203
            {
                "Adriana", "Alessandra", "Alice", "Aline", "Alinne", "Amanda", "Ana", "Anna", "Andrea", "Andreia", "Andressa",
                "Ariane", "Beariz", "Bianca", "Bruna", "Bárbara", "Camila", "Carla", "Carolina", "Caroline", "Cecília",
                "Cláudia", "Cristiane", "Cristina", "Cíntia", "Daniela", "Danielle", "Denise", "Débora", "Elaine", "Eliane",
                "Elisa", "Emanuele", "Emanuelle", "Fabiana", "Fernada", "Flávia", "Gabriela", "Giovanna", "Gisele", "Helena",
                "Heloisa", "Isabel", "Isabela", "Isadora", "Janaina", "Jaqueline", "Joyce", "Julia", "Juliana", "Jéssica",
                "Karen", "Karina", "Kelly", "Kátia", "Lara", "Larissa", "Laura", "Laís", "Letícia", "Lgia",
                "Lilian", "Luana", "Luciana", "Luiza", "Lívia", "Marcela", "Maria", "Mariana", "Mariane", "Marina",
                "Marília", "Mayara", "Mayra", "Michelle", "Milena", "Márcia", "Mônica", "Natália", "Nádia", "Pamela",
                "Patrícia", "Paula", "Priscila", "Rafaela", "Raquel", "Renata", "Roberta", "Sabrina", "Sandra", "Sarah",
                "Silvia", "Simone", "Stephanie", "Talita", "Tatiana", "Tatiane", "Thaís", "Vanessa", "Verônica", "Vitória",
                "Vivian", "Viviane"
            };
            int sexo = rnd.Next(0, 2);
            if (sexo == 0)
            {
                int IndexArray = rnd.Next(Homem.Length);
                return Homem[IndexArray];
            }
            else
            {
                int IndexArray = rnd.Next(Mulher.Length);
                return Mulher[IndexArray];
            }

        }
        internal static string geradorSobrenome()
        {

            string[] Sobrenome = //65
            {
                "Aguiar", "Aires", "Alencar", "Almeida", "Alves", "Amorim", "Antunes", "Araújo", "Azevedo", "Barbosa", "Barreto",
                "Barros", "Borges", "Brito", "Cabral", "Campelo", "Cardoso", "Carvalho", "Castro", "Cavalcante", "Correia",
                "Costela", "Cunha", "Dias", "Dutra", "Escobar", "Farias", "Faustino", "Fernandes", "Ferreira", "Flores",
                "Garcia", "Gomes", "Guimarães", "Gusmão", "Leite", "Leitão", "Lima", "Lopes", "Macedo", "Machado",
                "Magalhães", "Martins", "Melo", "Mendonça", "Moreira", "Nascimento", "Nogueira", "Nunes", "Oliveira", "Paes",
                "Paiva", "Pereira", "Pinto", "Queiroz", "Ramos", "Ribeiro", "Rocha", "Rodrigues", "Santos", "Saraiva",
                "Silva", "Soares", "Souza", "Xavier"
            };
            int IndexArray = rnd.Next(Sobrenome.Length);
            return Sobrenome[IndexArray];
        }
    }
}
