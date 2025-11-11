using System.ComponentModel.DataAnnotations;

namespace Formulario_Cadastro_Cliente.Models
{
    public class EditClienteViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório")]
        public string Telefone { get; set; }

        public string? CEP { get; set; }

        [Required(ErrorMessage = "A rua é obrigatória")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "O bairro é obrigatório")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório")]
        public string Estado { get; set; }

        public bool Ativo { get; set; } = true;
    }
}
