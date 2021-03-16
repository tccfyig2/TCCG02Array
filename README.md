# RoboCriadorDeItens
Objetivos do Pojeto:

Criação de um Robô:
Importar dados em massa de um sistema para outro.

Elaboração de Plugins:
Impedir a importação de dados inválidos ou duplicados no sistema.

Tratativa de dados vindos do cliente:
Verificam se os campos foram preenchidos com dados válidos.

Origem do robô de criação:
Popular o sistema de origem.
Desenvolvimento de um robô para gerar dados.

Fluxo do Robô de criação:
Criação e associação dos dados com seus respectivos campos.
Conexão e criação das entidades no sistema
Armazenamento das identificações.

DIFICULDADES:
Lentidão ao enviar dados para o sistema.
R: Emvio em pacotes.

Divisão dos Pacotes.
R: Quantidades.

Evitar duplicidade no código da ordem.
R: Alteração no sistema de busca.

Uso desnecessário de mecanismo de busca.
R: Armazenamento de identificadores.


Fluxo do Robô de importação:
Conexão e requerimento de dados do Sistema Origem.
Associação dos dados dos sistemas.
Conexão e envio dos dados para o sistema Destino e obtém a confirmação de entrega.
Marca dados como importados no sistema Origem.

DIFICULDADES:
Sistema de busca limitada – 5000 entidades.
R: Sistema de paginação.

Identificação e configuração dos campos.
R: Extensão do navegador e manipulação dos dados.

Manter a sequência dos dados na importação dos pacotes.
R: Inserção de contadores.

Não buscar entidades que já foram importadas.
R: Marcação das entidades como já importadas.

Dificuldade no uso do programa.
R: Criação de uma Interface.

Obter dados vindos do cliente e fazer as devidas tratativas:
- Melhoria da experiência com o usuário.
- Alteração dos formulários e criação de máscaras.
- Evitar dados inválidos e duplicados.
- Verificação em JavaScript.
- Evitar burlar a validação em JavaScript.
- Criação de uma verificação via Plugin.

- Experiência com o usuário
DIFICULDADES:
Criação de máscara.
R: Utilização de recurso existente no CRM.

Preenchimento automático de endereço a partir do CEP.
R: Utilização de Web Service (ViaCEP).

- JavaScript
DIFICULDADES:
Validação de CPF e CNPJ.
R: Desenvolvimento de uma validação.

Verificação de CPF e CNPJ duplicados.
R: Solução CRM REST Builder.

- Plugins
DIFICULDADES:
Validação de CPF e CNPJ.
R: Conversão dos validadores feitos em JavaScript para C#.

Verificação de CPF, CNPJ e do código da ordem duplicados.
R: Criação de um método para verificação de dados duplicados.

Evitar duplicidade de verificações.
R: Criação de uma chave.

Melhorias futuras:
Implementação de multithreading para fornecer várias execuções simultaneamente.
