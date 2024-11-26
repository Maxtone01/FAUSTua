using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.DTO.Auth;

public class ResetPasswordDto
{
    [Required(ErrorMessage = "Введіть пароль")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Повторіть пароль")]
    [Compare("Password", ErrorMessage = "Паролі не співпадають")]
    public string? ConfirmPassword { get; set; }

    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}
