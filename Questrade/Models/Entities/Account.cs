using Newtonsoft.Json;
using ParityService.Questrade.Models.Enums;

namespace ParityService.Questrade.Models.Entities
{
  public class Account
  {
    [JsonProperty("type")]
    public AccountType Type { get; set; }

    [JsonProperty("number")]
    public string Number { get; set; }

    [JsonProperty("isPrimary")]
    public bool IsPrimary { get; set; }

    [JsonProperty("isBilling")]
    public bool IsBilling { get; set; }

    [JsonProperty("clientAccountType")]
    public string ClientAccountType { get; set; }

    [JsonProperty("status")]
    public AccountStatus Status { get; set; }
  }
}