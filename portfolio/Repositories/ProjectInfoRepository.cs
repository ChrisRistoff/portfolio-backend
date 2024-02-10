using System.Text;
using Dapper;
using Npgsql;
using Org.BouncyCastle.Asn1.X509.Qualified;
using portfolio.Interfaces;
using portfolio.Models;

namespace portfolio.Repositories;

public class ProjectInfoRepository : IProjectInfo
{
    private readonly string? _connectionString;

    public ProjectInfoRepository(IConfiguration config)
    {
        string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (env == "Testing")
        {
            _connectionString = config.GetConnectionString("TestConnection");
        }
        if (env == "Development")
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
        if (env == "Production")
        {
            _connectionString = config.GetConnectionString("ProductionConnection");
        }
    }

    public async Task<IEnumerable<ProjectInfoModel>> GetProjectInfo(string? projectType)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        StringBuilder sql = new StringBuilder("SELECT * FROM projects WHERE 1 = 1");

        DynamicParameters parameters = new DynamicParameters();

        if (projectType != null)
        {
            sql.Append(" AND project_type = @ProjectType ");
            parameters.Add("ProjectType", projectType);
        }

        sql.Append(" ORDER BY id DESC");

        var results = await connection.QueryAsync<dynamic>(sql.ToString(), parameters);
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

    public async Task<ProjectInfoModel> CreateNewProject(CreateProjectDto project)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        DynamicParameters parameters = new DynamicParameters(project);

        string sql = @"INSERT INTO projects (id, name, tagline, description, image, repo, link, tech_stack, project_type)
                        VALUES (@Id, @Name, @Tagline, @Description, @Image, @Repo, @Link, @TechStack, @Type)
                        RETURNING *";

        var result = await connection.QueryFirstAsync<dynamic>(sql, parameters);

        return new ProjectInfoModel
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
    }

    public async Task<ProjectInfoModel> GetProjectById(int projectId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        var sql = "SELECT * FROM projects WHERE id = @ProjectId";

        var result = await connection.QueryFirstOrDefaultAsync<dynamic>(sql, new { ProjectId = projectId });

        if (result == null)
        {
            return null!;
        }

        return new ProjectInfoModel
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
    }

    public async Task<ProjectInfoModel> UpdateProject(int projectId, UpdateProjectDto project)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        string findProject = "SELECT * FROM projects WHERE id = @ProjectId";

        var projectToUpdate = await connection.QueryFirstOrDefaultAsync<ProjectInfoModel>(findProject, new { ProjectId = projectId });

        if (projectToUpdate == null)
        {
            return null!;
        }

        DynamicParameters parameters = new DynamicParameters(project);
        parameters.Add("ProjectId", projectId);

        string sql = @"UPDATE projects
                        SET id = @Id,
                            name = @Name,
                            tagline = @Tagline,
                            description = @Description,
                            image = @Image,
                            repo = @Repo,
                            link = @Link,
                            tech_stack = @TechStack,
                            project_type = @Type
                        WHERE id = @ProjectId
                        RETURNING *";

        var result = await connection.QueryFirstOrDefaultAsync<dynamic>(sql, parameters);

        return new ProjectInfoModel
        {
            Id = result!.id,
            Name = result.name,
            Tagline = result.tagline,
            Description = result.description,
            Image = result.image,
            Repo = result.repo,
            Link = result.link,
            TechStack = ((string[])result.tech_stack)?.ToArray(),
            Type = result.project_type
        };
    }

    public async Task DeleteProject(int projectId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        string sql = @"DELETE FROM projects WHERE id = @ProjectId";

        await connection.ExecuteAsync(sql, new { ProjectId = projectId });
    }
}
