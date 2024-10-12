namespace FaustWeb.Domain.DTO.Email;

public class EmailConfiguration
{
    public string From { get; set; } = string.Empty;
    public string Smtp { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
