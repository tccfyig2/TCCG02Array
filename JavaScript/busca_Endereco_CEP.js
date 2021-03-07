function busca_Endereco_CEP() {
    const campoCEP = Xrm.Page.getAttribute("address1_postalcode").getValue();
    if (campoCEP == null) {
        return;
    }
    else {
        var cep = campoCEP.replace(/\D/g, '');
        if (cep != "") {
            var validacaoCEP = /^[0-9]{8}$/;
            if (validacaoCEP.test(cep)) {
                limpa_Form();
                var script = document.createElement('script');
                script.src = 'https://viacep.com.br/ws/' + cep + '/json/?callback=imprime_Endereco';
                document.body.appendChild(script);
            }
            else {
                erro("Formato de CEP inválido!");
                return;
            }
        }
    }
};

function imprime_Endereco(valorCampo) {
    if (!("erro" in valorCampo)) {
        Xrm.Page.getAttribute("address1_line1").setValue(valorCampo.logradouro);
        Xrm.Page.getAttribute("address1_line3").setValue(valorCampo.bairro);
        Xrm.Page.getAttribute("address1_city").setValue(valorCampo.localidade);
        Xrm.Page.getAttribute("address1_stateorprovince").setValue(valorCampo.uf);
    }
    else {
        erro("CEP não encontrado!");
    }
};

function limpa_Form() {
    Xrm.Page.getAttribute("address1_line1").setValue("");
    Xrm.Page.getAttribute("address1_line3").setValue("");
    Xrm.Page.getAttribute("address1_city").setValue("");
    Xrm.Page.getAttribute("address1_stateorprovince").setValue("");
};

function erro(mensagem) {
    limpa_Form();
    Xrm.Navigation.openAlertDialog(mensagem);
};