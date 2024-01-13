using System.Text;
using Dapper;
using Npgsql;

namespace portfolio.Seed;

public class SeedProd
{
    public static async void Seed()
    {
        ProfileObject profileData = ProfileData.GetProfileData();
        ProjectObject[] projectData = ProjectData.GetProjectData();

        // get connection string from appsettings.json
        string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

        // connect to database with dapper
        await using var connection = new NpgsqlConnection(connectionString);

        // seed personal_info table
        await connection.ExecuteAsync(
            "INSERT INTO personal_info (name, email, bio, github, linkedin, image) VALUES (@Name, @Email, @Bio, @Github, @Linkedin, @Image)",
            new
            {
                Name = profileData.Name,
                Email = profileData.Email,
                Bio = profileData.Bio,
                Github = profileData.Github,
                Linkedin = profileData.Linkedin,
                Image = profileData.Image,
            }
        );

        // seed projects table
        foreach (ProjectObject project in projectData)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO projects (name, tagline, description, image, repo, link, tech_stack, type) VALUES (");
            sql.Append("@Name, @Tagline, @Description, @Image, @Repo, @Link, @TechStack, @Type");
            sql.Append(")");
            await connection.ExecuteAsync(sql.ToString(),
                new
                {
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
