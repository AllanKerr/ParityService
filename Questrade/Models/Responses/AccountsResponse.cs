using System.Collections.Generic;
using Newtonsoft.Json;
using ParityService.Questrade.Models.Entities;

namespace ParityService.Questrade.Models.Responses
{
    public class AccountsResponse
    {
        [JsonProperty("accounts")]
        public IList<Account> Accounts { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }
    }
}