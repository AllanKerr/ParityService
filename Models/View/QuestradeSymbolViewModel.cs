using System.ComponentModel.DataAnnotations;
using ParityService.Questrade.Models.Enums;

namespace ParityService.Models.View
{
  public sealed class QuestradeSymbolViewModel
  {
    [Required]
    public string Symbol { get; set; }

    [Required]
    public int SymbolId { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public SecurityType SecurityType { get; set; }

    [Required]
    public Exchange ListingExchange { get; set; }

    [Required]
    public Currency Currency { get; set; }
  }
}
