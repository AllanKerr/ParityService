using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ParityService.Models
{
  public sealed class User : IdentityUser
  {
    private readonly ILazyLoader m_lazyLoader;

    private Earnings m_earnings;

    public Earnings Earnings
    {
      get => m_earnings != null ? m_earnings : m_lazyLoader?.Load(this, ref m_earnings);
      private set { m_earnings = value; }
    }

    private List<LinkedAccount> m_linkedAccounts;

    public List<LinkedAccount> LinkedAccounts
    {
      get => m_linkedAccounts != null ? m_linkedAccounts : m_lazyLoader?.Load(this, ref m_linkedAccounts);
      private set { m_linkedAccounts = value; }
    }

    private User(ILazyLoader lazyLoader)
    {
      m_lazyLoader = lazyLoader;
    }

    public User(string username, string email)
    {
      UserName = username;
      Email = email;
    }

    private User() { }
  }
}
