using Formulario_Cadastro_Cliente.Data;
using Formulario_Cadastro_Cliente.Models;
using Microsoft.EntityFrameworkCore;


namespace Formulario_Cadastro_Cliente.Services
{
    public class ClienteService : IClienteService
    {
        private readonly AppDbContext _dbContext;
        private const int TamanhoPagina = 10;

        public ClienteService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ClienteListViewModel> GetClienteListViewModelAsync(
            int pagina,
            bool apenasAtivos,
            string searchString)
        {
            var clientesQuery = _dbContext.Clientes
                .Include(c => c.Endereco)
                .AsQueryable();

            if (apenasAtivos)
            {
                clientesQuery = clientesQuery.Where(c => c.Ativo);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                var termoBusca = searchString.ToLower();
                clientesQuery = clientesQuery.Where(c =>
                    c.Nome.ToLower().Contains(termoBusca) ||
                    c.Cpf.Contains(termoBusca)
                );
            }

            int totalItens = await clientesQuery.CountAsync();

            var pageResult = new PageResult(totalItens, pagina, TamanhoPagina);

            var clientesPagina = await clientesQuery
                .Skip((pageResult.PaginaAtual - 1) * pageResult.TamanhoPagina)
                .Take(pageResult.TamanhoPagina)
                .ToListAsync();

            return new ClienteListViewModel
            {
                Clientes = clientesPagina,
                Paginacao = pageResult,
                ApenasAtivos = apenasAtivos,
                SearchString = searchString
            };
        }
    }
}