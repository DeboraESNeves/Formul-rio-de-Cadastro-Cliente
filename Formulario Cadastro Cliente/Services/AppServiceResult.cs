namespace Formulario_Cadastro_Cliente.Services
{
    public class AppServiceResult
    {
        public bool Sucesso => !Erros.Any();
        public Dictionary<string, string> Erros { get; set; } = new();

        public void AdicionarErro(string campo, string mensagem)
        {
            Erros[campo ?? string.Empty] = mensagem;
        }

        public static AppServiceResult Ok() => new();
        public static AppServiceResult Falha(string campo, string mensagem)
        {
            var r = new AppServiceResult();
            r.AdicionarErro(campo, mensagem);
            return r;
        }
    }
}
