using Formulario_Cadastro_Cliente.Models;

namespace Formulario_Cadastro_Cliente.Services
{
    public interface IClienteService
    {
        Task<ClienteListViewModel> GetClienteListViewModelAsync(
            int pagina,
            bool apenasAtivos,
            string searchString);
    }
}
