using Dapper;
using Npgsql;
using portfolio.Models;

namespace portfolio.Repositories;

public class ProjectInfoRepository
{
    private readonly string? _connectionString;

    public ProjectInfoRepository(IConfiguration config)
    {
        var config1 = config;
        string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (env == "Testing")
        {
            _connectionString = config1.GetConnectionString("TestConnection");
        }
        if (env == "Development")
        {
            _connectionString = config1.GetConnectionString("DefaultConnection");
        }
        if (env == "Production")
        {
            _connectionString = config1.GetConnectionString("ProductionConnection");
        }
    }

    public async Task<IEnumerable<ProjectInfoModel>> GetProjectInfo(string? projectType)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var sql = "SELECT * FROM projects WHERE 1 = 1";

        DynamicParameters parameters = new DynamicParameters();

        if (projectType != null)
        {
            sql += " AND project_type = @ProjectType";
            parameters.Add("ProjectType", projectType);
        }

        var results = await connection.QueryAsync<dynamic>(sql, parameters);
        var projects = new List<ProjectInfoModel>();

        foreach (var result in results)
        {
            var project = new ProjectInfoModel
            {
                Id = result.id,
                Name = result.name,
                Tagline = result.tagline,
                Description = result.description,
                Image = result.image,
                Repo = result.repo,
                Link = result.link,
                TechStack = ((string[])result.tech_stack)?.ToArray(),
                Type = result.project_type
            };
            projects.Add(project);
        }

        return projects;
    }
}
