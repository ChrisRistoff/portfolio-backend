using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portfolio.Auth;
using portfolio.Models;
using portfolio.Repositories;

namespace portfolio.Controllers;

[ApiController]
public class AdminController(AdminRepository adminRepository, AuthService authService) : ControllerBase
{

    [HttpPost("apis/login-admin")]
    public async Task<ActionResult<LoginResponseDto>> LoginAdmin(LoginAdminDto loginAdminDto)
    {
        try
        {
            if (string.IsNullOrEmpty(loginAdminDto.Username))
            {
                return BadRequest("Username is required");
            }

            if (string.IsNullOrEmpty(loginAdminDto.Password))
            {
                return BadRequest("Password is required");
            }

            Admin user = await adminRepository.GetUser(loginAdminDto.Username!);

            if (user == null)
            {
                return BadRequest("Username or password is incorrect");
            }

            // check password
            bool isPasswordCorrect = authService.CheckPassword(loginAdminDto.Password!, user.Password!);

            if (!isPasswordCorrect)
            {
                return BadRequest("Username or password is incorrect");
            }

            LoginResponseDto loginResponse = await adminRepository.LoginAdmin(user);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // I set this to false because I'm not using https but it's recommended to set it to true in production
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(30)
            };

            Response.Cookies.Append("AuthToken", loginResponse.Token!, cookieOptions);

            return Ok(loginResponse);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("apis/test-auth")]
    [Authorize]
    public async Task<IActionResult> TestAuth()
    {
        return Ok("You are authorized");
    }
}
