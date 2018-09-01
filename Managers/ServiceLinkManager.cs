using ParityService.Models.Entities;
using ParityService.Data;
using QuestradeCredentials = ParityService.Questrade.Models.Entities.Credentials;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using ParityService.Questrade;
using System.Linq;
using System.Threading.Tasks;
using ParityService.Questrade.Models.Responses;
using ParityService.Questrade.Authentication;
using System;
using Microsoft.Extensions.Logging;
using System.Security.Authentication;
using System.Net.Http;
using ParityService.Transformers;
using ParityService.Models.Enums;

namespace ParityService.Managers
{
  public sealed class ServiceLinksManager
  {
    private readonly AppDbContext m_context;
    private readonly QuestradeClientFactory m_clientFactory;
    private readonly ISignInService m_signInService;
    private readonly ILogger<ServiceLinksManager> m_logger;
    private readonly AccountsManager m_accountsManager;

    public ServiceLinksManager(AppDbContext context, QuestradeClientFactory clientFactory, ISignInService signInService, ILogger<ServiceLinksManager> logger, AccountsManager accountsManager)
    {
      m_context = context;
      m_clientFactory = clientFactory;
      m_signInService = signInService;
      m_logger = logger;
      m_accountsManager = accountsManager;
    }

    public async Task<ServiceLink> CreateLink(string userId, IQuestradeLink questradeLink)
    {
      QuestradeCredentials questradeCredentials = await GetLinkCredentials(questradeLink);
      AccountsResponse response = await GetAccounts(questradeCredentials);

      ServiceLink link = new ServiceLink(userId, questradeLink.IsPractice);
      using (IDbContextTransaction transaction = m_context.Database.BeginTransaction())
      {
        m_context.ServiceLinks.Add(link);
        m_context.SaveChanges();

        Credentials credentials = new Credentials(link, questradeCredentials);
        m_context.Credentials.Add(credentials);
        m_context.SaveChanges();

        IEnumerable<ManagedAccount> accounts = response.Accounts.Select(account =>
        {
          AccountType type = AccountTypeTransformer.Transform(account.Type);
          return new ManagedAccount(link, account.Number, type);
        });
        m_accountsManager.SynchronizeAccounts(accounts);

        transaction.Commit();
      }
      return link;
    }

    public ServiceLink GetLink(string userId, int id)
    {
      return m_context.ServiceLinks.Find(id, userId);
    }

    public IEnumerable<ServiceLink> GetLinks(string userId)
    {
      return m_context.ServiceLinks.Where(ServiceLink => ServiceLink.UserId == userId);
    }

    public async Task<QuestradeCredentials> GetLinkCredentials(IQuestradeLink link)
    {
      try
      {
        return await m_signInService.SignIn(link.RefreshToken, link.IsPractice);
      }
      catch (Exception ex)
      {
        m_logger.LogWarning($"Failed to link Questrade account: {ex}");
        throw new InvalidCredentialException("Failed to link Questrade account.", ex);
      }
    }

    private async Task<AccountsResponse> GetAccounts(QuestradeCredentials credentials)
    {
      QuestradeClient client = m_clientFactory.CreateClient(credentials);
      try
      {
        return await client.FetchAccounts();
      }
      catch (Exception ex)
      {
        m_logger.LogWarning($"Failed to fetch service accounts: {ex}");
        throw new HttpRequestException("Failed to fetch service accounts.", ex);
      }
    }
  }
}
