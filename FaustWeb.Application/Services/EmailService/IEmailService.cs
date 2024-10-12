using FaustWeb.Domain.DTO.Email;

namespace FaustWeb.Application.Services.EmailService;

public interface IEmailService
{
    Task SendEmailAsync(EmailMessage message);
}
