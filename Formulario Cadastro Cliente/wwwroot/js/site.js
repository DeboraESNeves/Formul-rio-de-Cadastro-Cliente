// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function abrirModalDeletar(id) {
    // Define o ID do cliente no campo hidden do modal
    const inputId = document.getElementById('deleteClientId');
    if (inputId) {
        inputId.value = id;
    }

    // Cria e exibe o modal Bootstrap
    const modalEl = document.getElementById('confirmDeleteModal');
    if (modalEl) {
        const modal = new bootstrap.Modal(modalEl);
        modal.show();
    }
}


// enter leva ao proximo campo
document.addEventListener("DOMContentLoaded", function () {
    const inputs = document.querySelectorAll("input, select, textarea");

    inputs.forEach((input, index) => {
        input.addEventListener("keydown", function (e) {
            if (e.key === "Enter") {
                e.preventDefault();
                const next = inputs[index + 1];
                if (next) {
                    next.focus();
                } else {
                    input.form?.submit();
                }
            }
        });
    });
});

//campo fica vazio depois se cep incorreto
document.addEventListener("DOMContentLoaded", function () {
    const cepInput = document.getElementById('CEP');

    if (cepInput) {
        cepInput.addEventListener('blur', function () {
            const cep = cepInput.value.replace(/\D/g, '');

            if (cep.length === 8) {
                fetch(`https://viacep.com.br/ws/${cep}/json/`)
                    .then(response => response.json())
                    .then(data => {
                        if (data.erro) {
                            alert('CEP não encontrado!');
                            limparCamposEndereco();
                        } else {
                            document.getElementById('Rua').value = data.logradouro || '';
                            document.getElementById('Bairro').value = data.bairro || '';
                            document.getElementById('Cidade').value = data.localidade || '';
                            document.getElementById('Estado').value = data.uf || '';
                        }
                    })
                    .catch(() => {
                        limparCamposEndereco();
                    });
            } else if (cep.length > 0) {
                limparCamposEndereco();
            }
        });
    }

    function limparCamposEndereco() {
        const campos = ['CEP', 'Rua', 'Bairro', 'Cidade', 'Estado'];
        campos.forEach(id => {
            const campo = document.getElementById(id);
            if (campo) campo.value = '';
        });
    }
});

