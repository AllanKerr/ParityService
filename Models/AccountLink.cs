using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Questrade = ParityService.Questrade.Models;

namespace ParityUI.Models
{
  public sealed class AccountLink
  {
    public string Id { get; private set; }

    public string AppUserId { get; private set; }

    public bool IsPractice { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Credentials Credentials { get; private set; }

    private AccountLink() {}
    
    public AccountLink(string userId, bool isPractice)
    {
      AppUserId = userId;
      IsPractice = isPractice;
      CreatedAt = DateTime.UtcNow;
    }
  }
}
