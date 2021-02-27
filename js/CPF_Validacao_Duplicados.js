function CPF_Validacao_Duplicados(executionContext) {
  const cpfCampo = executionContext.getFormContext().getAttribute("cred2_cpf");
  const verificado = executionContext.getFormContext().getAttribute("cred2_verificado");
  verificado.setValue(null);
  if (cpfCampo.getValue() == null) {
    return;
  }
  else if(validacao_CPF(cpfCampo.getValue())) {
    Xrm.WebApi.online.retrieveMultipleRecords("contact", "?$select=" + "cred2_cpf").then(
      function success(results) {
        let security = true;
        for (let i = 0; i < results.entities.length; i++) {
          const cpfBanco = results.entities[i]["cred2_cpf"];
  
          if (cpfBanco == cpfCampo.getValue()) {
            security = false;
            cpfCampo.setValue("");
            Xrm.Navigation.openAlertDialog("CPF já cadastrado!");
            return;
          }
          if (security == true) {
            verificado.setValue("true");
          }
        }
      },
      function (error) {
        cpfCampo.setValue("");
        Xrm.Navigation.openAlertDialog("Erro na pesquisa!\n" + error.message);
        return;
      }
    );
  }
  else {
    cpfCampo.setValue("");
    Xrm.Navigation.openAlertDialog("CPF Inválido!");
    return;
  }
};

function validacao_CPF(cpfCampo) {
  cpfCampo = cpfCampo.replace(/[-.\s]/g, '');
  if (cpfCampo.length != 11 ||
    cpfCampo == "00000000000" ||
    cpfCampo == "11111111111" ||
    cpfCampo == "22222222222" ||
    cpfCampo == "33333333333" ||
    cpfCampo == "44444444444" ||
    cpfCampo == "55555555555" ||
    cpfCampo == "66666666666" ||
    cpfCampo == "77777777777" ||
    cpfCampo == "88888888888" ||
    cpfCampo == "99999999999"
  ) {
    return false;
  }

  const multpDigito1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
  const multpDigito2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];
  let cpf = [];
  for (let i = 0; i < 9; i++)
  {
      cpf[i] = parseInt(cpfCampo[i]);
  }

  // Calculo primeiro dígito
  let soma = 0;
  for (let i = 0; i < 9; i++) {
    soma += multpDigito1[i] * cpf[i];
  }

  cpf[9] = Resto(soma);

  // Calculo segundo dígito
  soma = 0;
  for (let i = 0; i < 10; i++) {
    soma += multpDigito2[i] * cpf[i];
  }

  cpf[10] = Resto(soma);

  if (cpfCampo == cpf.join('')) {
    return true;
  }
  else {
    return false;
  }
};

function Resto(soma) {
  let resto = soma % 11;
  if (resto < 2) {
    return 0;
  }
  else {
    return 11 - resto;
  }
};
