using System.ComponentModel.DataAnnotations;
using ParityService.Models.Enums;

namespace ParityService.Models
{
  public sealed class Earnings
  {
    [Key]
    public string UserId { get; private set; }

    public decimal AnnualIncome { get; private set; }

    public Region Region { get; private set; }

    public Earnings(string userId)
    {
      UserId = userId;
    }

    private Earnings() { }

    public void Update(decimal annualIncome, Region region)
    {
      AnnualIncome = annualIncome;
      Region = region;
    }
  }
}
