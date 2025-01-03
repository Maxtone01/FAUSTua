﻿using System.ComponentModel.DataAnnotations;

namespace FaustWeb.Domain.DTO.Auth;

public class ForgotPasswordDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}
