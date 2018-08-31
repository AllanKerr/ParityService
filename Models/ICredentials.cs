namespace ParityService.Models
{
  public interface ICredentials
  {
    string RefreshToken { get; }

    string ApiServer { get; }

    string AccessToken { get; }

    string AccessTokenType { get; }
  }
}
