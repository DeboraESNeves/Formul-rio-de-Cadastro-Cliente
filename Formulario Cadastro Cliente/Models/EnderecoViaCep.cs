namespace Formulario_Cadastro_Cliente.Models
{
    public class EnderecoViaCep
    {
        public string Cep {  get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public string Erro { get; set;}
    }
}
