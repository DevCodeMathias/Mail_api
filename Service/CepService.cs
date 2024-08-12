using mail_api.Data;
using mail_api.DTO;
using mail_api.InternalInterface;
using mail_api.Model;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace mail_api.Service
{
    public class CepService
    {

        private readonly HttpClient _httpClient;
        private readonly ICepRepository _cepRepository;

        public CepService(HttpClient httpClient, ICepRepository repository)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _cepRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        private async Task<CepInfo> GetAddressByCep(string cep)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CepInfo>(json);
                }

                return null;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error obtaining address by CEP.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error processing request.", ex);
            }
        }

        public async Task<CepInfo> GetByCep(string cep)
        {
            string pattern = @"^(\d{5})[-]?(\d{3})$";
            Regex regex = new Regex(pattern);
            string cepFormat = regex.Replace(cep, "$1-$2");

            var address = await _cepRepository.GetAdressByCep(cepFormat);

            if (address == null)
            {
                return null;
            }

            return address;
        }

        public async Task<bool> PostAddressByCep(cepRequest cepRequest)
        {
            string pattern = @"^\d{5}[-]?\d{3}$";
            if (!Regex.IsMatch(cepRequest.Cep, pattern))
            {
                throw new ArgumentException("Invalid CEP format.");
            }

            var addressData = await GetAddressByCep(cepRequest.Cep);
            if (addressData == null)
            {
                return false;
            }

            var existingCep = await _cepRepository.GetAdressByCep(cepRequest.Cep);
            if (existingCep != null)
            {
                throw new InvalidOperationException("CEP already exists in the database.");
            }

            var result = await _cepRepository.Create(addressData);
            return result;
        }

    }
}
