using Dapper;
using Npgsql;
using portfolio.Models;

namespace portfolio.Repositories;

public class PersonalInfoRepository
{
    private readonly IConfiguration _config;
    private readonly string? _connectionString;

    public PersonalInfoRepository(IConfiguration config)
    {
        _config = config;
        string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        if (env == "Development")
        {
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        if (env == "Testing")
        {
            _connectionString = _config.GetConnectionString("TestConnection");
        }

        if (env == "Production")
        {
            _connectionString = _config.GetConnectionString("ProductionConnection");
        }
    }

    public async Task<PersonalInfoModel> GetPersonalInfo()
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        var sql = @"
            SELECT *
            FROM personal_info
        ";

        var result = await connection.QueryAsync<PersonalInfoModel>(sql);
        return result.FirstOrDefault()!;
    }

    public async Task<PersonalInfoModel> UpdateTitle(UpdateTitleModel model)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        var sql = @"
            UPDATE personal_info
            SET title = @Title
            RETURNING *
        ";

        var result = await connection.QueryAsync<PersonalInfoModel>(sql, model);
        return result.FirstOrDefault()!;
    }

    public async Task<PersonalInfoModel> UpdateBio(UpdateBioModel model)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        var sql = @"
            UPDATE personal_info
            SET bio = @Bio
            RETURNING *
        ";

        var result = await connection.QueryAsync<PersonalInfoModel>(sql, model);
        return result.FirstOrDefault()!;
    }
}
