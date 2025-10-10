namespace Formulario_Cadastro_Cliente.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email {  get; set; }
        public string Telefone { get; set; }
        public bool Ativo {  get; set; } = true;

        public Endereco Endereco { get; set; }

    }
}
