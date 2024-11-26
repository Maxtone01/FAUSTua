using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.DTO.Auth;

public class RegisterDto
{
    [Required(ErrorMessage = "Введіть нік")]
    public string Nickname { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введіть пошту")]
    [EmailAddress(ErrorMessage = "Невірний формат")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введіть пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Повторіть пароль")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Паролі не співпадають")]
    public string RepeatPassword { get; set; } = string.Empty;
}
