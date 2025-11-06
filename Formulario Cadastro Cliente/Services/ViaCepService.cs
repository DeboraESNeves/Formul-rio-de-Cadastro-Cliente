using Formulario_Cadastro_Cliente.Models;
using System.Text.Json;

namespace Formulario_Cadastro_Cliente.Services
{
    public class ViaCepService
    {
        private readonly HttpClient _httpClient;
        private readonly LoggerService _logger;

        public ViaCepService(HttpClient httpClient, LoggerService logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<EnderecoViaCep> ConsultarEnderecoPorCepAsync(string cep)
        {
            try
            {
                var cepLimpo = new string(cep.Where(char.IsDigit).ToArray());

                if (cepLimpo.Length != 8)
                {
                    _logger.LogError($"CEP inválido: {cep}");
                    return null;
                }

                var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cepLimpo}/json/");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Erro ao consultar CEP {cep}: {response.StatusCode}");
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                var endereco = JsonSerializer.Deserialize<EnderecoViaCep>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return endereco;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao consultar ViaCEP: {ex.Message}");
                return null;
            }
        }
    }
}
