using mail_api.Domain.DTO;
using mail_api.Domain.Model;
using Newtonsoft.Json;
using System.Net.Http;
using mail_api.Domain.Interfaces;


namespace mail_api.Application.Service
{
    public class ViaCepService : IViaCepService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ViaCepService> _logger;

        public ViaCepService(HttpClient httpClient, ILogger<ViaCepService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<CepInfo> FetchAddressByCep(cepRequest cepRequest)
        {

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cepRequest.Cep}/json/");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    try
                    {
                        return JsonConvert.DeserializeObject<CepInfo>(json);
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError($"Error deserializing response for CEP {cepRequest.Cep}: {jsonEx.Message}");
                        throw new Exception("Error processing response from the API.", jsonEx);
                    }
                }

                throw new Exception("Address not found for the provided CEP.");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HttpRequestException occurred while fetching address for CEP {cepRequest.Cep}: {ex.Message}");
                throw new Exception("Error obtaining address by CEP.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error occurred while fetching address for CEP {cepRequest.Cep}: {ex.Message}");
                throw new Exception("Unexpected error processing request.", ex);
            }
        }
    }
}
