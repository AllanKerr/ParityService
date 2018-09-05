using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ParityService.Models.Enums;

namespace ParityService.Models.Entities
{
  public sealed class TargetAllocation
  {
    private readonly ILazyLoader m_lazyLoader;


    #region  TargetPortfolio Foreign Key

    private TargetPortfolio m_portfolio;

    public string PortfolioUserId { get; private set; }

    public TargetPortfolio Portfolio
    {
      get => m_portfolio != null ? m_portfolio : m_lazyLoader?.Load(this, ref m_portfolio);
      private set { m_portfolio = value; }
    }

    #endregion

    [Required]
    public string Symbol { get; private set; }

    [Required]
    public decimal Proportion { get; set; }

    public TargetAllocation(string userId, string symbol, decimal proportion)
    {
      PortfolioUserId = userId;
      Symbol = symbol;
      Proportion = proportion;
    }

    private TargetAllocation(ILazyLoader lazyLoader)
    {
      m_lazyLoader = lazyLoader;
    }

    private TargetAllocation() { }
  }
}
