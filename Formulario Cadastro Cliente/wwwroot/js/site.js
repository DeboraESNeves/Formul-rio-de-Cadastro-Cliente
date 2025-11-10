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
