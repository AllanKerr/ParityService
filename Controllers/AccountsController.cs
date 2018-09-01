using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ParityService.Models.View;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using ParityService.Models.Entities;
using ParityService.Managers;
using System.Linq;

namespace ParityService.Controllers
{
  [Authorize]
  public sealed class AccountsController : Controller
  {
    private readonly UserManager<User> m_userManager;
    private readonly ILogger<ServiceLinksController> m_logger;
    private readonly AccountsManager m_accountsManager;

    public AccountsController(UserManager<User> userManager, AccountsManager accountsManager, ILogger<ServiceLinksController> logger)
    {
      m_userManager = userManager;
      m_logger = logger;
      m_accountsManager = accountsManager;
    }

    [HttpGet("[controller]/synchronize/{id}", Name = "SynchronizeAccounts")]
    public async Task<IActionResult> SynchronizeAccounts(int id)
    {
      string userId = m_userManager.GetUserId(HttpContext.User);
      IEnumerable<ManagedAccount> accounts = await m_accountsManager.SynchronizeAccounts(userId, id);
      if (accounts == null)
      {
        return NotFound();
      }
      IEnumerable<AccountViewModel> accountViews = accounts.Select(account => new AccountViewModel(account));
      return Ok(accountViews);
    }
  }
}
