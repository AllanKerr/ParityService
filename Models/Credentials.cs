using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ParityService.Questrade.Models;
using QuestradeAuthToken = ParityService.Questrade.Models.AuthToken;

namespace ParityService.Models
{
  public sealed class Credentials : ICredentials
  {
    private readonly ILazyLoader m_lazyLoader;

    private ServiceLink m_ServiceLink;

    public int ServiceLinkId { get; private set; }

    public string UserId { get; private set; }

    public ServiceLink ServiceLink
    {
      get => m_ServiceLink != null ? m_ServiceLink : m_lazyLoader?.Load(this, ref m_ServiceLink);
      private set { m_ServiceLink = value; }
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

    public void Update(QuestradeAuthToken token)
    {
      RefreshToken = token.RefreshToken;
      ApiServer = token.ApiServer;
      AccessToken = token.AccessToken;
      AccessTokenType = token.TokenType;
      AccessTokenExpiresAt = DateTime.UtcNow.AddSeconds(token.ExpiresIn);
    }

    public Credentials(ServiceLink link, QuestradeAuthToken token)
    {
      ServiceLinkId = link.Id;
      UserId = link.UserId;

      Update(token);
    }

    private Credentials(ILazyLoader lazyLoader)
    {
      m_lazyLoader = lazyLoader;
    }

    private Credentials() { }
  }
}
