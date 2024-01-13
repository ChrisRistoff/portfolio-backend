using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using portfolio.Models;

namespace portfolio.Services;

public class EmailService(IOptions<EmailSettings> emailSettings)
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart("plain") { Text = message };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, true);
            await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
