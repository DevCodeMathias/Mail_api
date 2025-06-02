using mail_api.Application.Service;
using mail_api.Data;
using mail_api.Domain.DTO;
using mail_api.Domain.Interfaces;
using mail_api.Domain.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace mail_api.Service
{
    public class CepService : ICepService
    {
        
        private readonly IViaCepService _viaCepService;
        private readonly ICepRepository _cepRepository;
        private readonly ILogger<CepService> _logger;

        public CepService(IViaCepService viaCepService, ICepRepository repository, ILogger<CepService> logger)
        {
            _viaCepService = viaCepService ?? throw new ArgumentNullException(nameof(viaCepService));
            _cepRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
