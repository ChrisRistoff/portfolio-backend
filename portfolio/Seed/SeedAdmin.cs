using System.Text;
using Dapper;
using Npgsql;
using portfolio.Auth;
using portfolio.Models;

namespace portfolio.Seed;
public class SeedAdmin
{
    public static async Task Seed(string connectionString, IConfiguration configuration)
    {
        Admin[] adminData = AdminData.GetAdminData();

        // connect to database with dapper
        await using var connection = new NpgsqlConnection(connectionString);

        Console.WriteLine("Seeding admin...");
        Console.WriteLine("--------------------------------------------------------------");

        // hash password
        foreach (Admin admin in adminData)
        {
            admin.Password = new AuthService(configuration).HashPassword(admin.Password!);
        }

        // seed admin table
        foreach (Admin admin in adminData)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO admin (username, password, role) VALUES (");
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

        Console.WriteLine("Seeding complete!");
        Console.WriteLine("--------------------------------------------------------------");
    }
}
