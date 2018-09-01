using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ParityService.Models.Enums;
using QuestradeCredentials = ParityService.Questrade.Models.Entities.Credentials;

namespace ParityService.Models.Entities
{
  public sealed class ManagedAccount
  {
    private readonly ILazyLoader m_lazyLoader;

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

    public string AccountId { get; private set; }

    [Required]
    public AccountType AccountType { get; private set; }

    public ManagedAccount(ServiceLink link, string accountId, AccountType accountType)
    {
      ServiceLinkId = link.Id;
      ServiceLinkUserId = link.UserId;
      AccountId = accountId;
      AccountType = accountType;
    }

    public ManagedAccount(string userId, string accountName, AccountType accountType)
    {
      UserId = userId;
      AccountId = accountName;
      AccountType = accountType;
    }

    private ManagedAccount(ILazyLoader lazyLoader)
    {
      m_lazyLoader = lazyLoader;
    }

    private ManagedAccount() { }
  }
}
