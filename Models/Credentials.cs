using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ParityService.Questrade.Models;
using Questrade = ParityService.Questrade.Models;

namespace ParityUI.Models
{
  public sealed class Credentials : ICredentials
  {
    private readonly ILazyLoader m_lazyLoader;

    private LinkedAccount m_linkedAccount;

    public int LinkedAccountId { get; private set; }

    public string AppUserId { get; private set; }

    public LinkedAccount LinkedAccount
    {
      get => m_linkedAccount != null ? m_linkedAccount : m_lazyLoader?.Load(this, ref m_linkedAccount);
      private set { m_linkedAccount = value; }
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

    public void Update(Questrade.AuthToken token)
    {
      RefreshToken = token.RefreshToken;
      ApiServer = token.ApiServer;
      AccessToken = token.AccessToken;
      AccessTokenType = token.TokenType;
      AccessTokenExpiresAt = DateTime.UtcNow.AddSeconds(token.ExpiresIn);
    }

    public Credentials(LinkedAccount link, Questrade.AuthToken token)
    {
      LinkedAccountId = link.Id;
      AppUserId = link.AppUserId;

      Update(token);
    }

    private Credentials(ILazyLoader lazyLoader)
    {
      m_lazyLoader = lazyLoader;
    }

    private Credentials() { }
  }
}
