
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ParityUI.Models.View;
using ParityUI.Models;
using ParityUI.Extensions;
using Microsoft.Extensions.Logging;
using ParityUI.Data;
using ParityService.Questrade.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using ParityService.Models.View;
using ParityService.Questrade;
using ParityService.Questrade.Models.Responses;
using System.Linq;

namespace ParityService.Managers
{
  public sealed class LinkedAccountsManager
  {
    private readonly AppDbContext m_context;
    private readonly ILogger<LinkedAccountsManager> m_logger;
    private readonly QuestradeClientFactory m_clientFactory;

    public LinkedAccountsManager(AppDbContext context, ILogger<LinkedAccountsManager> logger,  QuestradeClientFactory clientFactory)
    {
      m_context = context;
      m_logger = logger;
      m_clientFactory = clientFactory;
    }

    public LinkedAccount CreateLink(string userId, bool isPractice, AuthToken token)
    {

      LinkedAccount link = new LinkedAccount(userId, isPractice);

      using (IDbContextTransaction transaction = m_context.Database.BeginTransaction())
      {
        m_context.LinkedAccounts.Add(link);
        m_context.SaveChanges();

        Credentials credentials = new Credentials(link, token);
        m_context.Credentials.Add(credentials);
        m_context.SaveChanges();

        transaction.Commit();
      }
      return link;
    }

    public LinkedAccount GetLink(string userId, int id) {

        return m_context.LinkedAccounts.Find(id, userId);
    }

    public async Task<IEnumerable<AccountViewModel>> GetAccounts(string userId, int linkedAccountId) {

      LinkedAccount linkedAccount = GetLink(userId, linkedAccountId);
      if (linkedAccount == null) {
        return null;
      }
      QuestradeClient client = m_clientFactory.CreateClient(userId, linkedAccountId);
      AccountsResponse response = await client.FetchAccounts();
      return response.Accounts.Select(account => new AccountViewModel(account));
    }
  }
}
