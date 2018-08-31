using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ParityUI.Models.View;
using ParityUI.Models;
using ParityUI.Extensions;
using Microsoft.Extensions.Logging;

namespace ParityUI.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public sealed class AccountController : Controller
    {
        private readonly UserManager<AppUser> m_userManager;
        private readonly SignInManager<AppUser> m_signInManager;
        private readonly IEmailSender m_emailSender;
        private readonly IUserClaimsPrincipalFactory<AppUser> m_principleFactory;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailSender emailSender, IUserClaimsPrincipalFactory<AppUser> principleFactory)
        {
            m_userManager = userManager;
            m_signInManager = signInManager;
            m_emailSender = emailSender;
            m_principleFactory = principleFactory;
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await m_signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await m_userManager.FindByEmailAsync(model.Email);
                HttpContext.User = await m_principleFactory.CreateAsync(user);
                return Ok();
            }
            if (result.IsLockedOut)
            {
                return Forbid();
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return BadRequest(ModelState);
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new AppUser(model.Email, model.Email);
            IdentityResult result = await m_userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                ModelState.AddErrors(result);
                return BadRequest(ModelState);
            }
            await m_signInManager.SignInAsync(user, isPersistent: false);
            HttpContext.User = await m_principleFactory.CreateAsync(user);

            return CreatedAtRoute("User", new AppUserViewModel(user));
        }

        [HttpGet(Name = "User")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> GetUser()
        {
            AppUser user = await m_userManager.GetUserAsync(HttpContext.User);
            return Ok(new AppUserViewModel(user));
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await m_signInManager.SignOutAsync();
            HttpContext.User = null;
            return Ok();
        }
    }
}
