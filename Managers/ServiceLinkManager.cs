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
using ParityService.Extensions;

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
      QuestradeClient client = m_clientFactory.CreateClient(questradeCredentials);
      AccountsResponse response = await client.FetchAccounts();

      ServiceType serviceType = ServiceType.Questrade;
      string serviceId = response.UserId.ToString();
      if (LinkExists(userId, serviceType, serviceId))
      {
        throw new ConflictException();
      }
      ServiceLink link = new ServiceLink(userId, ServiceType.Questrade, serviceId, questradeLink.IsPractice);
      using (IDbContextTransaction transaction = m_context.Database.BeginTransaction())
      {
        m_context.ServiceLinks.Add(link);
        m_context.SaveChanges();

        Credentials credentials = new Credentials(link, questradeCredentials);
        m_context.Credentials.Add(credentials);
        m_context.SaveChanges();

        m_accountsManager.SynchronizeAccounts(link, response.Accounts);

        transaction.Commit();
      }
      return link;
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

    public ServiceLink GetLink(string userId, int id)
    {
      return m_context.ServiceLinks.Find(id, userId);
    }

    public IEnumerable<ServiceLink> GetLinks(string userId)
    {
      return m_context.ServiceLinks.Where(ServiceLink => ServiceLink.UserId == userId);
    }

    public bool LinkExists(string userId, ServiceType serviceType, string serviceId)
    {
      return m_context.ServiceLinks.Any(link => link.UserId == userId && link.ServiceType == serviceType && link.ServiceId == serviceId);
    }
  }
}
