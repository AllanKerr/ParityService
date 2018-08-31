using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ParityService.Models.View;
using ParityService.Extensions;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using System.Collections.Generic;
using ParityService.Models;
using ParityService.Questrade;
using ParityService.Questrade.Models;
using ParityService.Managers;

namespace ParityService.Controllers
{
    [Authorize]
    public sealed class LinkedAccountsController : Controller
    {
        private readonly UserManager<User> m_userManager;
        private readonly ILogger<LinkedAccountsController> m_logger;
        private readonly ISignInService m_signInService;
        private readonly LinkedAccountsManager m_linkedAccountsManager;

        public LinkedAccountsController(UserManager<User> userManager, ISignInService signInService, LinkedAccountsManager linkedAccountsManager, ILogger<LinkedAccountsController> logger)
        {
            m_userManager = userManager;
            m_logger = logger;
            m_signInService = signInService;
            m_linkedAccountsManager = linkedAccountsManager;
        }

        //[ValidateAntiForgeryToken]
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
            LinkedAccount link = m_linkedAccountsManager.CreateLink(userId, model.IsPractice, token);
            return CreatedAtRoute("GetLinkedAccount", new { id = link.Id }, new LinkedAccountViewModel(link));
        }

        [HttpGet("[controller]/{id}", Name = "GetLinkedAccount")]
        public IActionResult GetLinkedAccount(int id)
        {
            string userId = m_userManager.GetUserId(HttpContext.User);
            LinkedAccount link = m_linkedAccountsManager.GetLink(userId, id);
            if (link == null) {
                return NotFound();
            }
            return Ok(new LinkedAccountViewModel(link));
        }

        [HttpGet("[controller]", Name = "LinkedAccounts")]
        public IActionResult GetLinkedAccounts() {

            string userId = m_userManager.GetUserId(HttpContext.User);
            IEnumerable<LinkedAccountViewModel> linkedAccounts = m_linkedAccountsManager.GetLinks(userId).Select(link => new LinkedAccountViewModel(link));
            return Ok(linkedAccounts);
        }

        [HttpGet("[controller]/{id}/accounts", Name = "GetAccounts")]
        public async Task<IActionResult> GetAccounts(int id)
        {
            string userId = m_userManager.GetUserId(HttpContext.User);
            IEnumerable<AccountViewModel> accounts = await m_linkedAccountsManager.GetAccounts(userId, id);
            if (accounts == null) {
                return NotFound();
            }
            return Ok(accounts);
        }
    }
}
