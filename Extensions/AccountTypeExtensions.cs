using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ParityService.Models.Enums;

namespace ParityService.Extensions
{
  public static class AccountTypeExtensions
  {
    public static bool HasContributionLimit(this AccountType type)
    {
      switch (type)
      {
        case AccountType.TFSA:
        case AccountType.RRSP:
          return true;
        case AccountType.Cash:
        case AccountType.Margin:
        case AccountType.Unknown:
        default:
          return false;
      }
    }
  }
}
