namespace Formulario_Cadastro_Cliente.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string CEP { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
