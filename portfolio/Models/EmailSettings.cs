namespace portfolio.Models;

public class EmailSettings
{
    public string? MailServer { get; set; }
    public int MailPort { get; set; }
    public string? SenderName { get; set; }
    public string? Sender { get; set; }
    public string? Password { get; set; }
}

public class EmailDto
{
    public string? Name { get; set; }
    public string? EmailOfSender { get; set; }
    public string EmailToSendTo { get; set; } = "krsnhrstv@gmail.com";
    public string? Subject { get; set; }
    public string? Message { get; set; }
}
