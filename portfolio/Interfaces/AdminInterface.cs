using portfolio.Models;

namespace portfolio.Interfaces;

public interface IAdminRepository
{
    Task<LoginResponseDto> LoginAdmin(Admin user);
    Task<Admin> GetUser(string username);
}
