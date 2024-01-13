using System.Text;
using Dapper;
using Npgsql;
using portfolio.Auth;
using portfolio.Models;

namespace portfolio.Seed;

public class SeedAdmin
{
    public static async Task Seed(string connectionString)
    {
        User[] adminData = AdminData.GetAdminData();

        // connect to database with dapper
        await using var connection = new NpgsqlConnection(connectionString);

        foreach (User admin in adminData)
        {
            admin.Password = new AuthService().HashPassword(admin.Password!);
        }

        Console.WriteLine("Seeding admin...");
        Console.WriteLine("--------------------------------------------------------------");

        // seed admin table
        foreach (User admin in adminData)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO admin (id, password, role) VALUES (");
            sql.Append("@Id, @Password, @Role");
            sql.Append(")");
            await connection.ExecuteAsync(sql.ToString(),
                new
                {
                    Id = admin.Id,
                    Password = admin.Password,
                    Role = admin.Role,
                }
            );
        }
    }
}
