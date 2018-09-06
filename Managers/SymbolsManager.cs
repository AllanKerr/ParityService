using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ParityService.Data;
using ParityService.Models.Entities;
using System.Linq;
using ParityService.Questrade;
using ParityService.Models.Enums;
using QuestradeSymbol = ParityService.Questrade.Models.Entities.EquitySymbol;
using System.Collections.Generic;

namespace ParityService.Managers
{
  public sealed class SymbolsManager
  {
    private readonly AppDbContext m_context;

    private readonly ILogger<AllocationsManager> m_logger;

    private readonly QuestradeClientFactory m_clientFactory;

    public SymbolsManager(AppDbContext context, ILogger<AllocationsManager> logger, QuestradeClientFactory clientFactory)
    {
      m_context = context;
      m_logger = logger;
      m_clientFactory = clientFactory;
    }

    private bool TryFindQuestradeLink(string userId, out ServiceLink serviceLink)
    {
      serviceLink = m_context.ServiceLinks.FirstOrDefault(link => link.UserId == userId && link.ServiceType == ServiceType.Questrade);
      return serviceLink != null;
    }

    public async Task<IEnumerable<QuestradeSymbol>> FindSymbol(string userId, string query)
    {
      if (!TryFindQuestradeLink(userId, out ServiceLink link))
      {
        throw new Exception("This account does not have a linked Questrade account");
      }
      QuestradeClient client = m_clientFactory.CreateClient(userId, link.Id);
      return await client.FindSymbols(query);
    }
  }
}
