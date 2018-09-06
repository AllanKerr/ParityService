using System;
using Newtonsoft.Json;
using ParityService.Questrade.Models.Enums;

namespace ParityService.Questrade.Models.Entities
{
  public class EquitySymbol
  {
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("symbolId")]
    public int SymbolId { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("securityType")]
    public SecurityType SecurityType { get; set; }

    [JsonProperty("listingExchange")]
    public string ListingExchangeValue { get; set; }

    [JsonIgnore]
    public Exchange ListingExchange
    {
      get
      {
        if (Enum.TryParse(ListingExchangeValue, true, out Exchange exchange))
        {
          return exchange;
        }
        return Exchange.Unknown;
      }
    }

    [JsonProperty("isQuotable")]
    public bool IsQuotable { get; set; }

    [JsonProperty("isTradable")]
    public bool IsTradable { get; set; }

    [JsonProperty("currency")]
    public Currency Currency { get; set; }
  }
}