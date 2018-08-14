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
    public sealed class AccountController : Controller
    {
        private readonly UserManager<AppUser> m_userManager;
        private readonly SignInManager<AppUser> m_signInManager;
        private readonly IEmailSender m_emailSender;
        private readonly ILogger<AccountController> m_logger;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailSender emailSender, ILogger<AccountController> logger) {
            m_userManager = userManager;
            m_signInManager = signInManager;
            m_emailSender = emailSender;
            m_logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model) {
            if (!ModelState.IsValid) {
                foreach (var state in ModelState.Values) {
                    foreach(var error in state.Errors) {
                        m_logger.LogCritical($"Error Model state: {error.ErrorMessage}");
                    }
                }

                return BadRequest(ModelState);
            }
            m_logger.LogCritical($"email: {model.Email} password: {model.Password} rememberMe: {model.RememberMe}");

            var result = await m_signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded) {
                return Ok();
            }
            if (result.IsLockedOut) {
                return Forbid();
            }
            m_logger.LogCritical($"result: {result}");

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return BadRequest(ModelState);
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }                 
            var user = new AppUser {
                UserName = model.Email,
                Email = model.Email,
            };
            IdentityResult result = await m_userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) {               
                ModelState.AddErrors(result);
                return BadRequest(ModelState);
            }
            await m_signInManager.SignInAsync(user, isPersistent: false);
            return Ok();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout() {
            await m_signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IActionResult> ConfirmEmail(string userId, string code) {
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
            return Ok();  
        }
    }
}
