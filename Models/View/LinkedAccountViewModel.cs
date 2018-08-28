using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ParityUI.Models.View
{
  public sealed class LinkedAccountViewModel
  {
    [Required]
    public string Id { get; private set; }

    [Required]
    public bool IsPractice { get; private set; }

    [Required]
    public DateTime CreatedAt { get; private set; }

    [Required]
    public bool IsAuthenticated { get; private set; }

    public LinkedAccountViewModel( AccountLink link) {
      Id = link.Id;
      IsPractice = link.IsPractice;
      CreatedAt = link.CreatedAt;
      IsAuthenticated = link.Credentials != null;
    }
  }
}