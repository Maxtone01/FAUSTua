using FaustWeb.Domain.DTO.Email;

namespace FaustWeb.Application.Services.EmailService;

public interface IEmailService
{
    Task SendTextEmailAsync(EmailMessage message);
    Task SendHTMLEmailAsync(EmailMessage message);
    Task SendPasswordResetEmailAsync(string email, string resetLink);
}
