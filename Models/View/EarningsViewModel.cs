using System;
using System.ComponentModel.DataAnnotations;
using ParityService.Models.Enums;

namespace ParityUI.Models.View
{
  public sealed class EarningsViewModel
  {
    [Required]
    public decimal AnnualIncome { get; set; }

    [Required]
    public Region Region { get; set; }

    public EarningsViewModel(Earnings earnings)
    {
      AnnualIncome = earnings.AnnualIncome;
      Region = earnings.Region;
    }

    public EarningsViewModel() { }
  }
}
