using mail_api.Data;
using mail_api.Domain.DTO;
using mail_api.Domain.Model;
using Newtonsoft.Json;
using mail_api.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace mail_api.Service
{
    public class CepService : ICepService
    {
        
        private readonly IViaCepService _viaCepService;
        private readonly ICepRepository _cepRepository;
        private readonly ILogger<CepService> _logger;

        public CepService( ICepRepository repository)
        {
     
            this._cepRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


 
        public async Task<CepInfo> GetByCep(string cep)
        {

            CepInfo addressInRepository = await _cepRepository.GetAdressByCep(cep);
            return addressInRepository;
        }
        
     
        public async Task<bool> PostAddressByCep(cepRequest cepRequest)
        {
            try
            {
                CepInfo addressApiResponse = await _viaCepService.FetchAddressByCep(cepRequest);

                CepInfo existingCep = await _cepRepository.GetAdressByCep(cepRequest.Cep);
                if (existingCep != null)
                {
                   
                    return false;
                }

                bool creationStatus = await _cepRepository.Create(addressApiResponse);

                return creationStatus;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while processing or saving address for CEP {cepRequest.Cep}: {ex.Message}");
                throw new Exception("Error creating or fetching address.", ex);
            }
        }
       
    }
}
