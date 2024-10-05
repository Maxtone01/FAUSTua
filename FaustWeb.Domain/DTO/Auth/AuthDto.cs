namespace FaustWeb.Domain.DTO.Auth;

public class AuthDto
{
    public bool IsRegistration { get; set; }
    public LoginDto Login { get; set; } = new LoginDto();
    public RegisterDto Register { get; set; } = new RegisterDto();
}
