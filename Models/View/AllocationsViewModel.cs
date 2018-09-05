using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ParityService.Models.View
{
  public sealed class AllocationsViewModel
  {
    [Required]
    public IDictionary<string, decimal> Allocations { get; set; }
  }
}