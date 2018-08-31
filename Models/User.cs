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

    private List<ServiceLink> m_serviceLinks;

    public List<ServiceLink> ServiceLinks
    {
      get => m_serviceLinks != null ? m_serviceLinks : m_lazyLoader?.Load(this, ref m_serviceLinks);
      private set { m_serviceLinks = value; }
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
