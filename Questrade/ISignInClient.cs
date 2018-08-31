using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParityService.Questrade.Models.Entities;

namespace ParityService.Questrade
{
  internal interface ISignInClient
  {
    Task<AuthToken> SignIn(string refreshToken);
  }

  internal interface IPracticeSignInClient : ISignInClient
  {

  }

  internal interface ILiveSignInClient : ISignInClient
  {

  }
}
