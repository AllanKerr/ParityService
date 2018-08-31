using ParityService.Models.Enums;
using ParityService.Transformers;
using QuestradeAccount = ParityService.Questrade.Models.Entities.Account;

namespace ParityService.Models.View
{
  public sealed class AccountViewModel
  {
    public string AccountNumber { get; private set; }

    public AccountType AccountType { get; private set; }

    public AccountViewModel(QuestradeAccount account)
    {
      AccountNumber = account.Number;
      AccountType = AccountTypeTransformer.Transform(account.Type);
    }
  }
}