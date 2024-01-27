using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using portfolio.Interfaces;
using portfolio.Models;

namespace portfolio.Services;

public class AuthService(IConfiguration configuration) : IAuthService
{
    public string GenerateJwtToken(Admin user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? string.Empty));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string HashPassword(string password)
    {
        var hasher = new PasswordHasher<Admin>();
        return hasher.HashPassword(null!, password);
    }

    public bool CheckPassword(string password, string hashedPassword)
    {
        var hasher = new PasswordHasher<Admin>();
        return hasher.VerifyHashedPassword(null!, hashedPassword, password) != PasswordVerificationResult.Failed;
    }
}
