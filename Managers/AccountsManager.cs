using System.Threading.Tasks;
using ParityService.Models.View;
using ParityService.Models.Entities;
using System.Collections.Generic;
using ParityService.Questrade;
using ParityService.Questrade.Models.Responses;
using System.Linq;
using ParityService.Data;
using ParityService.Models.Enums;
using ParityService.Transformers;
using QuestradeAccount = ParityService.Questrade.Models.Entities.Account;

namespace ParityService.Managers
{
  public sealed class AccountsManager
  {
    private readonly AppDbContext m_context;
    private readonly QuestradeClientFactory m_clientFactory;

    public AccountsManager(AppDbContext context, QuestradeClientFactory clientFactory)
    {
      m_context = context;
      m_clientFactory = clientFactory;
    }

    public async Task<IEnumerable<AccountViewModel>> GetAccounts(string userId, int serviceLinkId)
    {
      ServiceLink serviceLink = m_context.ServiceLinks.Find(serviceLinkId, userId);
      if (serviceLink == null)
      {
        return null;
      }
      QuestradeClient client = m_clientFactory.CreateClient(userId, serviceLinkId);
      AccountsResponse response = await client.FetchAccounts();
      return response.Accounts.Select(account => new AccountViewModel(account));
    }

    public async Task<IEnumerable<ManagedAccount>> SynchronizeAccounts(string userId, int linkId)
    {
      ServiceLink link = m_context.ServiceLinks.Find(linkId, userId);
      if (link == null)
      {
        return null;
      }
      QuestradeClient client = m_clientFactory.CreateClient(userId, linkId);
      AccountsResponse response = await client.FetchAccounts();
      return SynchronizeAccounts(link, response.Accounts);
    }

    public IEnumerable<ManagedAccount> SynchronizeAccounts(ServiceLink link, IEnumerable<QuestradeAccount> questradeAccounts)
    {
      IEnumerable<ManagedAccount> accounts = questradeAccounts.Select(account =>
       {
         AccountType type = AccountTypeTransformer.Transform(account.Type);
         return new ManagedAccount(link, account.Number, type);
       });
      return SynchronizeAccounts(accounts);
    }

    public IEnumerable<ManagedAccount> SynchronizeAccounts(IEnumerable<ManagedAccount> accounts)
    {
      var allAccounts = new List<ManagedAccount>();
      foreach (ManagedAccount account in accounts)
      {
        ManagedAccount existingAccount = m_context.ManagedAccounts.Find(account.AccountId, account.ServiceLinkId, account.UserId);
        if (existingAccount == null)
        {
          existingAccount = account;
          m_context.Add(account);
        }
        allAccounts.Add(existingAccount);
      }
      m_context.SaveChanges();
      return allAccounts;
    }
  }
}
