using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ParityUI.Models
{
    public sealed class AccountViewModel
    {
        [Required]
        public string AccountNumber { get; set; }

        public AccountType AccountType { get; set; }

        [Required]
        public bool HasContributionLimit { get; set; }

        public decimal? ContributionRoom { get; set; }
    }
}