using Formulario_Cadastro_Cliente.Data;
using Formulario_Cadastro_Cliente.Models;
using Formulario_Cadastro_Cliente.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Formulario_Cadastro_Cliente.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClienteService _clienteService;
        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddClienteViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var resultado = await _clienteService.AdicionarClienteAsync(viewModel);

            if (!resultado.IsSuccess)
            {
                ModelState.AddModelError("Cpf", resultado.ErrorMessage);
                return View(viewModel);
            }

            return RedirectToAction("List");
        }


        [HttpGet]
        public async Task<IActionResult> List(int pagina = 1, bool apenasAtivos = false, string searchString = null)
        {
            searchString = searchString?.Trim();

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
           
            var cliente = await _clienteService.ObterClientePorIdAsync(id);
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
            if (!ModelState.IsValid)
                return View(viewModel);

            var resultado = await _clienteService.AtualizarClienteAsync(viewModel);

            if (!resultado.IsSuccess)
            {
                ModelState.AddModelError(nameof(viewModel.Cpf), resultado.ErrorMessage);
                return View(viewModel);
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(int id)
        {
            await _clienteService.DeletarClienteAsync(id);
            return RedirectToAction("List");
        }
    }
}
