using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ParityUI.Models;
using ParityUI.Extensions;
using Microsoft.Extensions.Logging;

namespace ParityUI.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public sealed class AllocationsController : Controller
    {
        private readonly UserManager<AppUser> m_userManager;
        private readonly ILogger<AllocationsController> m_logger;


        public AllocationsController(UserManager<AppUser> userManager, ILogger<AllocationsController> logger)
        {
            m_userManager = userManager;
            m_logger = logger;
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromBody] AllocationsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach(var t in model.Allocations) {
                          m_logger.LogCritical($"{t.Key} : {t.Value}");
            }
            return Ok();
        }
    }
}
