using System.Text;
using Dapper;
using Npgsql;

namespace portfolio.Seed;

public class SeedProd
{
    public static async Task Seed(string connectionString)
    {
        ProfileObject profileData = ProfileData.GetProfileData();
        ProjectObject[] projectData = ProjectData.GetProjectData();

        // connect to database with dapper
        await using var connection = new NpgsqlConnection(connectionString);

        Console.WriteLine("Seeding personal info...");
        Console.WriteLine("--------------------------------------------------------------");
        // seed personal_info table
        await connection.ExecuteAsync(
            "INSERT INTO personal_info (id, name, email, bio, github, linkedin, image) VALUES (@Id, @Name, @Email, @Bio, @Github, @Linkedin, @Image)",
            new
            {
                Id = profileData.Id,
                Name = profileData.Name,
                Email = profileData.Email,
                Bio = profileData.Bio,
                Github = profileData.Github,
                Linkedin = profileData.Linkedin,
                Image = profileData.Image,
            }
        );

        Console.WriteLine("Seeding projects...");
        Console.WriteLine("--------------------------------------------------------------");
        // seed projects table
        foreach (ProjectObject project in projectData)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO projects (id, name, tagline, description, image, repo, link, tech_stack, project_type) VALUES (");
            sql.Append("@Id, @Name, @Tagline, @Description, @Image, @Repo, @Link, @TechStack, @Type");
            sql.Append(")");
            await connection.ExecuteAsync(sql.ToString(),
                new
                {
                    Id = project.Id,
                    Name = project.Name,
                    Tagline = project.Tagline,
                    Description = project.Description,
                    Image = project.Image,
                    Repo = project.Repo,
                    Link = project.Link,
                    TechStack = project.TechStack,
                    Type = project.Type,
                }
            );
        }
    }
}
