using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ParityUI.Models
{
    public sealed class AllocationsViewModel
    {
        [Required]
        public IDictionary<string, decimal> Allocations { get; set; }
    }
}