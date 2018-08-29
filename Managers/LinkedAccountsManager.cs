
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

namespace ParityService.Managers
{
  public sealed class LinkedAccountsManager
  {
    private readonly AppDbContext m_context;

    private readonly ILogger<LinkedAccountsManager> m_logger;

    public LinkedAccountsManager(AppDbContext context, ILogger<LinkedAccountsManager> logger)
    {
      m_context = context;
      m_logger = logger;
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
  }
}
