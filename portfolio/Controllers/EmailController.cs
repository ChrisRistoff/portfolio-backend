using Microsoft.AspNetCore.Mvc;
using portfolio.Models;
using portfolio.Services;

namespace portfolio.Controllers
{
    [ApiController]
    public class MailController(EmailService emailService) : ControllerBase
    {
        [HttpPost("api/send-email")]
        public async Task<IActionResult> SendEmail(EmailDto emailDto)
        {
            try
            {
                if (string.IsNullOrEmpty(emailDto.Subject))
                {
                    return BadRequest("Subject is required");
                }

                if (string.IsNullOrEmpty(emailDto.Message))
                {
                    return BadRequest("Message is required");
                }

                if (string.IsNullOrEmpty(emailDto.EmailOfSender))
                {
                    return BadRequest("Email of sender is required");
                }

                if (string.IsNullOrEmpty(emailDto.Name))
                {
                    return BadRequest("Name is required");
                }

                emailDto.Message = $"Name: {emailDto.Name}\n\nEmail: {emailDto.EmailOfSender}\n\n{emailDto.Message}";

                await emailService.SendEmailAsync(emailDto.EmailToSendTo, emailDto.Subject!, emailDto.Message!);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
