using portfolio.Models;

namespace portfolio.Interfaces;

interface IAuthService
{
    string GenerateJwtToken(Admin user);
    string HashPassword(string password);
    bool CheckPassword(string password, string hashedPassword);
}
