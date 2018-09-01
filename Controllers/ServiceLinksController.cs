using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ParityService.Models.View;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Collections.Generic;
using ParityService.Models.Entities;
using ParityService.Managers;
using System.Security.Authentication;
using System.Net.Http;

namespace ParityService.Controllers
{
  [Authorize]
  public sealed class ServiceLinksController : Controller
  {
    private readonly UserManager<User> m_userManager;
    private readonly ILogger<ServiceLinksController> m_logger;
    private readonly ServiceLinksManager m_serviceLinksManager;

    public ServiceLinksController(UserManager<User> userManager, ServiceLinksManager serviceLinksManager, ILogger<ServiceLinksController> logger)
    {
      m_userManager = userManager;
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
      string userId = m_userManager.GetUserId(HttpContext.User);
      ServiceLink link;
      try
      {
        link = await m_serviceLinksManager.CreateLink(userId, model);
      }
      catch (InvalidCredentialException)
      {
        ModelState.AddModelError(string.Empty, "Invalid refresh token.");
        return BadRequest(ModelState);
      }
      catch (HttpRequestException)
      {
        ModelState.AddModelError(string.Empty, "An error occurred while trying to communicate with Questrade.");
        return BadRequest(ModelState);
      }
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
