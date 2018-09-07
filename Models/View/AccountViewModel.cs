using System.ComponentModel.DataAnnotations;
using ParityService.Extensions;
using ParityService.Models.Entities;
using ParityService.Models.Enums;
using ParityService.Transformers;

namespace ParityService.Models.View
{
  public sealed class AccountViewModel : IAccountInfo
  {
    public int Id { get; }

    [Required]
    public string AccountName { get; set; }

    [Required]
    public AccountType AccountType { get; set; }

    public decimal? ContributionRoom { get; set; }

    public decimal Cash { get; set; }

    public bool IsManaged { get; }

    public bool HasContributionLimit => AccountType.HasContributionLimit();

    public AccountViewModel(Account account)
    {
      Id = account.Id;
      AccountName = account.AccountName;
      AccountType = account.AccountType;
      IsManaged = account.ServiceLinkUserId != null;
      ContributionRoom = account.ContributionRoom;
      Cash = account.Cash;
    }

    public AccountViewModel() { }
  }
}