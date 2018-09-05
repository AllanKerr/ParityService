using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ParityService.Models.Enums;

namespace ParityService.Models.Entities
{
  public sealed class TargetPortfolio
  {
    private readonly ILazyLoader m_lazyLoader;

    #region  User Foreign Key

    private User m_user;

    [Key]
    public string UserId { get; private set; }

    public User User
    {
      get => m_user != null ? m_user : m_lazyLoader?.Load(this, ref m_user);
      private set { m_user = value; }
    }

    #endregion

    #region TargetAllocations

    private IList<TargetAllocation> m_allocations;

    public IList<TargetAllocation> Allocations
    {
      get => m_allocations != null ? m_allocations : m_lazyLoader?.Load(this, ref m_allocations);
      private set { m_allocations = value; }
    }

    #endregion

    private TargetPortfolio() { }

    private TargetPortfolio(ILazyLoader lazyLoader)
    {
      m_lazyLoader = lazyLoader;
    }

    public TargetPortfolio(string userId)
    {
      UserId = userId;
    }
  }
}
