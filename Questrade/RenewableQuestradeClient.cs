using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ParityService.Managers;
using ParityService.Models.Entities;

namespace ParityService.Questrade
{
  public sealed class RenewableQuestradeClient : QuestradeClient
  {
    private readonly CredentialsManager m_credentialsManager;
    private readonly string m_userId;
    private readonly int m_ServiceLinkId;

    public RenewableQuestradeClient(IHttpClientFactory clientFactory, ILogger<QuestradeClient> logger, CredentialsManager credentialsManager, string userId, int ServiceLinkId) : base(clientFactory, logger)
    {
      m_credentialsManager = credentialsManager;
      m_userId = userId;
      m_ServiceLinkId = ServiceLinkId;
    }

    protected override async Task<ICredentials> GetCredentials()
    {
      return await m_credentialsManager.GetCredentials(m_userId, m_ServiceLinkId);
    }
  }
}