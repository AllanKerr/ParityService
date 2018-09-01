using System.ComponentModel.DataAnnotations;
using ParityService.Models.Entities;
using ParityService.Models.Enums;
using ParityService.Transformers;

namespace ParityService.Models.View
{
  public sealed class AccountViewModel
  {
    public int Id { get; }

    [Required]
    public string AccountName { get; set; }

    [Required]
    public AccountType AccountType { get; set; }

    public bool IsManaged { get; private set; }

    public AccountViewModel(Account account)
    {
      Id = account.Id;
      AccountName = account.AccountName;
      AccountType = account.AccountType;
      IsManaged = account.ServiceLinkUserId != null;
    }

    public AccountViewModel() { }
  }
}