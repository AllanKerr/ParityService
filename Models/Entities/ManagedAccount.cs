using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ParityService.Models.Enums;
using QuestradeCredentials = ParityService.Questrade.Models.Entities.Credentials;

namespace ParityService.Models.Entities
{
  public sealed class ManagedAccount
  {
    private readonly ILazyLoader m_lazyLoader;

    private ServiceLink m_serviceLink;

    public int ServiceLinkId { get; private set; }

    public string UserId { get; private set; }

    public string AccountId { get; private set; }

    public ServiceLink ServiceLink
    {
      get => m_serviceLink != null ? m_serviceLink : m_lazyLoader?.Load(this, ref m_serviceLink);
      private set { m_serviceLink = value; }
    }

    public AccountType AccountType { get; private set; }

    public ManagedAccount(ServiceLink link, string accountId, AccountType accountType)
    {
      ServiceLinkId = link.Id;
      UserId = link.UserId;
      AccountId = accountId;
      AccountType = accountType;
    }

    private ManagedAccount(ILazyLoader lazyLoader)
    {
      m_lazyLoader = lazyLoader;
    }

    private ManagedAccount() { }
  }
}
