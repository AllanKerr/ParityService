using System;
using System.ComponentModel.DataAnnotations;
using ParityService.Models.Enums;

namespace ParityUI.Models.View
{
    public sealed class EarningsViewModel
    {
        [Required]
        public decimal AnnualIncome { get; } 

        [Required]
        public Region Region { get; }

        public EarningsViewModel(Earnings earnings) {
            AnnualIncome = earnings.AnnualIncome;
            Region = earnings.Region;
        }
    }
}
