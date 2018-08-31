namespace ParityService.Models.Entities
{
  public interface ICredentials
  {
    string RefreshToken { get; }

    string ApiServer { get; }

    string AccessToken { get; }

    string AccessTokenType { get; }
  }
}
