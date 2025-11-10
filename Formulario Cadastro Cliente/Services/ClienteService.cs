using Formulario_Cadastro_Cliente.Data;
using Formulario_Cadastro_Cliente.Models;
using Microsoft.EntityFrameworkCore;


namespace Formulario_Cadastro_Cliente.Services
{
    public class ClienteService : IClienteService
    {
        private readonly AppDbContext _dbContext;
        private readonly ViaCepService _viaCepService;
        private const int TamanhoPagina = 10;


        public ClienteService(AppDbContext dbContext, ViaCepService viaCepService)
        {
            _dbContext = dbContext;
            _viaCepService = viaCepService;
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
        public async Task<AppServiceResult<Cliente>> AdicionarClienteAsync(AddClienteViewModel viewModel)
        {
            if (await CpfJaExisteAsync(viewModel.Cpf))
                return AppServiceResult<Cliente>.Failure("Já existe um cliente com este CPF.");

            if (string.IsNullOrWhiteSpace(viewModel.CEP))
            {
                if (string.IsNullOrWhiteSpace(viewModel.Rua) ||
                    string.IsNullOrWhiteSpace(viewModel.Bairro) ||
                    string.IsNullOrWhiteSpace(viewModel.Cidade) ||
                    string.IsNullOrWhiteSpace(viewModel.Estado))
                {
                    return AppServiceResult<Cliente>.Failure("Preencha o endereço completo (Rua, Bairro, Cidade e Estado) se não informar o CEP.");
                }
            }

            var enderecoViaCep = !string.IsNullOrWhiteSpace(viewModel.CEP)
                    ? await _viaCepService.ConsultarEnderecoPorCepAsync(viewModel.CEP)
                    : null;

            var cliente = new Cliente
            {
                Nome = viewModel.Nome,
                Cpf = viewModel.Cpf,
                Email = viewModel.Email,
                Telefone = viewModel.Telefone,
                Ativo = viewModel.Ativo,
                Endereco = new Endereco
                {
                    CEP = string.IsNullOrWhiteSpace(viewModel.CEP) ? null : viewModel.CEP,
                    Rua = enderecoViaCep?.Logradouro ?? viewModel.Rua,
                    Bairro = enderecoViaCep?.Bairro ?? viewModel.Bairro,
                    Cidade = enderecoViaCep?.Localidade ?? viewModel.Cidade,
                    Estado = enderecoViaCep?.Uf ?? viewModel.Estado
                }
            };

            await _dbContext.Clientes.AddAsync(cliente);
            await _dbContext.SaveChangesAsync();

            return AppServiceResult<Cliente>.Success(cliente);
        }

        public async Task<Cliente> ObterClientePorIdAsync(int id)
        {
            return await _dbContext.Clientes
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c=> c.Id == id);
        }

        public async Task<AppServiceResult<Cliente>> AtualizarClienteAsync(EditClienteViewModel viewModel)
        {
            if (await CpfJaExisteAsync(viewModel.Cpf, viewModel.Id))
                return AppServiceResult<Cliente>.Failure("Já existe um cliente com este CPF.");

            var cliente = await ObterClientePorIdAsync(viewModel.Id);
            if (cliente == null)
                return AppServiceResult<Cliente>.Failure("Cliente não encontrado");

            cliente.Nome = viewModel.Nome;
            cliente.Cpf = viewModel.Cpf;
            cliente.Email = viewModel.Email;
            cliente.Telefone = viewModel.Telefone;
            cliente.Ativo = viewModel.Ativo;

            if (!string.IsNullOrWhiteSpace(viewModel.CEP))
            {
                var enderecoViaCep = await _viaCepService.ConsultarEnderecoPorCepAsync(viewModel.CEP);

                cliente.Endereco.CEP = viewModel.CEP;
                cliente.Endereco.Rua = enderecoViaCep?.Logradouro ?? viewModel.Rua;
                cliente.Endereco.Bairro = enderecoViaCep?.Bairro ?? viewModel.Bairro;
                cliente.Endereco.Cidade = enderecoViaCep?.Localidade ?? viewModel.Cidade;
                cliente.Endereco.Estado = enderecoViaCep?.Uf ?? viewModel.Estado;
            }
            else
            {
                cliente.Endereco.CEP = null;
                cliente.Endereco.Rua = viewModel.Rua;
                cliente.Endereco.Bairro = viewModel.Bairro;
                cliente.Endereco.Cidade = viewModel.Cidade;
                cliente.Endereco.Estado = viewModel.Estado;
            }


            await _dbContext.SaveChangesAsync();
            return AppServiceResult<Cliente>.Success(cliente);
        }

        public async Task<bool> DeletarClienteAsync(int  id)
        {
            var cliente = await ObterClientePorIdAsync(id);
            if (cliente == null) return false;
            _dbContext.Clientes.Remove(cliente);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CpfJaExisteAsync(string cpf, int? clienteIdParaIgnorar = null)
        {
            var query = _dbContext.Clientes.Where(c => c.Cpf == cpf);
            if(clienteIdParaIgnorar.HasValue)
                query = query.Where(c => c.Id != clienteIdParaIgnorar.Value);

            return await query.AnyAsync();
        }
    }
}