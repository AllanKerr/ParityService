using System.Threading.Tasks;
using ParityService.Questrade;
using ParityService.Questrade.Models.Entities;

namespace ParityService.Questrade
{
  public interface ISignInService
  {
    Task<AuthToken> SignIn(string refreshToken, bool isPractice);
  }
}
