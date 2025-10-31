using System.Diagnostics;
using Formulario_Cadastro_Cliente.Models;
using Microsoft.AspNetCore.Mvc;

namespace Formulario_Cadastro_Cliente.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Editar()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
