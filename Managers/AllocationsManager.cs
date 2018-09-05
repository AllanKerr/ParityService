using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuestradeCredentials = ParityService.Questrade.Models.Entities.Credentials;
using ParityService.Data;
using ParityService.Models.Entities;
using ParityService.Questrade.Authentication;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;

namespace ParityService.Managers
{
  public sealed class AllocationsManager
  {
    private readonly AppDbContext m_context;

    private readonly ILogger<AllocationsManager> m_logger;

    public AllocationsManager(AppDbContext context, ILogger<AllocationsManager> logger, ISignInService signInService)
    {
      m_context = context;
      m_logger = logger;
    }

    public bool SetTargetPortfolio(string userId, IDictionary<string, decimal> allocations)
    {
      bool success = true;

      allocations = new Dictionary<string, decimal>(allocations);
      TargetPortfolio portfolio = m_context.TargetPortfolios.Find(userId);
      using (IDbContextTransaction transaction = m_context.Database.BeginTransaction())
      {
        try
        {
          if (portfolio == null)
          {
            portfolio = new TargetPortfolio(userId);
            m_context.TargetPortfolios.Add(portfolio);
            m_context.SaveChanges();
          }
          IList<TargetAllocation> currentAllocations = portfolio.Allocations.ToList();
          foreach (TargetAllocation allocation in currentAllocations)
          {
            string symbol = allocation.Symbol;
            if (allocations.ContainsKey(symbol))
            {
              allocation.Proportion = allocations[symbol];
              allocations.Remove(symbol);
            }
            else
            {
              m_context.TargetAllocations.Remove(allocation);
            }
            m_context.SaveChanges();
          }
          foreach (KeyValuePair<string, decimal> allocation in allocations)
          {
            var targetAllocation = new TargetAllocation(userId, allocation.Key, allocation.Value);
            m_context.TargetAllocations.Add(targetAllocation);
            m_context.SaveChanges();
          }
          transaction.Commit();
        }
        catch (Exception ex)
        {
          m_logger.LogError($"Failed to set target allocation: {ex}");
          transaction.Rollback();
          success = false;
        }
      }
      return success;
    }
  }
}
