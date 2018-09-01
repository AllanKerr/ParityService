using System.Net.Http;
using Microsoft.Extensions.Logging;
using ParityService.Managers;
using ParityService.Models.Entities;

namespace ParityService.Questrade
{
  public sealed class QuestradeClientFactory
  {
    private readonly CredentialsManager m_credentialsManager;
    private readonly IHttpClientFactory m_clientFactory;
    private readonly ILogger<QuestradeClient> m_logger;

    public QuestradeClientFactory(CredentialsManager credentialsManager, IHttpClientFactory clientFactory, ILogger<QuestradeClient> logger)
    {
      m_credentialsManager = credentialsManager;
      m_clientFactory = clientFactory;
      m_logger = logger;
    }

    public QuestradeClient CreateClient(string userId, int ServiceLinkId)
    {
      return new RenewableQuestradeClient(m_clientFactory, m_logger, m_credentialsManager, userId, ServiceLinkId);
    }

    public QuestradeClient CreateClient(ICredentials credentials)
    {
      return new ExpiringQuestradeClient(m_clientFactory, m_logger, credentials);
    }
  }
}