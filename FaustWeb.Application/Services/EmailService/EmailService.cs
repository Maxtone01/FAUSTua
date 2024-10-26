using FaustWeb.Domain.DTO.Email;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using MimeKit;

namespace FaustWeb.Application.Services.EmailService;

public class EmailService(EmailConfiguration emailConfiguration, IWebHostEnvironment env) : IEmailService
{
    #region Constants

    private const string TemplatesFolderName = "Templates";
    private const string PasswordResetTemplateName = "PasswordReset.html";

    #endregion

    #region Private methods

    private delegate MimeEntity GetEmailBody(string content);

    private MimeEntity GetHTMLBody(string content)
    {
        return new BodyBuilder() { HtmlBody = content }.ToMessageBody();
    }

    private MimeEntity GetTextBody(string content)
    {
        return new TextPart(MimeKit.Text.TextFormat.Text) { Text = content };
    }

    private string GetHTMLTemplate(string templateName)
    {
        string filePath = Path.Combine(env.WebRootPath, TemplatesFolderName, templateName);
        return File.ReadAllText(filePath);
    }

    private MimeMessage CreateEmailMessage(EmailMessage message, GetEmailBody getEmailBody)
    {
        MimeMessage emailMessage = new();
        emailMessage.From.Add(new MailboxAddress(string.Empty, emailConfiguration.From));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;
        emailMessage.Body = getEmailBody(message.Content);

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

    #endregion

    #region IEmailService methods

    public async Task SendTextEmailAsync(EmailMessage message)
    {
        var emailMessage = CreateEmailMessage(message, GetTextBody);
        await SendAsync(emailMessage);
    }

    public async Task SendHTMLEmailAsync(EmailMessage message)
    {
        var emailMessage = CreateEmailMessage(message, GetHTMLBody);
        await SendAsync(emailMessage);
    }

    public async Task SendPasswordResetEmailAsync(string reciever, string resetLink)
    {
        EmailMessage message = new(
            [reciever],
            "Password reset",
            GetHTMLTemplate(PasswordResetTemplateName),
            resetLink);

        await SendHTMLEmailAsync(message);
    }

    #endregion
}
