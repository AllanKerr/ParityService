using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ParityService.Models.View
{
  public sealed class AllocationViewModel
  {
    [Required]
    public IDictionary<string, decimal> Allocation { get; set; }
  }
}