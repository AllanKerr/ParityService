using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using ParityService.Questrade.Models;
using Questrade = ParityService.Questrade.Models;

namespace ParityUI.Models
{
  public sealed class Credentials
  {
    public int LinkedAccountId { get; private set; }

    public string AppUserId { get; private set; }

    public LinkedAccount LinkedAccount { get; private set; }

    public string RefreshToken { get; private set; }

    public string ApiServer { get; private set; }

    public string AccessToken { get; private set; }

    public string AccessTokenType { get; private set; }

    public DateTime AccessTokenExpiresAt { get; private set; }

    public Credentials(LinkedAccount link, Questrade.AuthToken token)
    {
      LinkedAccountId = link.Id;
      AppUserId = link.AppUserId;

      RefreshToken = token.RefreshToken;
      ApiServer = token.ApiServer;
      AccessToken = token.AccessToken;
      AccessTokenType = token.TokenType;
      AccessTokenExpiresAt = DateTime.UtcNow.AddSeconds(token.ExpiresIn);
    }

    private Credentials() {}
  }
}
