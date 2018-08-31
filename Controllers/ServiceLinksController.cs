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
using QuestradeCredentials = ParityService.Questrade.Models.Entities.Credentials;
using ParityService.Managers;
using ParityService.Questrade.Authentication;

namespace ParityService.Controllers
{
  [Authorize]
  public sealed class ServiceLinksController : Controller
  {
    private readonly UserManager<User> m_userManager;
    private readonly ILogger<ServiceLinksController> m_logger;
    private readonly ServiceLinksManager m_serviceLinksManager;
    private readonly ISignInService m_signInService;

    public ServiceLinksController(UserManager<User> userManager, ISignInService signInService, ServiceLinksManager serviceLinksManager, ILogger<ServiceLinksController> logger)
    {
      m_userManager = userManager;
      m_logger = logger;
      m_signInService = signInService;
      m_serviceLinksManager = serviceLinksManager;
    }

    //[ValidateAntiForgeryToken]
    [HttpPost("[controller]/[action]", Name = "LinkService")]
    public async Task<IActionResult> Add([FromBody] QuestradeLinkViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      QuestradeCredentials questradeCredentials;
      try
      {
        questradeCredentials = await m_signInService.SignIn(model.RefreshToken, model.IsPractice);
      }
      catch (Exception ex)
      {
        m_logger.LogWarning($"Failed to link Questrade account: {ex}");
        ModelState.AddModelError(string.Empty, "Invalid refresh token.");
        return BadRequest(ModelState);
      }
      string userId = m_userManager.GetUserId(HttpContext.User);
      ServiceLink link = m_serviceLinksManager.CreateLink(userId, model.IsPractice, questradeCredentials);
      return CreatedAtRoute("GetServiceLink", new { id = link.Id }, new ServiceLinkViewModel(link));
    }

    [HttpGet("[controller]/{id}", Name = "GetServiceLink")]
    public IActionResult GetServiceLink(int id)
    {
      string userId = m_userManager.GetUserId(HttpContext.User);
      ServiceLink link = m_serviceLinksManager.GetLink(userId, id);
      if (link == null)
      {
        return NotFound();
      }
      return Ok(new ServiceLinkViewModel(link));
    }

    [HttpGet("[controller]", Name = "GetServiceLinks")]
    public IActionResult GetServiceLinks()
    {

      string userId = m_userManager.GetUserId(HttpContext.User);
      IEnumerable<ServiceLinkViewModel> ServiceLinks = m_serviceLinksManager.GetLinks(userId).Select(link => new ServiceLinkViewModel(link));
      return Ok(ServiceLinks);
    }
  }
}
