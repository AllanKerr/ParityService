using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ParityService.Managers;
using ParityService.Questrade.Models.Responses;
using ParityService.Models.Entities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using ParityService.Questrade.Models.Entities;
using System.Collections.Specialized;
using System.Web;

namespace ParityService.Questrade
{
  public abstract class QuestradeClient
  {
    private const string ApiVersion = "v1";

    private readonly IHttpClientFactory m_clientFactory;
    private HttpClient m_client;

    protected readonly ILogger<QuestradeClient> m_logger;

    public QuestradeClient(IHttpClientFactory clientFactory, ILogger<QuestradeClient> logger)
    {
      m_clientFactory = clientFactory;
      m_logger = logger;
    }

    protected abstract Task<ICredentials> GetCredentials();

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

    private async Task<T> Fetch<T>(string path, NameValueCollection parameters = null)
    {
      if (parameters != null && parameters.Count > 0)
      {
        path = $"{path}?{parameters}";
      }
      HttpClient client = await GetAuthorizedClient();
      HttpResponseMessage httpResponse = await client.GetAsync(path);
      httpResponse.EnsureSuccessStatusCode();

      return await httpResponse.Content.ReadAsAsync<T>();
    }

    public async Task<AccountsResponse> FetchAccounts()
    {
      string path = BuildPath(ApiVersion, "accounts");
      try
      {
        return await Fetch<AccountsResponse>(path);
      }
      catch (Exception ex)
      {
        m_logger.LogError($"Failed to fetch service accounts: {ex}");
        throw new HttpRequestException("Failed to fetch service accounts.", ex);
      }
    }

    public async Task<IList<EquitySymbol>> FindSymbols(string query, int? offset = null)
    {
      string path = BuildPath(ApiVersion, "symbols", "search");

      NameValueCollection parameters = HttpUtility.ParseQueryString(string.Empty);
      parameters["prefix"] = HttpUtility.UrlEncode(query);

      if (offset.HasValue)
      {
        parameters["offset"] = Math.Max(offset.Value, 0).ToString();
      }

      try
      {
        SymbolsSearchResponse response = await Fetch<SymbolsSearchResponse>(path, parameters);
        return response.Symbols;
      }
      catch (Exception ex)
      {
        m_logger.LogError($"Failed to find symbols: {ex}");
        throw new HttpRequestException("Failed to find symbols.", ex);
      }
    }
  }
}