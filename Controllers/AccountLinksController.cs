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
    [Route("[controller]/[action]")]
    public sealed class AccountLinksController : Controller
    {
        private readonly UserManager<AppUser> m_userManager;
        private readonly ILogger<AccountLinksController> m_logger;
        private readonly ISignInService m_signInService;
        private readonly CredentialsManager m_credentialsManager;

        public AccountLinksController(UserManager<AppUser> userManager, ISignInService signInService, CredentialsManager credentialsManager, ILogger<AccountLinksController> logger)
        {
            m_userManager = userManager;
            m_logger = logger;
            m_signInService = signInService;
            m_credentialsManager = credentialsManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
            AccountLink link = m_credentialsManager.CreateLink(userId, model.IsPractice, token);
            return CreatedAtRoute("AccountLink", new AccountLinkViewModel(link));
        }

        [HttpGet(Name = "AccountLink")]
        public async Task<IActionResult> GetAccountLink()
        {
            
            return Ok(null);
        }
    }
}
