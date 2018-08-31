using Newtonsoft.Json;

namespace ParityService.Questrade.Models.Entities
{
  public sealed class AuthToken
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
    public string TokenType { get; internal set; }
  }
}