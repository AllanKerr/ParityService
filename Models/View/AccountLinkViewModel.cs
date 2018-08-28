using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ParityUI.Models.View
{
  public sealed class AccountLinkViewModel
  {
    [Required]
    public DateTime CreationTime { get; set; }

    [Required]
    public bool IsPractice { get; set; }

    [Required]
    public bool IsAuthenticated { get; set; }
  }
}