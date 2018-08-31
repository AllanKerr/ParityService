using System.Threading.Tasks;
using ParityService.Questrade;
using ParityService.Questrade.Models.Entities;

namespace ParityService.Questrade.Authentication
{
  public interface ISignInService
  {
    Task<Credentials> SignIn(string refreshToken, bool isPractice);
  }
}
