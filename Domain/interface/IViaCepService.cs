using mail_api.Domain.DTO;
using mail_api.Domain.Model;

namespace mail_api.Domain.Interfaces 
{
    public interface IViaCepService
    {
        Task<CepInfo> FetchAddressByCep(cepRequest cep);
    }
}


