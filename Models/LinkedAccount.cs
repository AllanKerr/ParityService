using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Questrade = ParityService.Questrade.Models;

namespace ParityUI.Models
{
  public sealed class LinkedAccount
  {
    private readonly ILazyLoader m_lazyLoader;

    private Credentials m_credentials;

    public int Id { get; private set; }

    public string AppUserId { get; private set; }

    public bool IsPractice { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Credentials Credentials
    {
      get => m_lazyLoader?.Load(this, ref m_credentials);
      private set { m_credentials = value; }
    }

    private LinkedAccount() { }

    private LinkedAccount(ILazyLoader lazyLoader)
    {
      m_lazyLoader = lazyLoader;
    }

    public LinkedAccount(string userId, bool isPractice)
    {
      AppUserId = userId;
      IsPractice = isPractice;
      CreatedAt = DateTime.UtcNow;
    }
  }
}
