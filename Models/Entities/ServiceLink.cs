using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ParityService.Models.Enums;

namespace ParityService.Models.Entities
{
  public sealed class ServiceLink
  {
    private readonly ILazyLoader m_lazyLoader;

    private Credentials m_credentials;

    private IList<ManagedAccount> m_accounts;

    public int Id { get; private set; }

    public string UserId { get; private set; }

    [Required]
    public ServiceType ServiceType { get; private set; }

    [Required]
    public string ServiceId { get; private set; }

    public bool IsPractice { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Credentials Credentials
    {
      get => m_credentials != null ? m_credentials : m_lazyLoader?.Load(this, ref m_credentials);
      private set { m_credentials = value; }
    }

    public IList<ManagedAccount> Accounts
    {
      get => m_accounts != null ? m_accounts : m_lazyLoader?.Load(this, ref m_accounts);
      private set { m_accounts = value; }
    }

    private ServiceLink() { }

    private ServiceLink(ILazyLoader lazyLoader)
    {
      m_lazyLoader = lazyLoader;
    }

    public ServiceLink(string userId, ServiceType serviceType, string serviceId, bool isPractice)
    {
      UserId = userId;
      ServiceId = serviceId;
      ServiceType = serviceType;
      IsPractice = isPractice;
      CreatedAt = DateTime.UtcNow;
    }
  }
}
