using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.DTO.Auth;

public class ResetPasswordDto
{
    [Required]
    public string? Password { get; set; }

    [Required]
    [Compare("Password")]
    public string? ConfirmPassword { get; set; }

    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}
