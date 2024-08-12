using mail_api.Model;

namespace mail_api.InternalInterface
{
    public interface ICepRepository
    {
        Task<CepInfo> GetAdressByCep(string cep);
        Task<bool> Create(CepInfo cep);
    }
}
