using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public sealed class RefreshTokenViewModel
{
    [Required]
    public string RefreshToken { get; set; }

    public bool IsPractice { get; set; }
}