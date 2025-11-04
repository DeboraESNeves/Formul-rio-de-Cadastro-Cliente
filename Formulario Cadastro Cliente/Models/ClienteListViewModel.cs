using Formulario_Cadastro_Cliente.Models;
using System.Collections.Generic;

namespace Formulario_Cadastro_Cliente.Models
{
    public class ClienteListViewModel
    {
        public List<Cliente> Clientes { get; set; }
        public PageResult Paginacao { get; set; }
        public bool ApenasAtivos {  get; set; }
    }
}
