using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.DTO.Auth;

public class RegisterDto
{
    [Required(ErrorMessage = "Nickname is required")]
    public string Nickname { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Repeat password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
    public string RepeatPassword { get; set; } = string.Empty;
}
