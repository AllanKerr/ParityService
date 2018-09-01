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
using System.Net.Http;

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

    [HttpGet("[controller]/synchronize/{linkId}", Name = "SynchronizeAccounts")]
    public async Task<IActionResult> SynchronizeAccounts(int linkId)
    {
      string userId = m_userManager.GetUserId(HttpContext.User);
      IEnumerable<Account> accounts;
      try
      {
        accounts = await m_accountsManager.SynchronizeAccounts(userId, linkId);
      }
      catch (HttpRequestException)
      {
        ModelState.AddModelError(string.Empty, "An error occurred while trying to communicate with Questrade.");
        return BadRequest(ModelState);
      }
      if (accounts == null)
      {
        return NotFound();
      }
      IEnumerable<AccountViewModel> accountViews = accounts.Select(account => new AccountViewModel(account));
      return Ok(accountViews);
    }

    [HttpGet("[controller]/{linkId}/{accountId}", Name = "GetManagedAccount")]
    public IActionResult GetManagedAccount(int linkId, int accountId)
    {
      string userId = m_userManager.GetUserId(HttpContext.User);
      if (!m_accountsManager.TryGetManagedAccount(userId, linkId, accountId, out Account account))
      {
        return NotFound();
      }
      return Ok(new AccountViewModel(account));
    }

    [HttpGet("[controller]/{linkId}", Name = "GetManagedAccounts")]
    public IActionResult GetManagedAccounts(int linkId)
    {
      string userId = m_userManager.GetUserId(HttpContext.User);
      IEnumerable<Account> accounts = m_accountsManager.GetManagedAccounts(userId, linkId);
      if (accounts == null)
      {
        return NotFound();
      }
      IEnumerable<AccountViewModel> accountViews = accounts.Select(account => new AccountViewModel(account));
      return Ok(accountViews);
    }

    //[ValidateAntiForgeryToken]
    [HttpPost("[controller]/local", Name = "AddLocalAccount")]
    public IActionResult AddLocalAccount([FromBody] AccountViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      string userId = m_userManager.GetUserId(HttpContext.User);
      Account account = m_accountsManager.AddLocalAccount(userId, model.AccountName, model.AccountType);
      return CreatedAtRoute("GetLocalAccount", new { accountId = account.Id }, new AccountViewModel(account));
    }

    [HttpGet("[controller]/local/{accountId}", Name = "GetLocalAccount")]
    public IActionResult GetLocalAccount(int accountId)
    {
      string userId = m_userManager.GetUserId(HttpContext.User);
      if (!m_accountsManager.TryGetLocalAccount(userId, accountId, out Account account))
      {
        return NotFound();
      }
      return Ok(new AccountViewModel(account));
    }

    [HttpGet("[controller]/local", Name = "GetLocalAccounts")]
    public async Task<IActionResult> GetLocalAccounts()
    {
      User user = await m_userManager.GetUserAsync(HttpContext.User);
      IEnumerable<AccountViewModel> accountViews = user.LocalAccounts.Select(account => new AccountViewModel(account));
      return Ok(accountViews);
    }

    [HttpGet("[controller]", Name = "GetAccounts")]
    public IActionResult GetAccounts()
    {
      string userId = m_userManager.GetUserId(HttpContext.User);
      IEnumerable<AccountViewModel> accountViews = m_accountsManager.GetAccounts(userId).Select(account => new AccountViewModel(account));
      return Ok(accountViews);
    }
  }
}
