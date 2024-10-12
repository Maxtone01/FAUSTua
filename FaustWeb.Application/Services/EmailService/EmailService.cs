using FaustWeb.Domain.DTO.Email;
using MailKit.Net.Smtp;
using MimeKit;

namespace FaustWeb.Application.Services.EmailService;

public class EmailService(EmailConfiguration emailConfiguration) : IEmailService
{
    public async Task SendEmailAsync(EmailMessage message)
    {
        var emailMessage = CreateEmailMessage(message);
        await SendAsync(emailMessage);
    }

    private MimeMessage CreateEmailMessage(EmailMessage message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(string.Empty, emailConfiguration.From));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

        return emailMessage;
    }

    private async Task SendAsync(MimeMessage message)
    {
        using var client = new SmtpClient();

        await client.ConnectAsync(emailConfiguration.Smtp, emailConfiguration.Port, true);
        client.AuthenticationMechanisms.Remove("XOAUTH2");
        await client.AuthenticateAsync(emailConfiguration.Username, emailConfiguration.Password);
        await client.SendAsync(message);

        await client.DisconnectAsync(true);
        client.Dispose();
    }
}
