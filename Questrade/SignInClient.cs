using Microsoft.Extensions.Logging;
using ParityService.Questrade.Models;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ParityService.Questrade
{
  internal class SignInClient : ILiveSignInClient, IPracticeSignInClient
  {
    private const string TokenPath = "oauth2/token";

    private const string GrantTypeParameter = "grant_type";
    private const string RefreshTokenParameter = "refresh_token";

    private const string ResfreshGrantType = "refresh_token";

    private HttpClient m_client;
    private readonly ILogger<SignInClient> m_logger;

    public SignInClient(HttpClient client, ILogger<SignInClient> logger)
    {
      m_client = client;
      m_logger = logger;
    }

    private string BuildBody(string refreshToken)
    {
      NameValueCollection parameters = HttpUtility.ParseQueryString(string.Empty);
      parameters[GrantTypeParameter] = ResfreshGrantType;
      parameters[RefreshTokenParameter] = refreshToken;
      return $"{TokenPath}?{parameters}";
    }

    async Task<AuthToken> ISignInClient.SignIn(string refreshToken)
    {
      if (string.IsNullOrEmpty(refreshToken))
      {
        throw new ArgumentException("invalid refresh token: cannot be null or empty");
      }
      string query = BuildBody(refreshToken);

      HttpResponseMessage response = await m_client.GetAsync(query);
      response.EnsureSuccessStatusCode();
      return await response.Content.ReadAsAsync<AuthToken>();
    }
  }
}
