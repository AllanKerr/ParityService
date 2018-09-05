using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ParityService.Models.Entities
{
  public sealed class User : IdentityUser
  {
    private readonly ILazyLoader m_lazyLoader;

    #region Earnings

    private Earnings m_earnings;

    public Earnings Earnings
    {
      get => m_earnings != null ? m_earnings : m_lazyLoader?.Load(this, ref m_earnings);
      private set { m_earnings = value; }
    }

    #endregion

    #region TargetPortfolio

    private TargetPortfolio m_targetPortfolio;

    public TargetPortfolio TargetPortfolio
    {
      get => m_targetPortfolio != null ? m_targetPortfolio : m_lazyLoader?.Load(this, ref m_targetPortfolio);
      private set { m_targetPortfolio = value; }
    }

    #endregion

    #region ServiceLinks

    private List<ServiceLink> m_serviceLinks;

    public List<ServiceLink> ServiceLinks
    {
      get => m_serviceLinks != null ? m_serviceLinks : m_lazyLoader?.Load(this, ref m_serviceLinks);
      private set { m_serviceLinks = value; }
    }

    #endregion

    #region LocalAccounts

    private List<Account> m_localAccounts;

    public List<Account> LocalAccounts
    {
      get => m_localAccounts != null ? m_localAccounts : m_lazyLoader?.Load(this, ref m_localAccounts);
      private set { m_localAccounts = value; }
    }

    #endregion

    private User() { }

    private User(ILazyLoader lazyLoader)
    {
      m_lazyLoader = lazyLoader;
    }

    public User(string username, string email)
    {
      UserName = username;
      Email = email;
    }
  }
}
