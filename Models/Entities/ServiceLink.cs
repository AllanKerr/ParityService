using System;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ParityService.Models.Entities
{
  public sealed class ServiceLink
  {
    private readonly ILazyLoader m_lazyLoader;

    private Credentials m_credentials;

    public int Id { get; private set; }

    public string UserId { get; private set; }

    public bool IsPractice { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Credentials Credentials
    {
      get => m_credentials != null ? m_credentials : m_lazyLoader?.Load(this, ref m_credentials);
      private set { m_credentials = value; }
    }

    private ServiceLink() { }

    private ServiceLink(ILazyLoader lazyLoader)
    {
      m_lazyLoader = lazyLoader;
    }

    public ServiceLink(string userId, bool isPractice)
    {
      UserId = userId;
      IsPractice = isPractice;
      CreatedAt = DateTime.UtcNow;
    }
  }
}