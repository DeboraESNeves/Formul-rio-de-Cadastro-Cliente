using Formulario_Cadastro_Cliente.Models;

namespace Formulario_Cadastro_Cliente.Services
{
    public interface IClienteService
    {
        Task<ClienteListViewModel> GetClienteListViewModelAsync(
            int pagina,
            bool apenasAtivos,
            string searchString);
        Task<AppServiceResult<Cliente>> AdicionarClienteAsync(AddClienteViewModel viewModel);
        Task<Cliente> ObterClientePorIdAsync(int id);
        Task<AppServiceResult<Cliente>> AtualizarClienteAsync(EditClienteViewModel viewModel);
        Task<bool> DeletarClienteAsync(int id);
        Task<bool> CpfJaExisteAsync(string cpf, int? clienteIdParaIgnorar = null);
    }
}
