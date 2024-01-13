using Dapper;
using Npgsql;

namespace portfolio.Repositories;

public class ProjectInfoRepository
{
    private readonly IConfiguration _config;
    private readonly string? _connectionString;

    public ProjectInfoRepository(IConfiguration config)
    {
        _config = config;
        string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (env == "Development")
        {
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        if (env == "Production")
        {
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }
    }

    public async Task<IEnumerable<ProjectInfoRepository>> GetPersonalInfo(string? projectType)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        var sql = @"
            SELECT *
            FROM projects
            WHERE 1 = 1
        ";

        DynamicParameters parameters = new DynamicParameters();

        if (projectType != null)
        {
            sql += "AND type = @ProjectType";
            parameters.Add("ProjectType", projectType);
        }

        var result = await connection.QueryAsync<ProjectInfoRepository>(sql);
        return result;
    }
}
