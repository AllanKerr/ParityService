using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public sealed class AllocationsViewModel
{
    [Required]
    public IDictionary<string, decimal> Allocations { get; set; }
}