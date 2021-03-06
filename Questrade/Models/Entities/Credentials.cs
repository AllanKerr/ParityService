using Newtonsoft.Json;
using ParityService.Models.Entities;

namespace ParityService.Questrade.Models.Entities
{
  public sealed class Credentials : ICredentials
  {
    [JsonProperty("access_token")]
    public string AccessToken { get; internal set; }

    [JsonProperty("api_server")]
    public string ApiServer { get; internal set; }

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; internal set; }

    [JsonProperty("refresh_token")]
    public string RefreshToken { get; internal set; }

    [JsonProperty("token_type")]
    public string AccessTokenType { get; internal set; }
  }
}