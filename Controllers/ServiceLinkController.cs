using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ParityService.Models.View;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using System.Collections.Generic;
using ParityService.Models.Entities;
using ParityService.Questrade;
using ParityService.Questrade.Models;
using ParityService.Managers;

namespace ParityService.Controllers
{
  [Authorize]
  public sealed class ServiceLinkController : Controller
  {
    private readonly UserManager<User> m_userManager;
    private readonly ILogger<ServiceLinkController> m_logger;
    private readonly ISignInService m_signInService;
    private readonly ServiceLinkManager m_serviceLinkManager;

    public ServiceLinkController(UserManager<User> userManager, ISignInService signInService, ServiceLinkManager serviceLinksManager, ILogger<ServiceLinkController> logger)
    {
      m_userManager = userManager;
      m_logger = logger;
      m_signInService = signInService;
      m_serviceLinkManager = serviceLinksManager;
    }

    //[ValidateAntiForgeryToken]
    [HttpPost("[controller]/[action]", Name = "LinkService")]
    public async Task<IActionResult> Add([FromBody] QuestradeLinkViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      AuthToken token;
      try
      {
        token = await m_signInService.SignIn(model.RefreshToken, model.IsPractice);
      }
      catch (Exception ex)
      {
        m_logger.LogWarning($"Failed to link Questrade account: {ex}");
        ModelState.AddModelError(string.Empty, "Invalid refresh token.");
        return BadRequest(ModelState);
      }
      string userId = m_userManager.GetUserId(HttpContext.User);
      ServiceLink link = m_serviceLinkManager.CreateLink(userId, model.IsPractice, token);
      return CreatedAtRoute("GetServiceLink", new { id = link.Id }, new ServiceLinkViewModel(link));
    }

    [HttpGet("[controller]/{id}", Name = "GetServiceLink")]
    public IActionResult GetServiceLink(int id)
    {
      string userId = m_userManager.GetUserId(HttpContext.User);
      ServiceLink link = m_serviceLinkManager.GetLink(userId, id);
      if (link == null)
      {
        return NotFound();
      }
      return Ok(new ServiceLinkViewModel(link));
    }

    [HttpGet("[controller]", Name = "ServiceLinks")]
    public IActionResult GetServiceLinks()
    {

      string userId = m_userManager.GetUserId(HttpContext.User);
      IEnumerable<ServiceLinkViewModel> ServiceLinks = m_serviceLinkManager.GetLinks(userId).Select(link => new ServiceLinkViewModel(link));
      return Ok(ServiceLinks);
    }
  }
}
