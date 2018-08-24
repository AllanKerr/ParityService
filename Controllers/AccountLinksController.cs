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
            return Ok();
        }
    }
}
