using System.Threading.Tasks;
using ParityService.Models.View;
using ParityService.Models.Entities;
using System.Collections.Generic;
using ParityService.Questrade;
using ParityService.Questrade.Models.Responses;
using System.Linq;

namespace ParityService.Managers
{
  public sealed class AccountsManager
  {
    private readonly ServiceLinkManager m_serviceLinkManager;
    private readonly QuestradeClientFactory m_clientFactory;

    public AccountsManager(ServiceLinkManager serviceLinkManager, QuestradeClientFactory clientFactory)
    {
      m_serviceLinkManager = serviceLinkManager;
      m_clientFactory = clientFactory;
    }

    public async Task<IEnumerable<AccountViewModel>> GetAccounts(string userId, int serviceLinkId)
    {
      ServiceLink ServiceLink = m_serviceLinkManager.GetLink(userId, serviceLinkId);
      if (ServiceLink == null)
      {
        return null;
      }
      QuestradeClient client = m_clientFactory.CreateClient(userId, serviceLinkId);
      AccountsResponse response = await client.FetchAccounts();
      return response.Accounts.Select(account => new AccountViewModel(account));
    }
  }
}
