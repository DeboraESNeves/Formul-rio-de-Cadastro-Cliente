using Formulario_Cadastro_Cliente.Data;
using Formulario_Cadastro_Cliente.Models;
using Formulario_Cadastro_Cliente.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Formulario_Cadastro_Cliente.Controllers
{
    public class ClientesController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly HttpClient _httpClient;
        private readonly IClienteService _clienteService;
        public ClientesController(AppDbContext dbContext, IHttpClientFactory httpClientFactory, IClienteService clienteService)
        {
            this.dbContext = dbContext;
            _httpClient = httpClientFactory.CreateClient();
            _clienteService = clienteService;
        }

        private async Task<EnderecoViaCep> ConsultarEnderecoPorCep(string cep)
        {
            var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var endereco = System.Text.Json.JsonSerializer.Deserialize<EnderecoViaCep>(json,
                    new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return endereco;
            }

            return null;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddClienteViewModel viewModel)
        {
            var cepLimpo = viewModel.CEP.Trim();
            var cpfExistente = await dbContext.Clientes
                .AnyAsync(c => c.Cpf == viewModel.Cpf);
            if(cpfExistente)
            {
                ModelState.AddModelError("Cpf", "Já existe um cliente com este CPF");
                return View(viewModel);
            }
            
            var enderecoViaCep = await ConsultarEnderecoPorCep(viewModel.CEP);

            var cliente = new Cliente
            {
                Nome = viewModel.Nome,
                Cpf = viewModel.Cpf,
                Email = viewModel.Email,
                Telefone = viewModel.Telefone,
                Ativo = viewModel.Ativo,
                Endereco = new Endereco
                {
                    CEP = viewModel.CEP,
                    Rua = enderecoViaCep?.Logradouro ?? viewModel.Rua,
                    Bairro = enderecoViaCep?.Bairro ?? viewModel.Bairro,
                    Cidade = enderecoViaCep?.Localidade ?? viewModel.Cidade,
                    Estado = enderecoViaCep?.Uf ?? viewModel.Estado
                }
            };

            await dbContext.Clientes.AddAsync(cliente);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List");
        }


        [HttpGet]
        public async Task<IActionResult> List(int pagina = 1, bool apenasAtivos = false, string searchString = null)
        {
            var viewModel = await _clienteService.GetClienteListViewModelAsync(
   
                pagina,
                apenasAtivos,
                searchString
            );

    
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
           
            var cliente = await dbContext.Clientes
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente is null) return NotFound();

            var viewModel = new EditClienteViewModel
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Cpf = cliente.Cpf,
                Email = cliente.Email,
                Telefone = cliente.Telefone,
                Ativo = cliente.Ativo,
                CEP = cliente.Endereco.CEP,
                Rua = cliente.Endereco.Rua,
                Bairro = cliente.Endereco.Bairro,
                Cidade = cliente.Endereco.Cidade,
                Estado = cliente.Endereco.Estado
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EditClienteViewModel viewModel)
        {
            var cpfExistente = await dbContext.Clientes
                .AnyAsync(c => c.Cpf == viewModel.Cpf && c.Id != viewModel.Id);

            if (cpfExistente)
            {
                ModelState.AddModelError("Cpf", "Já existe um cliente com este CPF.");
                return View(viewModel);
            }

                
            var cliente = await dbContext.Clientes
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c => c.Id == viewModel.Id);

            if (cliente is not null)
            {
                cliente.Nome = viewModel.Nome;
                cliente.Cpf = viewModel.Cpf;
                cliente.Email = viewModel.Email;
                cliente.Telefone = viewModel.Telefone;
                cliente.Ativo = viewModel.Ativo;

                cliente.Endereco.CEP = viewModel.CEP;
                cliente.Endereco.Rua = viewModel.Rua;
                cliente.Endereco.Bairro = viewModel.Bairro;
                cliente.Endereco.Cidade = viewModel.Cidade;
                cliente.Endereco.Estado = viewModel.Estado;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(Cliente viewModel)
        {
            var cliente = await dbContext.Clientes
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (cliente is not null)
            {
                dbContext.Clientes.Remove(cliente);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List");
        }
    }
}
