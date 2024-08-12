using mail_api.DTO;
using mail_api.Model;

namespace mail_api.Interfaces
{
    public interface ICepService
    {
        Task<bool> PostAddressByCep(cepRequest cepRequest);
        Task<CepInfo> GetByCep(string cep);
    }
}
