namespace portfolio.Models;

public class Admin
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }
}

public class LoginAdminDto
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}

public class LoginResponseDto
{
    public string? Username { get; set; }
    public string? Role { get; set; }
    public string? Token { get; set; }
}
