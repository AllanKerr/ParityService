using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ParityService.Extensions;
using ParityService.Models.Enums;
using QuestradeCredentials = ParityService.Questrade.Models.Entities.Credentials;

namespace ParityService.Models.Entities
{
  public sealed class Account
  {
    private readonly ILazyLoader m_lazyLoader;

    private decimal? m_contributionRoom;

    #region  User Foreign Key

    private User m_user;
    public string UserId { get; private set; }

    public User User
    {
      get => m_user != null ? m_user : m_lazyLoader?.Load(this, ref m_user);
      private set { m_user = value; }
    }

    #endregion

    #region SerivceLink Foreign Key

    private ServiceLink m_serviceLink;
    public int ServiceLinkId { get; private set; }
    public string ServiceLinkUserId { get; private set; }

    public ServiceLink ServiceLink
    {
      get => m_serviceLink != null ? m_serviceLink : m_lazyLoader?.Load(this, ref m_serviceLink);
      private set { m_serviceLink = value; }
    }

    #endregion

    [Key]
    public int Id { get; private set; }

    public string AccountName { get; private set; }

    [Required]
    public AccountType AccountType { get; private set; }

    [Column]
    public decimal? ContributionRoom
    {
      get
      {
        if (!AccountType.HasContributionLimit())
        {
          return null;
        }
        return m_contributionRoom != null ? m_contributionRoom : 0;
      }
      set
      {
        if (!AccountType.HasContributionLimit())
        {
          return;
        }
        m_contributionRoom = value < 0 ? 0 : value;
      }
    }

    public decimal Cash { get; set; }

    public Account(ServiceLink link, string accountName, AccountType accountType)
    {
      ServiceLinkId = link.Id;
      ServiceLinkUserId = link.UserId;
      AccountName = accountName;
      AccountType = accountType;
    }

    public Account(string userId, string accountName, AccountType accountType)
    {
      UserId = userId;
      AccountName = accountName;
      AccountType = accountType;
    }

    private Account(ILazyLoader lazyLoader)
    {
      m_lazyLoader = lazyLoader;
    }

    private Account() { }
  }
}
