using System.Collections.Generic;
using Newtonsoft.Json;
using ParityService.Questrade.Models.Entities;

namespace ParityService.Questrade.Models.Responses
{
  public class SymbolsSearchResponse
  {
    [JsonProperty("symbols")]
    public IList<EquitySymbol> Symbols { get; set; }
  }
}