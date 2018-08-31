using ParityService.Questrade.Models.Entities;
using System.Threading.Tasks;

namespace ParityService.Questrade.Authentication
{
  internal sealed class SignInService : ISignInService
  {
    private readonly ILiveSignInClient m_liveClient;
    private readonly IPracticeSignInClient m_practiceClient;

    public SignInService(ILiveSignInClient liveClient, IPracticeSignInClient practiceClient)
    {
      m_liveClient = liveClient;
      m_practiceClient = practiceClient;
    }

    async Task<Credentials> ISignInService.SignIn(string refreshToken, bool isPractice)
    {
      ISignInClient client;
      if (isPractice)
      {
        client = m_practiceClient;
      }
      else
      {
        client = m_liveClient;
      }
      return await client.SignIn(refreshToken);
    }
  }
}
