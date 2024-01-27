using Dapper;
using Npgsql;
using portfolio.Auth;
using portfolio.Models;
using portfolio.Interfaces;

namespace portfolio.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly AuthService _authService;
    private readonly string? _connectionString;

    public AdminRepository(IConfiguration config, AuthService authService)
    {
        _authService = authService;

        string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        if (env == "Development")
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        if (env == "Testing")
        {
            _connectionString = config.GetConnectionString("TestConnection");
        }

        if (env == "Production")
        {
            _connectionString = config.GetConnectionString("ProductionConnection");
        }
    }

    public async Task<LoginResponseDto> LoginAdmin(Admin user)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        string token = _authService.GenerateJwtToken(user);

        return new LoginResponseDto
        {
            Username = user.Username,
            Role = user.Role,
            Token = token,
        };
    }

    public async Task<Admin> GetUser(string username)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        var sql = @"
            SELECT *
            FROM admin
            WHERE username = @Username
        ";

        var result = await connection.QueryAsync<Admin>(sql, new {Username = username});
        return result.FirstOrDefault()!;
    }
}

