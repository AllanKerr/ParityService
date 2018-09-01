using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ParityService.Models.Entities;

namespace ParityService.Questrade
{
  public sealed class ExpiringQuestradeClient : QuestradeClient
  {
    private readonly ICredentials m_credentials;

    public ExpiringQuestradeClient(IHttpClientFactory clientFactory, ILogger<QuestradeClient> logger, ICredentials credentials) : base(clientFactory, logger)
    {
      m_credentials = credentials;
    }

    protected override Task<ICredentials> GetCredentials()
    {
      return Task.FromResult(m_credentials);
    }
  }
}