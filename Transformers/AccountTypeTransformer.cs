using ParityService.Models.Enums;
using QuestradeAccountType = ParityService.Questrade.Models.Enums.AccountType;

namespace ParityService.Transformers
{
  public static class AccountTypeTransformer
  {
    public static AccountType Transform(QuestradeAccountType accountType)
    {
      switch (accountType)
      {
        case QuestradeAccountType.Cash:
          return AccountType.Cash;
        case QuestradeAccountType.Margin:
          return AccountType.Margin;
        case QuestradeAccountType.TFSA:
          return AccountType.TFSA;
        case QuestradeAccountType.RRSP:
          return AccountType.RRSP;
      }
      return AccountType.Unknown;
    }
  }
}