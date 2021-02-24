function CNPJ_Validacao_Duplicados(executionContext) {
  const cnpjCampo = executionContext.getFormContext().getAttribute("cred2_cnpj");
  if (cnpjCampo.getValue() == null) {
    return;
  }
  else if (validacao_CNPJ(cnpjCampo.getValue())) {
    Xrm.WebApi.online.retrieveMultipleRecords("account", "?$select=" + "cred2_cnpj").then(
      function success(results) {
        for (let i = 0; i < results.entities.length; i++) {
          const cnpjBanco = results.entities[i]["cred2_cnpj"];
  
          if (cnpjBanco == Xrm.Page.getAttribute("cred2_cnpj").getValue()) {
            cnpjCampo.setValue("");
            Xrm.Navigation.openAlertDialog("CNPJ já cadastrado!");
            return;
          }
        }
      },
      function (error) {
        cnpjCampo.setValue("");
        Xrm.Navigation.openAlertDialog("Erro na pesquisa!\n" + error.message);
        return;
      }
    );
  }
  else {
    cnpjCampo.setValue("");
    Xrm.Navigation.openAlertDialog("CNPJ Inválido!");
    return;
  }
};

function validacao_CNPJ(cnpjCampo) {
  cnpjCampo = cnpjCampo.replace(/[-./\s]/g, '');
  if (cnpjCampo.length != 14 ||
    cnpjCampo == "00000000000000" ||
    cnpjCampo == "11111111111111" ||
    cnpjCampo == "22222222222222" ||
    cnpjCampo == "33333333333333" ||
    cnpjCampo == "44444444444444" ||
    cnpjCampo == "55555555555555" ||
    cnpjCampo == "66666666666666" ||
    cnpjCampo == "77777777777777" ||
    cnpjCampo == "88888888888888" ||
    cnpjCampo == "99999999999999"
  ) {
    return false;
  }

  const multpDigito1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
  const multpDigito2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
  let cnpj = [];
  for (let i = 0; i < 12; i++)
  {
      cnpj[i] = parseInt(cnpjCampo[i]);
  }

  // Calculo primeiro dígito
  let soma = 0;
  for (let i = 0; i < 12; i++) {
    soma += multpDigito1[i] * cnpj[i];
  }

  cnpj[12] = Resto(soma);

  // Calculo segundo dígito
  soma = 0;
  for (let i = 0; i < 13; i++) {
    soma += multpDigito2[i] * cnpj[i];
  }

  cnpj[13] = Resto(soma);
  
  if (cnpjCampo == cnpj.join('')) {
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