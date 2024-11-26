using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.DTO.Auth;

public class LoginDto
{
    [Required(ErrorMessage = "Введіть пошту")]
    [EmailAddress(ErrorMessage = "Невірний формат")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введіть пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
