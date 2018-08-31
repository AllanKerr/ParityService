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
    public sealed class AccountsController : Controller
    {
        private readonly UserManager<User> m_userManager;
        private readonly ILogger<ServiceLinkController> m_logger;
        private readonly ISignInService m_signInService;
        private readonly AccountsManager m_accountsManager;

        public AccountsController(UserManager<User> userManager, ISignInService signInService, AccountsManager accountsManager, ILogger<ServiceLinkController> logger)
        {
            m_userManager = userManager;
            m_logger = logger;
            m_signInService = signInService;
            m_accountsManager = accountsManager;
        }

        [HttpGet("[controller]", Name = "GetAccounts")]
        public async Task<IActionResult> GetAccounts(int id)
        {
            string userId = m_userManager.GetUserId(HttpContext.User);
            IEnumerable<AccountViewModel> accounts = await m_accountsManager.GetAccounts(userId, id);
            if (accounts == null) {
                return NotFound();
            }
            return Ok(accounts);
        }
    }
}
