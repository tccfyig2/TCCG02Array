# TCC - Desenvolvimento Dynamics 365



## Objetivos do Projeto:

- Criação de um Robô: 
  Importar dados em massa de um sistema para outro.

- Elaboração de Plugins: 
  Impedir a importação de dados inválidos ou duplicados no sistema.

- Tratativa de dados vindos do cliente: :
  Verificam se os campos foram preenchidos com dados válidos.

- Origem do robô de criação:
  Popular o sistema de origem.
  Desenvolvimento de um robô para gerar dados.

  
  

## Pontos Principais, desafios e soluções

#### **Robô de criação:**
*Criação e associação dos dados com seus respectivos campos.*
*Conexão e criação das entidades no sistema*
*Armazenamento das identificações.*

##### <u>DIFICULDADES:</u>
**Lentidão ao enviar dados para o sistema.**
*Envio em pacotes.*

**Divisão dos Pacotes.**
*Quantidades.*

**Evitar duplicidade no código da ordem.**
*Alteração no sistema de busca.*

**Uso desnecessário de mecanismo de busca.**
*Armazenamento de identificadores.*



#### Robô de importação:
*Conexão e requerimento de dados do Sistema Origem.*
*Associação dos dados dos sistemas.*
*Conexão e envio dos dados para o sistema Destino e obtém a confirmação de entrega.*
*Marca dados como importados no sistema Origem.*

##### <u>DIFICULDADES:</u>
**Sistema de busca limitada – 5000 entidades.**
*Sistema de paginação.*

**Identificação e configuração dos campos.**
*Extensão do navegador e manipulação dos dados.*

**Manter a sequência dos dados na importação dos pacotes.**
*Inserção de contadores.*

**Não buscar entidades que já foram importadas.**
*Marcação das entidades como já importadas.*

**Dificuldade no uso do programa.**
*Criação de uma Interface.*





#### Tratativas cliente:

- Melhoria da experiência com o usuário.

- Alteração dos formulários e criação de máscaras.

- Evitar dados inválidos e duplicados.

- Verificação em JavaScript.

- Evitar burlar a validação em JavaScript.

- Criação de uma verificação via Plugin.

- Experiência com o usuário



##### <u>DIFICULDADES:</u>
**Criação de máscara.**
*Utilização de recurso existente no CRM.*

**Preenchimento automático de endereço a partir do CEP.**
*Utilização de Web Service (ViaCEP).*



#### JavaScript:
##### <u>DIFICULDADES:</u>
**Validação de CPF e CNPJ.**
*Desenvolvimento de uma validação.*

***Verificação de CPF e CNPJ duplicados.***
*Solução CRM REST Builder.*

#### Plugins:
##### <u>DIFICULDADES:</u>
**Validação de CPF e CNPJ.**
*Conversão dos validadores feitos em JavaScript para C#.*

**Verificação de CPF, CNPJ e do código da ordem duplicados.**
*Criação de um método para verificação de dados duplicados.*

**Evitar duplicidade de verificações.**
*Criação de uma chave.*



#### Melhorias futuras:
Implementação de multithreading para fornecer várias execuções simultaneamente.