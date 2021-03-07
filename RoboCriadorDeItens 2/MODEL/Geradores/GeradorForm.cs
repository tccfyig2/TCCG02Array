using DotCEP;
using System;

namespace RoboCriadorDeItens_2.Geradores
{
    class GeradorForm
    {
        static Random rnd = new Random();
        internal static string GeradorEmail(string nome = "exemplo")
        {
            string[] sufixos = { "live", "yahoo", "uol", "bol", "aol", "gmail", "ymail", "hotmail", "ig" };
            int index = rnd.Next(sufixos.Length);
            string email = $"{nome.ToLower().Trim()}_{rnd.Next(0, 1000)}@{sufixos[index]}.com";
            return email;
        }
        internal static string[] GeradorEndereco()
        {
            string[] arrayCEP = // 500
            {
                "57040575", "68903869", "54160280", "76960083", "54410376", "58052130", "89256520", "64218370", "69305095", "77403110",
                "40243520", "58057025", "78553252", "69084225", "29169666", "69060190", "67146406", "68908199", "74953370", "77818820",
                "57020240", "57070720", "76828018", "75094380", "40220650", "95098370", "58802100", "57081005", "91540424", "65060624",
                "54518445", "53220101", "79003250", "94185230", "81530290", "76801198", "65054330", "72503320", "59012185", "60874355",
                "89280244", "83210043", "68741560", "38055150", "38041199", "65067886", "66813500", "35164311", "92030796", "38412622",
                "69316192", "72250202", "24420560", "56302180", "97578040", "79310136", "49043200", "88812759", "77809410", "79002190",
                "29901288", "55640166", "24465260", "77824010", "79041190", "68906903", "69309240", "54160351", "74393690", "60526470",
                "79105460", "79023111", "29700554", "65058729", "57015227", "40301516", "44050482", "58407272", "29190484", "29143510",
                "79830903", "83606495", "56906102", "69900058", "64601421", "69316049", "65604875", "68907540", "57063080", "29707140",
                "57307360", "29900073", "72430400", "55644826", "77828080", "59146845", "17523100", "69089230", "79042871", "76824167",
                "76913029", "79116133", "77813290", "86078330", "72833646", "58045509", "79841460", "29106120", "69072126", "69307420",
                "72428725", "29306327", "14784323", "77425170", "69151462", "33855660", "72860403", "79003172", "56320280", "64052904",
                "69317300", "69065055", "69911490", "68906620", "68903335", "93222200", "50781590", "76824197", "91900230", "36200322",
                "57084021", "59040136", "72323502", "79006761", "64002095", "86802766", "64011165", "60743180", "61600530", "72545235",
                "74140040", "64058080", "58401288", "79092122", "69152076", "69151716", "78080518", "60766130", "38406148", "77440750",
                "24732230", "81330050", "60310480", "68903129", "54300082", "79037805", "78720089", "64045780", "78098664", "02126050",
                "72548200", "58410050", "23060180", "69078610", "64007828", "57080540", "83707741", "49052040", "33015320", "79837030",
                "36030720", "59073283", "78149482", "69309593", "74964470", "93220490", "74685590", "86047720", "68901420", "58059426",
                "35164241", "77825786", "49080641", "79015410", "72220049", "77015516", "68908300", "69316051", "33125160", "58082008",
                "69086180", "41253070", "77445070", "69088200", "53550510", "57017195", "37060700", "69314398", "78117352", "84050110",
                "26112480", "64012794", "63050138", "60360180", "69084044", "29101641", "86050494", "15808446", "77016484", "59143260",
                "69903242", "49044286", "84178630", "89219614", "69306280", "69315601", "72536540", "68926090", "76960236", "89066001",
                "57082107", "58057160", "74310150", "65058436", "76804498", "76804498", "38400064", "83215721", "65037320", "88511540",
                "61645440", "65085191", "60540507", "77001814", "88056575", "72329569", "69401362", "69901597", "79084090", "79116473",
                "79604250", "79906816", "74884704", "99060464", "49008253", "68909495", "41600770", "49015020", "12912450", "77440080",
                "60533020", "58075520", "69306371", "57050120", "65056120", "80630230", "45994698", "68925073", "70744070", "71694040",
                "76876684", "36204160", "13212118", "69307274", "68927444", "63050971", "65630532", "66110132", "69088301", "26160470",
                "77059020", "73021010", "25040070", "54315065", "77814170", "69400246", "12061063", "64052305", "58062062", "63902020",
                "76873762", "49097140", "69307685", "38041354", "50750290", "74425560", "66643260", "67145745", "41610250", "72233001",
                "57039203", "66842160", "85601477", "66640638", "59042462", "65032542", "85870696", "33031250", "50970020", "49080661",
                "69021200", "71996080", "65080810", "44091558", "78710060", "64200538", "88102440", "69918848", "29053333", "60190160",
                "59060300", "49037570", "23918520", "77060382", "65905382", "69048610", "57025776", "55294742", "81930650", "69402262",
                "65057871", "78556788", "59150095", "34535020", "20932180", "79009250", "77060308", "89213380", "69915455", "35660183",
                "85862348", "71536322", "68909010", "68551178", "49035430", "87083374", "38180134", "79904192", "50751000", "29702141",
                "77017303", "65062500", "89255270", "29017050", "78142442", "51240070", "77015562", "25525390", "71090655", "81170170",
                "72238136", "69044600", "69909784", "54517230", "65067361", "77001133", "90230001", "68035325", "24426455", "13256723",
                "57302695", "69027170", "29160132", "64055373", "76906627", "54280220", "29163348", "72851770", "58080240", "49008441",
                "79654970", "53230130", "77808340", "65067866", "69906486", "61610015", "73088340", "77433410", "69088270", "77420866",
                "32606214", "25730202", "58807645", "27313080", "21330370", "29023567", "59152270", "89010130", "78558269", "76824316",
                "64035445", "69015173", "64200905", "79091734", "49065063", "39406142", "64059500", "40725783", "40430220", "27516310",
                "65908836", "58058815", "61922060", "76901276", "59618660", "76876634", "72880608", "60743835", "72860767", "57075039",
                "77024480", "34525560", "78559459", "78720463", "59108140", "69055752", "71880043", "64053030", "59945970", "64075050",
                "64057348", "49042290", "79816220", "93125210", "59108235", "69317336", "59122028", "65605575", "25710390", "77815090",
                "65045630", "69314128", "78731609", "68927435", "59604350", "69317810", "52130100", "67015303", "76808882", "38442088",
                "65061511", "93040190", "76965892", "49085280", "79612473", "13178215", "74473822", "75712380", "77829086", "37903198",
                "49075510", "86058268", "45611516", "39803091", "77060102", "49044199", "60711055", "60711055", "59010362", "88030902",
                "23020160", "59115686", "24472200", "68909316", "39401066", "94935670", "41301700", "60430430", "77823658", "78068790",
                "57086062", "69920076", "72907106", "29900190", "69915658", "76829504", "79601030", "68906172", "53260060", "85027100",
                "58805350", "64008075", "49063217", "80520150", "69028001", "69921398", "65068292", "60544280", "77064704", "63105190",
                "69006451", "64065130", "96840520", "89212520", "45038254", "49055000", "96410070", "60450042", "72863215", "21721050"
            };
            int index = rnd.Next(0, arrayCEP.Length);
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
        internal static string GeradorNome()
        {
            string[] homem = //1000
            {
                "Matheus", "Davi", "Heitor", "Henrique", "Bruno", "Samuel", "Felipe", "Lorenzo", "Benjamim", "Vinícius",
                "Eduardo", "Noah", "Antônio", "Leonardo", "Diego", "Nicolas", "Daniel", "Thiago", "José", "Gael",
                "Alexandre", "André", "Vitor", "Mateus", "Carlos", "Caleb", "Paulo", "Theo", "Caio", "Joaquim",
                "Marcos", "Bryan", "Pietro", "Tiago", "Victor", "Francisco", "Yuri", "Fernando", "Ricardo", "Marcelo",
                "Lúcifer", "Leandro", "João Pedro", "Luiz", "Murilo", "Igor", "Emanuel", "Diogo", "Pedro Henrique", "Anderson",
                "Augusto", "Luan", "Levi", "Fábio", "Jonathan", "Renan", "Breno", "William", "Wesley", "Jesus",
                "David", "Isaac", "João Vítor", "Douglas", "Danilo", "Israel", "Filipe", "Ravi", "Vicente", "Erick",
                "Cauã", "Silva", "Isaque", "Luís", "Artur", "Kauã", "Kauan", "Ryan", "Jefferson", "Benjamin",
                "Adriano", "Maicon", "Otávio", "Saulo", "Ícaro", "Hugo", "Ruan", "Ariel", "Asafe", "Edson",
                "Lucca", "Alan", "Ítalo", "Kevin", "Moisés", "Anthony", "Yan", "Wellington", "Oliveira", "Alex",
                "Renato", "Márcio", "Thomas", "Henry", "Kaique", "Jorge", "Robson", "Téo", "Luciano", "Júnior",
                "Iago", "Sérgio", "Ian", "César", "Giovanni", "Roberto", "Jean", "João Miguel", "Richard", "Bento",
                "Nathan", "Tomás", "Jonas", "Alison", "Júlio", "Emerson", "Kauê", "João Victor", "Enzo Gabriel", "Willian",
                "Everton", "Pablo", "Elias", "João Lucas", "Cláudio", "Fabrício", "Josué", "Maurício", "Santos", "Raul",
                "Tales", "Enrico", "Rogério", "Ramon", "Carlos Eduardo", "Cristiano", "Yago", "Adrian", "Flávio", "Michael",
                "Ronaldo", "Micael", "Mario", "Ivan", "Martim", "Frederico", "Ezequiel", "Natanael", "Patrick", "Oliver",
                "Pereira", "Souza", "Alessandro", "Afonso", "Estevão", "Wagner", "Apolo", "Luca", "Harry", "Juan",
                "Álvaro", "Brayan", "Efraim", "Dante", "Ismael", "Santiago", "Rodrigues", "Marlon", "Jônatas", "Ferreira",
                "Adriel", "Abner", "Davi Lucas", "Júlio César", "Raphael", "Antony", "Eric", "Christian", "Kaio", "Castiel",
                "Victor Hugo", "Wanderson", "João Paulo", "Natan", "Manoel", "Jackson", "Aquiles", "João Gabriel", "Sebastião", "Wendel",
                "Allan", "Wilson", "Gomes", "Otto", "John", "Nelson", "Joshua", "Luiz Henrique", "Rui", "Kaleb",
                "Dominic", "Liam", "Manuel", "Brian", "Luiz Felipe", "Adão", "Simão", "Kelvin", "Raimundo", "Valentim",
                "Caíque", "Michel", "Wallace", "Akira", "Juliano", "Denis", "Taylor", "Mauro", "Fabiano", "Ethan",
                "Lourenço", "Gilberto", "Marco", "Thor", "Klaus", "Reginaldo", "Rian", "Derick", "Alef", "Pedro Lucas",
                "Alves", "Martins", "Oséias", "Joel", "Vieira", "Jacó", "Aruna", "Matias", "Zayn", "Lázaro",
                "Alberto", "Abraão", "Marco Antônio", "João Guilherme", "Geovane", "Marcos Vinícius", "Matteo", "Donald", "Washington", "Geraldo",
                "Derek", "Jhonatan", "Evandro", "Eliseu", "Rodolfo", "Christopher", "James", "Cauê", "Sandro", "Ângelo",
                "Isaías", "Gonçalo", "Cleiton", "Inácio", "Almeida", "Ribeiro", "Clayton", "Nuno", "Andrew", "Hélio",
                "Teodoro", "Rubens", "Louis", "Lima", "Luiz Fernando", "Carvalho", "Pierre", "Eros", "Zion", "Max",
                "Marques", "Cícero", "Leo", "Salomão", "Paulo Henrique", "Luigi", "Robert", "Mathias", "Uriel", "Martina",
                "Charles", "Jair", "Luiz Miguel", "Erik", "Rômulo", "Duarte", "Kennedy", "Higor", "Galileu", "Damon",
                "Manassés", "Dimitri", "Túlio", "Silas", "Lúcio", "Nascimento", "Celso", "Eliel", "Reinaldo", "Jeremias",
                "Abrão", "Edilson", "Isac", "Edgar", "Eli", "Hector", "José Carlos", "Miquéias", "Nicholas", "Caetano",
                "Éder", "Carlos Henrique", "Lucas Gabriel", "Luís Gustavo", "Sidney", "Gilmar", "Edmílson", "Lopes", "Cássio", "Dylan",
                "Vanderlei", "Gonçalves", "Theodoro", "Xavier", "Abel", "Zeus", "Santana", "Kalebe", "Osvaldo", "Luís Felipe",
                "Soares", "Ruben", "Tito", "Leon", "Saymon", "Jailson", "Valdir", "Walter", "Vagner", "Adílson",
                "Jairo", "Andrade", "Jeferson", "Ivo", "Áquila", "Orlando", "Denílson", "Sebastian", "Thales", "Cléber",
                "Roger", "Zaqueu", "Borges", "Deus", "Calebe", "Jack", "Nilson", "Ademir", "Niall", "Ulísses",
                "Lincoln", "Romeu", "Aaron", "Arthur Gabriel", "Neymar", "Kaleo", "Edvaldo", "Jacob", "Jeová", "Alfredo",
                "Benedito", "Gérson", "Cristian", "Queiroz", "Magno", "Gustavo Henrique", "Milton", "Whindersson", "Ben-Hur", "Conrado",
                "Messias", "Maxwel", "Adam", "Jaime", "Tobias", "Félix", "George", "Peter", "Átila", "Mendes",
                "Raoni", "Moacir", "Logan", "Luiz Carlos", "Joseph", "Domingos", "Cassiano", "Moura", "Marcus", "Cauan",
                "Humberto", "Gregório", "Matheus Henrique", "Sávio", "Jessé", "Araújo", "Gílson", "Fernandes", "Saul", "Jó",
                "Misael", "Hudson", "Thierry", "Esdras", "Enoque", "Costa", "Severino", "Mikhael", "Sílvio", "Dario",
                "Maciel", "Neemias", "Nilton", "Christofer", "Pinto", "Eliézer", "Osmar", "Riquelme", "Vítor Hugo", "Olavo",
                "Raí", "Deivid", "Mohammed", "Noé", "Alexander", "Armando", "Edward", "Válter", "Cunha", "Josemar",
                "Oscar", "Kléber", "Alejandro", "Petrus", "Valmir", "Peterson", "Salvador", "Heron", "Cainã", "Ygor",
                "Vinícios", "Antônio Carlos", "Bartolomeu", "Serafim", "Caim", "Dionísio", "Valdeci", "Brito", "Franklin", "Batista",
                "Murillo", "Obede", "Joabe", "Tarcísio", "Athos", "Valentin", "Élton", "Neto", "Johnny", "Nick",
                "Henri", "Samir", "Claudinei", "Tyler", "Vicenzo", "Vladimir", "Hélder", "Salatiel", "Albuquerque", "Luís Fernando",
                "Ronald", "Yanni", "Josias", "Sansão", "João Batista", "Amon", "Sam", "Ben", "Nabucodonosor", "Natã",
                "Tadeu", "Hércules", "Vasco", "Almir", "João Henrique", "Pedro Miguel", "Odair", "Luiz Otávio", "Romário", "Bob",
                "Arnaldo", "Célio", "Charlie", "Lola", "MacGyver", "Rhuan", "Justin", "Lorran", "Quemuel", "Querubim",
                "Ivanildo", "Gleison", "Phelipe", "Eliabe", "Nicollas", "Hades", "Leonel", "Simon", "Darlan", "Aílton",
                "Marcus Vinicius", "Ayala", "João Marcos", "Lohan", "Baltazar", "Anael", "Edivaldo", "Hamilton", "Tomé", "Viana",
                "Atos", "Naamã", "Amorim", "Ernesto", "Moreira", "Ubirajara", "Sídnei", "Judá", "Agnaldo", "Elói",
                "Boaz", "Ramos", "Dias", "Nicolau", "Arthur Henrique", "Elvis", "Dinis", "João Carlos", "Baruc", "Gabriel Henrique",
                "René", "Camilo", "Tom", "Ronny", "Clóvis", "Morais", "Edmar", "Tony", "Youssef", "Fagner",
                "Gênesis", "Nivaldo", "Carlos Alberto", "Kalil", "Pires", "Lauro", "André Luís", "Mizael", "Germano", "Haniel",
                "Irineu", "José Pedro", "Luiz Guilherme", "Damião", "Laércio", "Zacarias", "Rudá", "Cezar", "Melo", "Rocha",
                "Ednaldo", "Paulo César", "Arão", "Luiz Gustavo", "Aurélio", "Demétrio", "Ávila", "Adônis", "Jordão", "Moraes",
                "Yudi", "Melquisedeque", "Cardoso", "Timóteo", "Sete", "Valdemar", "Fonseca", "Freitas", "Frank", "Lourenzo",
                "Bóris", "Dimas", "Carlos Daniel", "Wanderley", "Henrico", "Wilian", "Itamar", "Olímpio", "Ken", "Juarez",
                "Simeão", "Sadraque", "Kalel", "Ari", "Brendon", "Ciro", "Goku", "Eugênio", "Stefan", "Marco Aurélio",
                "Ezequias", "Nunes", "Anselmo", "Jerônimo", "Darci", "Marcos Paulo", "Donizete", "Azevedo", "Adalberto", "Enos",
                "Valdinei", "Brendo", "Luiz Eduardo", "Scott", "Ubiratan", "Judas", "Albert", "Gaspar", "Dídimo", "Vitório",
                "Davidson", "Manu", "Hermes", "Eudes", "Altair", "Jadson", "Spike", "Omar", "Alencar", "José Antônio",
                "Maximiliano", "Guto", "Valdemir", "Diógenes", "Epaminondas", "Luís Otávio", "Antoni", "Javé", "Bruno Henrique", "Djalma",
                "Naim", "Jacinto", "Allyson", "Ramsés", "Pedro Augusto", "Oziel", "Jean Carlos", "Guilherme Henrique", "Patrício", "Ragnar",
                "Nataniel", "Andrey", "Aírton", "Roque", "Estevan", "Weverton", "Enrique", "Bruce", "João Felipe", "Pacheco",
                "Welington", "Aparecido", "Plínio", "Guido", "Jamal", "Hiroshi", "Júnio", "Emílio", "Malaquias", "Teddy",
                "Luke", "Arlindo", "Penha", "Rick", "Régis", "Naruto", "Hunter", "Gilvan", "Josafá", "Axel",
                "Harley", "Jurandir", "Luiz Antônio", "Esaú", "Everaldo", "Haroldo", "Edmundo", "Assis", "Kai", "Miguel Henrique",
                "Elijah", "Yure", "Ronan", "Hazel", "Airon", "Genivaldo", "Ibrahim", "Otoniel", "Narciso", "Aldo",
                "Joelson", "Marcelino", "Haley", "Paiva", "Máximus", "José Maria", "Paco", "José Henrique", "Elder", "Etienne",
                "Kayke", "Brandão", "Venâncio", "Nikolas", "Hideki", "Aires", "Chico", "Mike", "Ademar", "Sales",
                "Teixeira", "Yves", "Noel", "José Augusto", "Agostinho", "Genilson", "Atlas", "Eliaquim", "Nilo", "Érico",
                "Barnabé", "Ananias", "Jason", "Filippo", "Magalhães", "Júpiter", "Jorge Luiz", "Takashi", "César Augusto", "Acácio",
                "Mota", "Wilton", "Eike", "Adolfo", "Talles", "Kaneki", "Cleyton", "Azael", "Luís Carlos", "Gomer",
                "Elcana", "Ali", "Éderson", "José Roberto", "Gohan", "Jardel", "Gil", "Cosme", "Vincenzo", "Toby",
                "Brandon", "Herbert", "Bill", "Luís Eduardo", "Machado", "Welton", "Ayrton", "Alcídes", "Killian", "Benoni",
                "Diniz", "Antunes", "Heleno", "Américo", "Silvano", "Nabal", "Ranieri", "Asher", "Laerte", "Luís Henrique",
                "Franco", "Nico", "Ezra", "Lino", "Dalton", "Amadeu", "Lourival", "Pedro Gabriel", "Ageu", "Horácio",
                "Everson", "Marley", "Adiel", "Jimmy", "Roney", "Juca", "Zaki", "Mariano", "Hian", "Gedeão",
                "Zack", "Marvin", "Nero", "Harrison", "Morfeu", "Leal", "Manuele", "Paulo Sérgio", "Ênio", "Sasuke",
                "Marcos Antônio", "Estevam", "Paulo Ricardo", "Nycolas", "Sampaio", "Wellerson", "Adonai", "Odin", "Marinho", "Silvestre",
                "Ettore", "Aldair", "Juscelino", "Vincent", "Mustafá", "Ramiro", "Cândido", "Romero", "Felipe Gabriel", "Acabe",
                "Cam", "Marcel", "Carmelo", "Custódio", "Galdino", "Alonso", "Samael", "João Antônio", "Constantino", "Élio",
                "Uriah", "Dilan", "Cabral", "Jordan", "Basílio", "Aguinaldo", "Monteiro", "Dan", "Nildo", "Octavio",
                "Aníbal", "Otávio Augusto", "Euclides", "Deivison", "Evaldo", "Amós", "Beni", "Seth", "Khalil", "Claudemir",
                "Perseu", "Muniz", "Johnson", "Castro", "Égon", "Fred", "Ronildo", "Rodney", "Ralph", "Couto",
                "Josiel", "Bastos", "Aquino", "Aloísio", "Expedito", "Jhon", "Clemente", "Iori", "Valdomiro", "Paulino",
                "Eden", "Ednei", "Xamã", "João Marcelo", "Héber", "Tácio", "Sabino", "José Eduardo", "Salvatore", "Uahoomanaokahaku",
                "Calixto", "Élson", "Adelino", "Nino", "Juan Pablo", "Rivaldo", "Odilon", "Dirceu", "Cipriano", "Lee",
                "Zeca", "Romildo", "Chuck", "Stanley", "Magnus", "Alec", "Pascoal", "Divino", "Castilho", "Oto",
                "Ebenézer", "Leopoldo", "Barata", "Marcondes", "Nicodemus", "Jarbas", "Horus", "Paulo Victor", "Said", "Golias",
                "Valentino", "Habib", "Percy", "Kaled", "Alyson", "Fausto", "Floyd", "Francesco", "Cristóvão", "Firmino",
                "Thalisson", "Homero", "Ataíde", "Getúlio", "Nazareno", "Sean", "Baruque", "Apolinário", "Péricles", "Eraldo",
                "Florêncio", "Elliot", "Evangelista", "Valério", "Ézio", "Raj", "Spencer", "Alex Sandro", "Erasmo", "Abílio",
                "Adailton", "Hans", "Niklaus", "Fênix", "Amauri", "Lionel", "Elísio", "Norberto", "Gregory"
            };
            string[] mulher = //1000
            {
                "Isabela", "Lara", "Camila", "Letícia", "Valentina", "Luana", "Amanda", "Yasmin", "Sophia", "Juliana",
                "Cecília", "Bruna", "Fernanda", "Isadora", "Lorena", "Lívia", "Manuela", "Vitória", "Sara", "Aline",
                "Luna", "Luiza", "Gabriela", "Giovanna", "Jéssica", "Bianca", "Melissa", "Carolina", "Ester", "Vanessa",
                "Heloísa", "Rafaela", "Nicole", "Milena", "Isabella", "Laís", "Eloá", "Ana Clara", "Bárbara", "Emily",
                "Sabrina", "Raquel", "Maria Clara", "Thaís", "Catarina", "Patrícia", "Daniela", "Brenda", "Adriana", "Marina",
                "Eduarda", "Lavínia", "Isis", "Débora", "Priscila", "Alana", "Caroline", "Hadassa", "Maria Luíza", "Raissa",
                "Natália", "Emilly", "Isabel", "Ayla", "Alícia", "Luísa", "Joana", "Aurora", "Sarah", "Giovana",
                "Ana Beatriz", "Ana Carolina", "Maitê", "Ana Luíza", "Marcela", "Jaqueline", "Talita", "Luciana", "Diana", "Ana Júlia",
                "Clara", "Stephanie", "Evelyn", "Rayane", "Ingrid", "Ana Paula", "Liz", "Cristina", "Renata", "Elisa",
                "Jennifer", "Andressa", "Maya", "Pietra", "Ágata", "Geovana", "Alessandra", "Márcia", "Mayara", "Ohana",
                "Sandra", "Érica", "Antonella", "Janaina", "Cláudia", "Nina", "Laiane", "Kelly", "Graziela", "Tatiana",
                "Mirela", "Adriele", "Carla", "Gisele", "Zoé", "Elaine", "Tainá", "Samara", "Íris", "Taís",
                "Agatha", "Gabrielle", "Paula", "Viviane", "Olívia", "Michele", "Poliana", "Pamela", "Flávia", "Simone",
                "Mônica", "Karen", "Fátima", "Nathalia", "Eliane", "Andréia", "Lia", "Maria Júlia", "Tamires", "Pérola",
                "Lis", "Joyce", "Louise", "Cíntia", "Karina", "Emanuelly", "Rita", "Victória", "Esther", "Maria Fernanda",
                "Jade", "Tainara", "Kiara", "Laísa", "Micaela", "Roberta", "Ellen", "Fabiana", "Lorraine", "Nayara",
                "Giulia", "Eloah", "Maísa", "Rosana", "Marta", "Safira", "Franciele", "Aisha", "Sônia", "Paloma",
                "Clarice", "Isabelly", "Lilian", "Isabele", "Manuella", "Eva", "Regina", "Gabriele", "Ana Laura", "Francisca",
                "Paola", "Miriam", "Emmanuelle", "Ariane", "Valéria", "Alexia", "Agnes", "Samira", "Verônica", "Érika",
                "Monique", "Danielle", "Alexandra", "Lúcia", "Natalie", "Maria Luísa", "Rosângela", "Jenifer", "Geovanna", "Rayssa",
                "Estela", "Elisângela", "Denise", "Stefany", "Michelle", "Maria Alice", "Karine", "Elizabeth", "Mariah", "Dayane",
                "Sophie", "Maria Vitória", "Iara", "Betina", "Antônia", "Inês", "Silvana", "Laila", "Dandara", "Sheila",
                "Eliana", "Ângela", "Ludmila", "Marlene", "Cibele", "Vânia", "Sueli", "Kamila", "Solange", "Lana",
                "Mara", "Thalia", "Edna", "Ketlyn", "Kaylane", "Tatiane", "Carol", "Isabelle", "Carina", "Eloísa",
                "Cristiane", "Maia", "Bella", "Noemi", "Melina", "Hannah", "Ana Luísa", "Morgana", "Rute", "Lídia",
                "Malu", "Margarida", "Karoline", "Raiane", "Daiane", "Marisa", "Marília", "Rosa", "Iasmin", "Andréa",
                "Sílvia", "Maria Cecília", "Anita", "Kátia", "Tânia", "Elis", "Vivian", "Jamile", "Natasha", "Mel",
                "Célia", "Yasmim", "Teresa", "Keila", "Suelen", "Luara", "Daniele", "Queren Hapuque", "Leonor", "Samantha",
                "Alisson", "Cristiana", "Vera", "Maíra", "Hellen", "Andreza", "Joice", "Thamires", "Elena", "Marjorie",
                "Anne", "Stella", "Luma", "Madalena", "Nicolly", "Yara", "Kimberly", "Susana", "Diane", "Suzana",
                "Lidiane", "Cássia", "Angélica", "Késia", "Suellen", "Kauane", "Neusa", "Maria Helena", "Camille", "Luzia",
                "Conceição", "Eliza", "Ana Lívia", "Liliane", "Jacqueline", "Manoela", "Maiara", "Gabrielly", "Amélia", "Jane",
                "Layla", "Izabele", "Dominique", "Leila", "Joseane", "Cleide", "Katherine", "Yohanna", "Quezia", "Nádia",
                "Thainá", "Charlotte", "Luciene", "Lígia", "Emanuela", "Kamilly", "Pandora", "Acsa", "Mia", "Karla",
                "Dalila", "Valquíria", "Naomi", "Elisabete", "Taiane", "Núbia", "Neide", "Aysha", "Analu", "Selma",
                "Mikaelly", "Tayla", "Josiane", "Antonela", "Ana Alice", "Anna", "Bia", "Deise", "Abigail", "Mariane",
                "Angelina", "Matilde", "Camilly", "Rose", "Aparecida", "Evellyn", "Rosilene", "Evelin", "Samanta", "Lauren",
                "Tâmara", "Isa", "Ana Vitória", "Celina", "Lucimar", "Julie", "Ariana", "Yumi", "Aylla", "Clarissa",
                "Flora", "Melinda", "Soraia", "Raísa", "Ruth", "Tereza", "Dara", "Dafne", "Emília", "Moana",
                "Nara", "Josefa", "Shirley", "Ravena", "Helen", "Thaynara", "Ana Cláudia", "Amora", "Heloise", "Chloe",
                "Vilma", "Ariele", "Maria Aparecida", "Jasmine", "Milene", "Mirian", "Maria de Fátima", "Marilene", "Joelma", "Cristal",
                "Ananda", "Melanie", "Thalita", "Maria Valentina", "Ana Maria", "Lua", "Ashley", "Irene", "Terezinha", "Hilary",
                "Janete", "Carmen", "Sasha", "Olga", "Filipa", "Maria Flor", "Roseli", "Emanuelle", "Ana Sofia", "Gabriely",
                "Ivone", "Tábata", "Jussara", "Kyara", "Rosemeire", "Leandra", "Eunice", "Hanna", "Telma", "Francine",
                "Jasmim", "Clarisse", "Gislaine", "Karol", "Rafaella", "Vera Lúcia", "Lourdes", "Maria José", "Serena", "Marli",
                "Naiara", "Jordana", "Maria Laura", "Betânia", "Elza", "Raimunda", "Noa", "Lilith", "Akemi", "Monalisa",
                "Jeane", "Miriã", "Ana Caroline", "Cleonice", "Ana Cristina", "Kauany", "Meg", "Anabela", "Emma", "Deborah",
                "Mayra", "Glaucia", "Thayná", "Izabel", "Ana Lúcia", "Emanuele", "Stephany", "Atena", "Annie", "Dilma",
                "Ana Flávia", "Gabriella", "Mirella", "Cátia", "Angel", "Ivy", "Hana", "Leide", "Dalva", "Lisa",
                "Nicoly", "Nilza", "Jaine", "Daiana", "Edilene", "Géssica", "Selena", "Duda", "Elizabete", "Rita de Cássia",
                "Fabíola", "Maria de Lourdes", "Iracema", "Elen", "Mary", "Ariadne", "Nataly", "Valdirene", "Meire", "Sarai",
                "Kemilly", "Stela", "Eloise", "Kaila", "Virgínia", "Esmeralda", "Katarina", "Lorrane", "Ana Cecília", "Léia",
                "Dora", "Mila", "Raabe", "Barbie", "Mikaela", "Ária", "Giselle", "Potira", "Regiane", "Catherine",
                "Thayla", "Iolanda", "Glória", "Lâmia", "Adriane", "Ivana", "Penélope", "Linda", "Élida", "Maria Rita",
                "Sandy", "Salete", "Roseane", "Kênia", "Socorro", "Beatrice", "Gleice", "Lucy", "Penina", "Vanda",
                "Cora", "Scarlet", "Nair", "Jamilly", "Teodora", "Liliana", "Désirée", "Aghata", "Andresa", "Julieta",
                "Dione", "kathleen", "Barbosa", "Maria Isabel", "Maria Heloísa", "Dulce", "Cleusa", "Lucilene", "Mafalda", "Magda",
                "Fabiane", "Suely", "Juliane", "Vanusa", "Odete", "Elsa", "Rúbia", "Jurema", "Leidiane", "Ada",
                "Adelaide", "Aimée", "Gabrieli", "Zara", "Gilmara", "Afrodite", "Renesmée", "Anastácia", "Anahí", "Ivonete",
                "Rosemary", "Lily", "Natacha", "Tiffany", "Muriel", "Mariele", "Iraci", "Jaciara", "Miranda", "Chiara",
                "Maria Beatriz", "Maria Antônia", "Amábile", "Marie", "Tamiris", "Zilda", "Sulamita", "Loren", "Emanueli", "Carmem",
                "Tália", "Celeste", "Hosana", "Eliete", "Carmo", "Rachel", "Cloé", "Benedita", "Stefani", "Sirlene",
                "Nazaré", "Carolaine", "Arlete", "Zaíra", "Marcelle", "Estéfani", "Rosane", "Wendy", "Naiane", "Sol",
                "Cassandra", "Venus", "Karolina", "Graça", "Felícia", "Larissa Manoela", "Isaura", "Karolaine", "Marinalva", "Filomena",
                "Margarete", "Dâmaris", "Nilda", "Estefânia", "Kim", "Kéfera", "Luciane", "Eliene", "Laura Beatriz", "Sophia Vitória",
                "Mirelle", "Ana Letícia", "Dayse", "Mari", "Amália", "Hilda", "Áurea", "Cícera", "Zélia", "Isabelli",
                "Crislaine", "Judite", "Josi", "Maria do Carmo", "Maristela", "Sebastiana", "Ana Carla", "Iana", "Violeta", "Lucineide",
                "Thays", "Elise", "Ana Lara", "Soraya", "Alzira", "Severina", "Cândida", "Jemima", "Claudete", "Anna Clara",
                "Elizete", "Alanis", "Leônidas", "Rejane", "Pilar", "Magali", "Goreti", "Rubi", "Petra", "Yarin",
                "Maria Gabriela", "Kellen", "Luz", "Greice", "Quésia", "Jezabel", "Lívia Maria", "Leda", "Kalina", "Dávila",
                "Thamiris", "Maria Sophia", "Valentine", "Ivete", "Tarsila", "Megan", "Naira", "Noemia", "Jamily", "Giuliana",
                "Lurdes", "Amélie", "Dina", "Marinete", "Mahina", "Maria Lúcia", "Edileusa", "Leona", "Micaele", "Gisleine",
                "Ana Elisa", "Geísa", "Melody", "Ludimila", "Charlote", "Adele", "Nadir", "Juraci", "Bernadete", "Xuxa",
                "Cléo", "Maria Carolina", "Anelise", "Raíra", "Kate", "Cinara", "Daisy", "Ema", "Elenice", "Alba",
                "Eugênia", "Mayla", "Eleonora", "Khadija", "Úrsula", "Ilana", "Alanna", "Damiana", "Hermione", "Silmara",
                "Amy", "Jacira", "Suri", "Alda", "Holly", "Aiko", "Dulce Maria", "Verena", "Maike", "Yolanda",
                "Aimê", "Frida", "Ângela Maria", "Celine", "Séfora", "Ione", "Tabita", "Adria", "Gisela", "Gaby",
                "Cida", "Janine", "Pamella", "Cynthia", "Geórgia", "Yanka", "Jandira", "Luise", "Anastasia", "Berenice",
                "Ivani", "Agar", "Blue Ivy", "Nice", "Açucena", "Nívea", "Aruana", "Zenilda", "Margot", "Cilene",
                "Zenaide", "Amara", "Augusta", "Janice", "Valesca", "Samia", "Antonieta", "Kássia", "Wilma", "Fabrícia",
                "Quitéria", "Sayuri", "Anabel", "Branca", "Jaci", "Ieda", "Grace", "Eulália", "Ana Rita", "Adélia",
                "Audrey", "Francis", "Sirley", "Salomé", "Hinata", "Cassiane", "Queren", "Marcilene", "Astrid", "Selene",
                "Ramona", "Tauane", "Ana Gabriela", "Dorcas", "Samila", "Kailani", "Valdete", "Ingride", "Aiyra", "Ártemis",
                "Maura", "Edite", "Lea", "Myrella", "Ilda", "Jessie", "Maria Tereza", "Florence", "Sakura", "Juliete",
                "Maria da Graça", "Luzinete", "Cris", "Irani", "Lindalva", "Maria Paula", "Marly", "Ágda", "Glenda", "Drielle",
                "Lolita", "Cléia", "Rosália", "Elvira", "Najla", "Maria Sofia", "Rayla", "Creuza", "Angelita", "Margareth",
                "Elane", "Ana Sophia", "Maria Cristina", "Marilda", "Josefina", "Doralice", "Neiva", "Edivânia", "Guilhermina", "Acácia",
                "Taíssa", "Gal", "Gilda", "Sthefanny", "Fábia", "Cacilda", "Lisiane", "Elisabeth", "Tuane", "Alcione",
                "Veridiana", "Ella", "Geni", "Marianne", "Maria Madalena", "Ednalva", "Paulina", "Maressa", "Geralda", "Nefertari",
                "Darlene", "Yoko", "Lila", "Dirce", "Carlota", "Irina", "Mabel", "Edilaine", "Leone", "Celma",
                "Sofie", "Olinda", "Winnie", "Adelina", "Pollyanna", "Maria Inês", "Ivanete", "Kristen", "Yonara", "Vasti",
                "Violetta", "Maria da Conceição", "Catrina", "Anália", "Queila", "Eloíza", "Magna", "Pricila", "Greta", "Lacerda",
                "Brígida", "Luci", "Cyndi", "Raven", "Ruana", "Marines", "Jasmin", "Josiana", "Blenda", "Esperança",
                "Bibiana", "Eveline", "Hera", "Imaculada", "Samela", "Josilene", "Lizandra", "Mércia", "Zilma", "Alina",
                "Kely", "Mercedes", "Keli", "Alessa", "Margô", "Josélia", "Donatella", "Idalina", "Jerusa", "Norma",
                "Neli", "Arabela", "Tamar", "Dolores", "Rosimeri", "Ninive", "Dália", "Iza", "Giullia", "Flaviane",
                "Emi", "Yasmine", "Zulmira", "Hagata", "Rosário", "Keity", "Gerlane", "Diva", "Chelsea", "Nancy",
                "Elizandra", "Heidi", "Lena", "Lucélia", "Lorrana", "Edina", "Julianne", "Opala", "Yoná", "Jael",
                "Lydia", "Brenna", "Claire", "Carmelita", "Thaisa", "Bela", "Lira", "Anny", "Gertrudes", "Fiona",
                "Domingas", "Constança", "Larisa", "Gina", "Lucineia", "Flor", "Mahara", "Belinda", "Zuleide"
            };
            int sexo = rnd.Next(0, 2);
            if (sexo == 0)
            {
                int index = rnd.Next(0, homem.Length);
                return homem[index];
            }
            else
            {
                int index = rnd.Next(0, mulher.Length);
                return mulher[index];
            }
        }
        internal static string GeradorSobrenome()
        {
            string[] sobrenome =
            {
                "Abranches", "Abreu", "Adães", "Adorno", "Agostinho", "Aguiar", "Albuquerque", "Alcântara", "Aleluia", "Alencar",
                "Almada", "Almeida", "Altamirano", "Alvarenga", "Álvares", "Alves", "Alvim", "Amaral", "Amigo", "Amor",
                "Amorim", "Anchieta", "Andrade", "Anes", "Anjos", "Anlicoara", "Antas", "Antunes", "Anunciação", "Aragão",
                "Aranha", "Araújo", "Arruda", "Ascensão", "Assis", "Assunção", "Ávila", "Azeredo", "Azevedo", "Baldaia",
                "Bandeira", "Barbosa", "Barros", "Barroso", "Bastos", "Batista", "Benevides", "Berenguer", "Bermudes", "Bernades",
                "Bernardes", "Bettencourt", "Bicalho", "Bispo", "Boaventura", "Bolsonaro", "Borba", "Borges", "Borsoi", "Botelho",
                "Braga", "Bragança", "Bragançãos", "Brandão", "Brasil", "Brasiliense", "Brito", "Bueno", "Cabral", "Café",
                "Caldas", "Camargo", "Camelo", "Caminha", "Camões", "Campos", "Canto", "Cardoso", "Carmo", "Carnaval",
                "Carneiro", "Carvalhal", "Carvalho", "Castilho", "Castro", "Cerejeira", "Chaves", "Chávez", "Coelho", "Coentrão",
                "Coimbra", "Constante", "Cordeiro", "Costa", "Costa e Silva", "Cotrim", "Coutinho", "Couto", "Cruz", "Cunha",
                "Curado", "Custódia", "Custódio", "Da Silva", "Dambros", "Das Dores", "Das Neves", "De Assunção", "De Lima", "De Morais",
                "De Sá", "De Sousa", "De Souza", "De Oliveira", "Dias", "Diegues", "Do Carmo", "Do Prado", "Do Rosário", "Dorneles",
                "Dos Santos", "Duarte", "Eça", "Einloft", "Encarnação", "Esteves", "Evangelista", "Exaltação", "Fagundes", "Faleiros",
                "Falópio", "Falqueto", "Faria", "Farias", "Faro", "Feijó", "Fernandes", "Ferrão", "Ferreira", "Ferrolho",
                "Ferronha", "Figo", "Figueira", "Figueiredo", "Figueiroa", "Filgueiras", "Fioravante", "Fonseca", "Fontes", "Fortaleza",
                "Frade", "França", "Freire", "Freitas", "Frota", "Furquim", "Furtado", "Galvão", "Gama", "Garcia",
                "Garrastazu", "Gato", "Gomes", "Gonçales", "Gonçalves", "Gonzaga", "Gouveia", "Guedes", "Guimarães", "Gusmão",
                "Henriques", "Hernandes", "Holanda", "Holtreman", "Homem", "Hora", "Hungria", "Imaculada", "Jardim", "Jordão",
                "Junqueira", "Lacerda", "Lancastre", "Lange", "Leitão", "Leite", "Leme", "Lima", "Lins", "Locatelli",
                "Lopes", "Luz", "Macedo", "Machado", "Maciel", "Madureira", "Maduro", "Magalhães", "Maia", "Mairinque",
                "Malafaia", "Malta", "Mariz", "Marnel", "Marques", "Martins", "Mascarenhas", "Massa", "Mata", "Matos",
                "Medeiros", "Médici", "Meireles", "Mello", "Melo", "Mendes", "Mendonça", "Menino", "Mesquita", "Miranda",
                "Monteiro", "Montenegro", "Moraes", "Morais", "Moreira", "Muniz", "Namorado", "Nantes", "Nascimento", "Navarro",
                "Naves", "Negreiros", "Negrete", "Neves", "Nóbrega", "Nogueira", "Noronha", "Nunes", "Oliva", "Oliveira",
                "Outeiro", "Pacheco", "Padrão", "Paes", "Pais", "Paiva", "Paixão", "Papanicolau", "Parga", "Pascal",
                "Pascoal", "Pasquim", "Patriota", "Peçanha", "Pedrosa", "Pedroso", "Peixoto", "Penha", "Pensamento", "Penteado",
                "Pereira", "Peres", "Pessoa", "Pestana", "Picão", "Pimenta", "Pimentel", "Pinheiro", "Pinto", "Pires",
                "Poeta", "Policarpo", "Porto", "Portugal", "Prado", "Proença", "Prudente", "Quaresma", "Queirós", "Queiroz",
                "Ramalhete", "Ramalho", "Ramires", "Ramírez", "Ramos", "Rangel", "Reis", "Resende", "Ribeiro", "Rios",
                "Rodrigues", "Roma", "Romão", "Sá", "Sacramento", "Sampaio", "Sampaulo", "Sampedro", "Sanches", "Santacruz",
                "Santana", "Santander", "Santarrosa", "Santiago", "Santos", "Saragoça", "Saraiva", "Saramago", "Sarmento", "Seixas",
                "Serra", "Serrano", "Silva", "Silveira", "Simões", "Siqueira", "Soares", "Soeiro", "Sousa", "Souza",
                "Soverosa", "Tavares", "Taveira", "Teixeira", "Teles", "Torquato", "Trindade", "Valadares", "Valente", "Varela",
                "Vasco", "Vasconcelos", "Vasques", "Vaz", "Veiga", "Velasques", "Veloso", "Viana", "Viegas", "Vieira"
            };
            int index = rnd.Next(0, sobrenome.Length);
            return sobrenome[index];
        }
        internal static string GeredorTelefone(string estado = "RS")
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
        internal static string GeredorTopico()
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
                "Padeiro contratado",
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