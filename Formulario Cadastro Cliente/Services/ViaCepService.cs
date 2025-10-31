namespace Formulario_Cadastro_Cliente.Services
{
    public class ViaCepService
    {
        private readonly HttpClient _httpClient;
        private readonly LoggerService _logger;

        public ViaCepService(HttpClient httpClient, LoggerService logger)
        {
            _httpClient = httpClient; ;
            _logger = logger;
        }

        
    }
}
