using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using QuestradeCredentials = ParityService.Questrade.Models.Entities.Credentials;

namespace ParityService.Models.Entities
{
  public sealed class Credentials : ICredentials
  {
    private readonly ILazyLoader m_lazyLoader;

    private ServiceLink m_serviceLink;

    public int ServiceLinkId { get; private set; }

    public string UserId { get; private set; }

    public ServiceLink ServiceLink
    {
      get => m_serviceLink != null ? m_serviceLink : m_lazyLoader?.Load(this, ref m_serviceLink);
      private set { m_serviceLink = value; }
    }
    public string RefreshToken { get; private set; }

    public string ApiServer { get; private set; }

    public string AccessToken { get; private set; }

    public string AccessTokenType { get; private set; }

    public DateTime AccessTokenExpiresAt { get; private set; }

    public bool IsExpired(int expirationBuffer = 0)
    {
      return AccessTokenExpiresAt.AddSeconds(-expirationBuffer) <= DateTime.UtcNow;
    }

    public void Update(QuestradeCredentials credentials)
    {
      RefreshToken = credentials.RefreshToken;
      ApiServer = credentials.ApiServer;
      AccessToken = credentials.AccessToken;
      AccessTokenType = credentials.AccessTokenType;
      AccessTokenExpiresAt = DateTime.UtcNow.AddSeconds(credentials.ExpiresIn);
    }

    public Credentials(ServiceLink link, QuestradeCredentials credentials)
    {
      ServiceLinkId = link.Id;
      UserId = link.UserId;

      Update(credentials);
    }

    private Credentials(ILazyLoader lazyLoader)
    {
      m_lazyLoader = lazyLoader;
    }

    private Credentials() { }
  }
}
