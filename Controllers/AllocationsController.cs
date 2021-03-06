using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParityService.Managers;
using ParityService.Models.Entities;
using ParityService.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParityService.Controllers
{
  [Authorize]
  public sealed class AllocationsController : Controller
  {
    private readonly UserManager<User> m_userManager;
    private readonly AllocationsManager m_allocationsManager;

    public AllocationsController(UserManager<User> userManager, AllocationsManager allocationsManager)
    {
      m_userManager = userManager;
      m_allocationsManager = allocationsManager;
    }

    //[ValidateAntiForgeryToken]
    [HttpPut("[controller]/target", Name = "SetTargetPortfolio")]
    public IActionResult SetTargetPortfolio([FromBody] AllocationsViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      if (model.Allocations.Count == 0)
      {
        ModelState.AddModelError(string.Empty, $"There must be at least one item in the allocation.");
        return BadRequest(ModelState);
      }
      if (model.Allocations.Count > 0)
      {
        decimal sum = Math.Round(model.Allocations.Values.Aggregate((a, b) => a + b));
        if (Math.Round(sum) != 100)
        {
          ModelState.AddModelError(string.Empty, $"The allocations must add up to 100%, received {sum}%.");
          return BadRequest(ModelState);
        }
      }
      string userId = m_userManager.GetUserId(HttpContext.User);
      bool success = m_allocationsManager.SetTargetPortfolio(userId, model.Allocations);
      if (!success)
      {
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
      return Ok();
    }

    [HttpGet("[controller]/target", Name = "GetTargetPortfolio")]
    public IActionResult GetTargetPortfolio()
    {
      string userId = m_userManager.GetUserId(HttpContext.User);
      TargetPortfolio portfolio = m_allocationsManager.GetTargetPortfolio(userId);
      if (portfolio == null)
      {
        return NotFound();
      }
      IDictionary<string, decimal> allocations = portfolio.Allocations.ToDictionary(allocation => allocation.Symbol, allocation => allocation.Proportion);
      return Ok(new AllocationsViewModel(allocations));
    }
  }
}
