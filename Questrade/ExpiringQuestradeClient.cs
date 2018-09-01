using System.Net.Http;
using System.Threading.Tasks;
using ParityService.Models.Entities;

namespace ParityService.Questrade
{
  public sealed class ExpiringQuestradeClient : QuestradeClient
  {
    private readonly ICredentials m_credentials;

    public ExpiringQuestradeClient(IHttpClientFactory clientFactory, ICredentials credentials) : base(clientFactory)
    {
      m_credentials = credentials;
    }

    protected override Task<ICredentials> GetCredentials()
    {
      return Task.FromResult(m_credentials);
    }
  }
}