$(document).ready(function () {
    // Máscaras
    $('.cpf').mask('000.000.000-00');
    $('.telefone').mask('(00)00000-0000');

    // Auto-preenchimento de endereço via CEP
    $('#CEP').on('blur', function () {
        const cep = $(this).val().replace(/\D/g, '');

        if (cep.length === 8) {
            fetch(`https://viacep.com.br/ws/${cep}/json/`)
                .then(res => res.json())
                .then(data => {
                    if (!data.erro) {
                        $('#Rua').val(data.logradouro || '');
                        $('#Bairro').val(data.bairro || '');
                        $('#Cidade').val(data.localidade || '');
                        $('#Estado').val(data.uf || '');
                    } else {
                        alert('CEP não encontrado. Preencha o endereço manualmente.');
                    }
                })
                .catch(() => alert('Erro ao consultar o CEP. Preencha o endereço manualmente.'));
        }
    });
});
