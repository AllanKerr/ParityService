
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ParityService.Managers;
using ParityService.Questrade.Models.Entities;
using ParityService.Questrade.Models.Responses;
using ParityUI.Models;

namespace ParityService.Questrade
{
  public sealed class QuestradeClient
  {
    private const string ApiVersion = "v1";

    private readonly CredentialsManager m_credentialsManager;
    private readonly IHttpClientFactory m_clientFactory;
    private readonly string m_userId;
    private readonly int m_linkedAccountId;

    private HttpClient m_client;

    public QuestradeClient(CredentialsManager credentialsManager, IHttpClientFactory clientFactory, string userId, int linkedAccountId)
    {
      m_credentialsManager = credentialsManager;
      m_userId = userId;
      m_linkedAccountId = linkedAccountId;
      m_clientFactory = clientFactory;
    }

    private async Task<ICredentials> GetCredentials()
    {
      return await m_credentialsManager.GetCredentials(m_userId, m_linkedAccountId);
    }

    private async Task<HttpClient> GetAuthorizedClient()
    {
      ICredentials credentials = await GetCredentials();
      Uri baseAddress = new Uri(credentials.ApiServer);

      if (m_client == null || m_client.BaseAddress != baseAddress)
      {
        m_client = m_clientFactory.CreateClient();
        m_client.BaseAddress = baseAddress;
      }
      m_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(credentials.AccessTokenType, credentials.AccessToken);
      return m_client;
    }

    private static string BuildPath(params string[] components)
    {
      return string.Join('/', components);
    }

    private async Task<T> Fetch<T>(params string[] pathComponents)
    {
      string path = BuildPath(pathComponents);
      HttpClient client = await GetAuthorizedClient();
      HttpResponseMessage httpResponse = await client.GetAsync(path);
      httpResponse.EnsureSuccessStatusCode();

      return await httpResponse.Content.ReadAsAsync<T>();
    }

    public async Task<AccountsResponse> FetchAccounts()
    {
      return await Fetch<AccountsResponse>(ApiVersion, "accounts");
    }
  }
}