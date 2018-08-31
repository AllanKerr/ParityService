using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParityService.Questrade.Models.Entities;

namespace ParityService.Questrade.Authentication
{
  internal interface ISignInClient
  {
    Task<Credentials> SignIn(string refreshToken);
  }

  internal interface IPracticeSignInClient : ISignInClient
  {

  }

  internal interface ILiveSignInClient : ISignInClient
  {

  }
}
