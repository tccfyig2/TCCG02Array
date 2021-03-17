# TCC - Desenvolvimento Dynamics 365



## Objetivos do Projeto:

- Criação de um Robô: </br>
  Importar dados em massa de um sistema para outro.

- Elaboração de Plugins: </br>
  Impedir a importação de dados inválidos ou duplicados no sistema.

- Tratativa de dados vindos do cliente:</br>
  Verificam se os campos foram preenchidos com dados válidos.

- Origem do robô de criação:</br>
  Popular o sistema de origem.</br>
  Desenvolvimento de um robô para gerar dados.

  
  

## Pontos Principais, desafios e soluções

### **Robô de criação:**
*Criação e associação dos dados com seus respectivos campos.*</br>
*Conexão e criação das entidades no sistema*</br>
*Armazenamento das identificações.*

#### <ins>Dificuldades:</ins>
**Lentidão ao enviar dados para o sistema.** </br>
*Envio em pacotes.*

**Divisão dos Pacotes.** </br>
*Quantidades.*

**Evitar duplicidade no código da ordem.** </br>
*Alteração no sistema de busca.*

**Uso desnecessário de mecanismo de busca.** </br>
*Armazenamento de identificadores.*



### Robô de importação:
*Conexão e requerimento de dados do Sistema Origem.*</br>
*Associação dos dados dos sistemas.*</br>
*Conexão e envio dos dados para o sistema Destino e obtém a confirmação de entrega.*</br>
*Marca dados como importados no sistema Origem.*


#### <ins>Dificuldades:</ins>
**Sistema de busca limitada – 5000 entidades.** </br>
*Sistema de paginação.*

**Identificação e configuração dos campos.** </br>
*Extensão do navegador e manipulação dos dados.*

**Manter a sequência dos dados na importação dos pacotes.** </br>
*Inserção de contadores.*

**Não buscar entidades que já foram importadas.** </br>
*Marcação das entidades como já importadas.*

**Dificuldade no uso do programa.** </br>
*Criação de uma Interface.*





### Tratativas cliente:

- Melhoria da experiência com o usuário.

- Alteração dos formulários e criação de máscaras.

- Evitar dados inválidos e duplicados.

- Verificação em JavaScript.

- Evitar burlar a validação em JavaScript.

- Criação de uma verificação via Plugin.

- Experiência com o usuário



#### <ins>Dificuldades:</ins>

**Criação de máscara.** </br>
*Utilização de recurso existente no CRM.*

**Preenchimento automático de endereço a partir do CEP.** </br>
*Utilização de Web Service (ViaCEP).*



### JavaScript:

#### <ins>Dificuldades:</ins>

**Validação de CPF e CNPJ.** </br>
*Desenvolvimento de uma validação.*

**Verificação de CPF e CNPJ duplicados.** </br>
*Solução CRM REST Builder.*

### Plugins:

#### <ins>Dificuldades:</ins>

**Validação de CPF e CNPJ.** </br>
*Conversão dos validadores feitos em JavaScript para C#.*

**Verificação de CPF, CNPJ e do código da ordem duplicados.** </br>
*Criação de um método para verificação de dados duplicados.*

**Evitar duplicidade de verificações.** </br>
*Criação de uma chave.*



### Melhorias futuras:
Implementação de multithreading para fornecer várias execuções simultaneamente.
