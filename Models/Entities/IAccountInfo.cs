using ParityService.Models.Enums;

namespace ParityService.Models.Entities
{
  public interface IAccountInfo
  {
    string AccountName { get; }

    AccountType AccountType { get; }

    decimal? ContributionRoom { get; }

    bool HasContributionLimit { get; }
  }
}