using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ParityUI.Models.View;
using ParityUI.Extensions;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using System.Collections.Generic;
using ParityUI.Models;
using ParityService.Questrade;
using ParityService.Questrade.Models;
using ParityService.Managers;

namespace ParityUI.Controllers
{
    [Authorize]
    public sealed class LinkedAccountsController : Controller
    {
        private readonly UserManager<AppUser> m_userManager;
        private readonly ILogger<LinkedAccountsController> m_logger;
        private readonly ISignInService m_signInService;
        private readonly CredentialsManager m_credentialsManager;

        public LinkedAccountsController(UserManager<AppUser> userManager, ISignInService signInService, CredentialsManager credentialsManager, ILogger<LinkedAccountsController> logger)
        {
            m_userManager = userManager;
            m_logger = logger;
            m_signInService = signInService;
            m_credentialsManager = credentialsManager;
        }

        [ValidateAntiForgeryToken]
        [HttpPost("[controller]/[action]", Name = "LinkAccount")]
        public async Task<IActionResult> Add([FromBody] QuestradeLinkViewModel model)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            AuthToken token;
            try {
                token = await m_signInService.SignIn(model.RefreshToken, model.IsPractice);
            } catch(Exception ex) {
                m_logger.LogWarning($"Failed to link Questrade account: {ex}");
                ModelState.AddModelError(string.Empty, "Invalid refresh token.");
                return BadRequest(ModelState);
            }
            string userId = m_userManager.GetUserId(HttpContext.User);
            LinkedAccount link = m_credentialsManager.CreateLink(userId, model.IsPractice, token);
            return CreatedAtRoute("GetLinkedAccount", new { id = link.Id }, new LinkedAccountViewModel(link));
        }

        [HttpGet("[controller]/{id}", Name = "GetLinkedAccount")]
        public IActionResult GetLinkedAccount(int id)
        {
            string userId = m_userManager.GetUserId(HttpContext.User);
            LinkedAccount link = m_credentialsManager.GetLink(userId, id);
            if (link == null) {
                return NotFound();
            }
            return Ok(new LinkedAccountViewModel(link));
        }

        [HttpGet("[controller]", Name = "LinkedAccounts")]
        public async Task<IActionResult> GetLinkedAccounts() {

            AppUser user = await m_userManager.GetUserAsync(HttpContext.User);
            IEnumerable<LinkedAccountViewModel> linkedAccounts = user.LinkedAccounts.Select(link => new LinkedAccountViewModel(link));
            return Ok(linkedAccounts);
        }
    }
}
