using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ParityUI.Models;
using ParityUI.Extensions;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using System.Collections.Generic;

namespace ParityUI.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public sealed class AccountLinksController : Controller
    {
        private readonly UserManager<AppUser> m_userManager;
        private readonly ILogger<AccountLinksController> m_logger;


        public AccountLinksController(UserManager<AppUser> userManager, ILogger<AccountLinksController> logger)
        {
            m_userManager = userManager;
            m_logger = logger;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromBody] RefreshTokenViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var margin = new AccountViewModel();
            margin.AccountNumber = "35055923";
            margin.AccountType = AccountType.Margin;
            margin.HasContributionLimit = false;

            var tfsa = new AccountViewModel();
            tfsa.AccountNumber = "63480923";
            tfsa.AccountType = AccountType.TFSA;
            tfsa.HasContributionLimit = true;

            var rrsp = new AccountViewModel();
            rrsp.AccountNumber = "43987234";
            rrsp.AccountType = AccountType.RRSP;
            rrsp.HasContributionLimit = true;

            var accountLink = new AccountLinkViewModel();
            accountLink.CreationTime = DateTime.UtcNow;
            accountLink.IsPractice = model.IsPractice;
            accountLink.IsAuthenticated = true;
            accountLink.Accounts = new List<AccountViewModel>() {
                margin,
                tfsa,
                rrsp
            };

            return CreatedAtRoute("AccountLink", accountLink);
        }

        [HttpGet(Name = "AccountLink")]
        public async Task<IActionResult> GetAccountLink()
        {
            var margin = new AccountViewModel();
            margin.AccountNumber = "35055923";
            margin.AccountType = AccountType.Margin;
            margin.HasContributionLimit = false;

            var tfsa = new AccountViewModel();
            tfsa.AccountNumber = "63480923";
            tfsa.AccountType = AccountType.TFSA;
            tfsa.HasContributionLimit = true;

            var rrsp = new AccountViewModel();
            rrsp.AccountNumber = "43987234";
            rrsp.AccountType = AccountType.RRSP;
            rrsp.HasContributionLimit = true;

            var accountLink = new AccountLinkViewModel();
            accountLink.CreationTime = DateTime.UtcNow;
            accountLink.IsPractice = true;
            accountLink.IsAuthenticated = true;
            accountLink.Accounts = new List<AccountViewModel>() {
                margin,
                tfsa,
                rrsp
            };
            return Ok(accountLink);
        }
    }
}
