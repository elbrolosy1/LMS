using System.Net;
using System.Net.Mail;
using BLL__Buisness_Logic_Layer_.Dtos.AccountManager;
using Microsoft.Extensions.Configuration;

public class SmtpEmailSender : IEmailSender
{
    private readonly IConfiguration _config;

    public SmtpEmailSender(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var smtpClient = new SmtpClient(_config["Email:Smtp:Host"])
        {
            Port = int.Parse(_config["Email:Smtp:Port"]),
            Credentials = new NetworkCredential(_config["Email:Smtp:Username"], _config["Email:Smtp:Password"]),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_config["Email:Smtp:From"]),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(toEmail);

        await smtpClient.SendMailAsync(mailMessage);
    }
}
