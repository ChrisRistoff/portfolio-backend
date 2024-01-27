using System.Text;
using Dapper;
using Npgsql;
using portfolio.Models;
using portfolio.Services;

namespace portfolio.Seed;

public class SeedTest
{
    public static async Task Seed(string connectionString, IConfiguration configuration)
    {
        ProfileObject profileData = ProfileTestData.GetProfileTestData();
        ProjectObject[] projectData = ProjectTestData.GetProjectTestData();
        Admin[] adminData = new TestAdminData().GetTestAdminData();

        await using var connection = new NpgsqlConnection(connectionString);

        Console.WriteLine("Seeding personal info...");
        Console.WriteLine("--------------------------------------------------------------");

        // seed personal_info table
        await connection.ExecuteAsync(
            "INSERT INTO personal_info (id, name, title, email, bio, github, linkedin, image) VALUES (@Id, @Name, @Title, @Email, @Bio, @Github, @Linkedin, @Image)",
            new
            {
                Id = profileData.Id,
                Name = profileData.Name,
                Title = profileData.Title,
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

        Console.WriteLine("Seeding admin...");
        Console.WriteLine("--------------------------------------------------------------");
        foreach (Admin admin in adminData)
        {
            admin.Password = new AuthService(configuration).HashPassword(admin.Password!);

            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO admin (username , password, role) VALUES (");
            sql.Append("@Username, @Password, @Role");
            sql.Append(")");
            await connection.ExecuteAsync(sql.ToString(),
                new
                {
                    Username = admin.Username,
                    Password = admin.Password,
                    Role = admin.Role,
                }
            );
        }
    }
}
