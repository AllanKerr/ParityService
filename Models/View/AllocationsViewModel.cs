using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ParityService.Migrations;

namespace ParityService.Models.View
{
  public sealed class AllocationsViewModel
  {
    [Required]
    public IDictionary<string, decimal> Allocations { get; set; }

    public AllocationsViewModel(IDictionary<string, decimal> allocations)
    {
      Allocations = allocations;
    }
  }
}