using System.ComponentModel.DataAnnotations;
using ParityService.Questrade.Models.Entities;
using ParityService.Questrade.Models.Enums;
using QuestradeSymbol = ParityService.Questrade.Models.Entities.EquitySymbol;

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

    public QuestradeSymbolViewModel(QuestradeSymbol questradeSymbol)
    {
      Symbol = questradeSymbol.Symbol;
      SymbolId = questradeSymbol.SymbolId;
      Description = questradeSymbol.Description;
      SecurityType = questradeSymbol.SecurityType;
      ListingExchange = questradeSymbol.ListingExchange;
      Currency = questradeSymbol.Currency;
    }
  }
}
