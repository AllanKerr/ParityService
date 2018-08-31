using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ParityService.Questrade;
using QuestradeCredentials = ParityService.Questrade.Models.Entities.Credentials;
using ParityService.Data;
using ParityService.Models.Entities;

namespace ParityService.Managers
{
  public sealed class CredentialsManager
  {
    private const int ExpirationBuffer = 300;

    private readonly AppDbContext m_context;

    private readonly ILogger<CredentialsManager> m_logger;

    private readonly ISignInService m_signInService;

    public CredentialsManager(AppDbContext context, ILogger<CredentialsManager> logger, ISignInService signInService)
    {
      m_context = context;
      m_logger = logger;
      m_signInService = signInService;
    }

    public async Task<ICredentials> GetCredentials(string userId, int ServiceLinkId)
    {

      Credentials creds = m_context.Credentials.Find(ServiceLinkId, userId);
      if (creds == null)
      {
        m_logger.LogWarning($"No credentials found for {{{userId}, {ServiceLinkId}}}");
        throw new InvalidCredentialException("No credentials found for linked account.");
      }
      if (!creds.IsExpired(ExpirationBuffer))
      {
        m_logger.LogDebug($"Valid credentials found for {{{userId}, {ServiceLinkId}}}");
        return creds;
      }
      QuestradeCredentials questradeCredentials;
      bool isPractice = creds.ServiceLink.IsPractice;
      try
      {
        questradeCredentials = await m_signInService.SignIn(creds.RefreshToken, isPractice);
      }
      catch (Exception ex)
      {
        m_logger.LogError($"Failed to refresh Questrade account: {ex}");
        throw new InvalidCredentialException("An error occurred while attempting to refresh the credentials.");
      }
      creds.Update(questradeCredentials);
      m_context.SaveChanges();

      m_logger.LogDebug($"Expired credentials found for {{{userId}, {ServiceLinkId}}}");

      return creds;
    }
  }
}
