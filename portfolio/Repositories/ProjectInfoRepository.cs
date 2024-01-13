using Dapper;
using Npgsql;
using portfolio.Models;

namespace portfolio.Repositories;

public class ProjectInfoRepository
{
    private readonly IConfiguration _config;
    private readonly string? _connectionString;

    public ProjectInfoRepository(IConfiguration config)
    {
        _config = config;
        string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (env == "Testing")
        {
            _connectionString = _config.GetConnectionString("TestConnection");
        }
        else
        {
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }
    }

    public async Task<IEnumerable<ProjectInfoModel>> GetProjectInfo(string? projectType)
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

        var result = await connection.QueryAsync<ProjectInfoModel>(sql);
        return result;
    }
}
