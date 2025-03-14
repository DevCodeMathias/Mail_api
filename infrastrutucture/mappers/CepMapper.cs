using mail_api.Domain.Model;

namespace mail_api.Data.mappers
{
    public class CepMapper
    {
        public static CepInfo ToDomainModel(CepInfo cepInfo)
        {
            return new CepInfo
            {
                Cep = cepInfo.Cep,
                Logradouro = cepInfo.Logradouro,
                Complemento = cepInfo.Complemento,
                Bairro = cepInfo.Bairro,
                Localidade = cepInfo.Localidade,
                Uf = cepInfo.Uf,
                Ibge = cepInfo.Ibge,
                Gia = cepInfo.Gia,
                Ddd = cepInfo.Ddd,
                Siafi = cepInfo.Siafi
            };

        }

    }
}
