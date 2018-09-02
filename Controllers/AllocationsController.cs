using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParityService.Models.Entities;
using ParityService.Models.View;
using System;
using System.Linq;

namespace ParityService.Controllers
{
  [Authorize]
  public sealed class AllocationsController : Controller
  {
    private readonly UserManager<User> m_userManager;
    private readonly ILogger<AllocationsController> m_logger;

    public AllocationsController(UserManager<User> userManager, ILogger<AllocationsController> logger)
    {
      m_userManager = userManager;
      m_logger = logger;
    }

    //[ValidateAntiForgeryToken]
    [HttpPut("[controller]/target", Name = "SetTargetAllocation")]
    public IActionResult SetTargetAllocation([FromBody] AllocationViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      if (model.Allocation.Count == 0)
      {
        ModelState.AddModelError(string.Empty, $"There must be at least one item in the allocation.");
        return BadRequest(ModelState);
      }
      if (model.Allocation.Count > 0)
      {
        decimal sum = Math.Round(model.Allocation.Values.Aggregate((a, b) => a + b));
        if (Math.Round(sum) != 100)
        {
          ModelState.AddModelError(string.Empty, $"The allocations must add up to 100%, received {sum}%.");
          return BadRequest(ModelState);
        }
      }
      string userId = m_userManager.GetUserId(HttpContext.User);
      foreach (var t in model.Allocation)
      {
        m_logger.LogCritical($"{t.Key} : {t.Value}");
      }
      return Ok();
    }
  }
}
