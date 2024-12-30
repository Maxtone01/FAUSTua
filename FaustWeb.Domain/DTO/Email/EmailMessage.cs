using MimeKit;

namespace FaustWeb.Domain.DTO.Email;

public class EmailMessage
{
    public List<MailboxAddress> To { get; set; } = [];
    public string Subject { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public EmailMessage(IEnumerable<string> to, string subject, string content)
    {
        To.AddRange(to.Select(x => new MailboxAddress(string.Empty, x)));
        Subject = subject;
        Content = content;
    }

    public EmailMessage(IEnumerable<string> to,
        string subject,
        string template,
        params string[] parameters) : this(to, subject, string.Format(template, parameters)) { }
}
