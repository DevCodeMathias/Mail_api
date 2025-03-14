using Dapper;
using Npgsql;
using mail_api.Domain.Model;
using mail_api.Domain.Interfaces;
using mail_api.Data.mappers;

namespace mail_api.Data
{
    public class CepRepository : ICepRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<CepRepository> _logger;

       
        public CepRepository(IConfiguration configuration, ILogger<CepRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }

       
        public async Task<CepInfo> GetAdressByCep(string cep)
        {
            const string sql = @"
                SELECT Cep, Logradouro, Complemento, Bairro, Localidade, Uf, Ibge, Gia, Ddd, Siafi
                FROM cepinfo
                WHERE Cep = @Cep";

            try
            {
               
                await using (var connection = new NpgsqlConnection(_connectionString))
                {
                    var result = await connection.QuerySingleOrDefaultAsync<CepInfo>(sql, new { Cep = cep }).ConfigureAwait(false);
                    return result;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error fetching data from the database {ex.Message}");
                throw new Exception("Error fetching data from the database", ex);
            }
        }

        public async Task<bool> Create(CepInfo cep)
        {
            const string sql = @"
                INSERT INTO cepinfo (Cep, Logradouro, Complemento, Bairro, Localidade, Uf, Ibge, Gia, Ddd, Siafi) 
                VALUES (@Cep, @Logradouro, @Complemento, @Bairro, @Localidade, @Uf, @Ibge, @Gia, @Ddd, @Siafi)";

            try
            {
            
                await using (var connection = new NpgsqlConnection(_connectionString))
                {
                    var cepDto = CepMapper.ToDomainModel(cep);
                    var result = await connection.ExecuteAsync(sql, cepDto).ConfigureAwait(false);
                    return result > 0;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error inserting data into the database {ex.Message}");
                throw new Exception("Error inserting data into the database", ex);
            }
        }
    }
}
