using ParityService.Models.Entities;
using ParityService.Models.Enums;
using ParityService.Transformers;

namespace ParityService.Models.View
{
  public sealed class AccountViewModel
  {
    public string AccountId { get; private set; }

    public AccountType AccountType { get; private set; }

    public AccountViewModel(Account account)
    {
      AccountId = account.AccountId;
      AccountType = account.AccountType;
    }
  }
}