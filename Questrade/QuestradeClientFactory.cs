

using System.Net.Http;
using ParityService.Managers;

namespace ParityService.Questrade
{
  public sealed class QuestradeClientFactory
  {
    private readonly CredentialsManager m_credentialsManager;
    private readonly IHttpClientFactory m_clientFactory;

    public QuestradeClientFactory(CredentialsManager credentialsManager, IHttpClientFactory clientFactory) {
      m_credentialsManager = credentialsManager;
      m_clientFactory = clientFactory;
    }

    public QuestradeClient CreateClient(string userId, int linkedAccountId) {
      return new QuestradeClient(m_credentialsManager, m_clientFactory, userId, linkedAccountId);
    }
  }
}