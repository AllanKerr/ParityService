using System.Net.Http;
using ParityService.Managers;
using ParityService.Models.Entities;

namespace ParityService.Questrade
{
  public sealed class QuestradeClientFactory
  {
    private readonly CredentialsManager m_credentialsManager;
    private readonly IHttpClientFactory m_clientFactory;

    public QuestradeClientFactory(CredentialsManager credentialsManager, IHttpClientFactory clientFactory)
    {
      m_credentialsManager = credentialsManager;
      m_clientFactory = clientFactory;
    }

    public QuestradeClient CreateClient(string userId, int ServiceLinkId)
    {
      return new RenewableQuestradeClient(m_clientFactory, m_credentialsManager, userId, ServiceLinkId);
    }

    public QuestradeClient CreateClient(ICredentials credentials)
    {
      return new ExpiringQuestradeClient(m_clientFactory, credentials);
    }
  }
}