using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ParityUI.Models
{
public sealed class AccountLinkViewModel
{
    [Required]
    public DateTime CreationTime { get; set;}

    [Required]
    public bool IsPractice { get; set; }

    [Required]
    public bool IsAuthenticated { get; set; }

    [Required]
    public IList<AccountViewModel> Accounts { get; set; }
}
}