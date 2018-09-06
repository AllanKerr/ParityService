using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParityService.Managers;
using ParityService.Models.Entities;
using ParityService.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using QuestradeSymbol = ParityService.Questrade.Models.Entities.EquitySymbol;

namespace ParityService.Controllers
{
  [Authorize]
  public sealed class SymbolsController : Controller
  {
    private readonly UserManager<User> m_userManager;

    private readonly SymbolsManager m_symbolsManager;

    public SymbolsController(UserManager<User> userManager, SymbolsManager symbolsManager)
    {
      m_userManager = userManager;
      m_symbolsManager = symbolsManager;
    }

    //[ValidateAntiForgeryToken]
    [HttpGet("[controller]/search", Name = "FindSymbol")]
    public async Task<IActionResult> SearchForSymbol([FromQuery]string query)
    {
      IEnumerable<QuestradeSymbol> symbols;
      string userId = m_userManager.GetUserId(HttpContext.User);
      try
      {
        symbols = await m_symbolsManager.FindSymbol(userId, query);
      }
      catch (HttpRequestException)
      {
        ModelState.AddModelError(string.Empty, "An error occurred while trying to communicate with Questrade.");
        return BadRequest(ModelState);
      }
      catch (Exception)
      {
        ModelState.AddModelError(string.Empty, "This account does not have a linked Questrade account.");
        return BadRequest(ModelState);
      }
      return Ok(symbols.Select(symbol => new QuestradeSymbolViewModel(symbol)));
    }
  }
}
