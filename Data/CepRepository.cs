using System.Security.Cryptography.Xml;
using Dapper;
using Npgsql;

namespace mail_api.Data
{
    public class CepRepository: ICepRepository
    {
        private const string connectionString = " connection String  ";

        public async Task<CepInfo> GetAdressByCep(string cep)
        {
            const string sql = @"
                SELECT Cep, Logradouro, Complemento, Bairro, Localidade, Uf, Ibge, Gia, Ddd, Siafi
                FROM CepInfo
                WHERE Cep = @Cep";

            try
            {
                await using (var connection = new NpgsqlConnection(connectionString))
                {
                    var result = await connection.QuerySingleOrDefaultAsync<CepInfo>(sql, new { Cep = cep }).ConfigureAwait(false);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching data from the database", ex);
            }
        }

        public async Task<bool> Create(CepInfo cep)
        {
            const string sql = @"
                INSERT INTO CepInfo (Cep, Logradouro, Complemento, Bairro, Localidade, Uf, Ibge, Gia, Ddd, Siafi) 
                VALUES (@Cep, @Logradouro, @Complemento, @Bairro, @Localidade, @Uf, @Ibge, @Gia, @Ddd, @Siafi)";

            try
            {
                await using (var connection = new NpgsqlConnection(connectionString))
                {
                    var result = await connection.ExecuteAsync(sql, new
                    {
                        cep.Cep,
                        cep.Logradouro,
                        cep.Complemento,
                        cep.Bairro,
                        cep.Localidade,
                        cep.Uf,
                        cep.Ibge,
                        cep.Gia,
                        cep.Ddd,
                        cep.Siafi
                    }).ConfigureAwait(false);

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching data from the database", ex);
            }
        }

    }
}
