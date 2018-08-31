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
using ParityService.Models.View;
using ParityUI.Data;

namespace ParityUI.Controllers
{
    [Authorize]
    public sealed class EarningsController : Controller
    {
        private readonly UserManager<AppUser> m_userManager;
        private readonly ILogger<LinkedAccountsController> m_logger;
        private readonly ISignInService m_signInService;
        private readonly AppDbContext m_context;

        public EarningsController(UserManager<AppUser> userManager, ISignInService signInService, AppDbContext context, ILogger<LinkedAccountsController> logger)
        {
            m_userManager = userManager;
            m_logger = logger;
            m_signInService = signInService;
            m_context = context;
        }

        [ValidateAntiForgeryToken]
        [HttpPut("[controller]", Name = "SetEarnings")]
        public IActionResult Set([FromBody] EarningsViewModel model)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            string userId = m_userManager.GetUserId(HttpContext.User);
            Earnings earnings = m_context.Earnings.Find(userId);
            bool isNew = earnings == null;

            if (isNew) {
              earnings = new Earnings(userId);
              m_context.Earnings.Add(earnings);
            } 
            earnings.Update(model.AnnualIncome, earnings.Region);
            m_context.SaveChanges();

            if (isNew) {
              return CreatedAtRoute("GetEarnings", new EarningsViewModel(earnings));
            }
            return Ok();
        }

        [HttpGet("[controller]", Name = "GetEarnings")]
        public IActionResult GetEarnings()
        {
            string userId = m_userManager.GetUserId(HttpContext.User);
            Earnings earnings = m_context.Earnings.Find(userId);
            if (earnings == null) {
              return NotFound();
            }
            return Ok(new EarningsViewModel(earnings));
        }
    }
}
