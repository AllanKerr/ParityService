using System.ComponentModel.DataAnnotations;
using ParityService.Models.Entities;
using ParityService.Models.Enums;
using ParityService.Transformers;

namespace ParityService.Models.View
{
  public sealed class AccountViewModel
  {
    [Required]
    public string AccountName { get; set; }

    [Required]
    public AccountType AccountType { get; set; }

    public AccountViewModel(Account account)
    {
      AccountName = account.AccountName;
      AccountType = account.AccountType;
    }

    public AccountViewModel() { }
  }
}