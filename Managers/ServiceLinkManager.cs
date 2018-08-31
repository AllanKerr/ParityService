using ParityService.Models.Entities;
using ParityService.Data;
using QuestradeCredentials = ParityService.Questrade.Models.Entities.Credentials;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using ParityService.Questrade;
using System.Linq;

namespace ParityService.Managers
{
  public sealed class ServiceLinksManager
  {
    private readonly AppDbContext m_context;
    private readonly QuestradeClientFactory m_clientFactory;

    public ServiceLinksManager(AppDbContext context, QuestradeClientFactory clientFactory)
    {
      m_context = context;
      m_clientFactory = clientFactory;
    }

    public ServiceLink CreateLink(string userId, bool isPractice, QuestradeCredentials questradeCredentials)
    {
      ServiceLink link = new ServiceLink(userId, isPractice);

      using (IDbContextTransaction transaction = m_context.Database.BeginTransaction())
      {
        m_context.ServiceLinks.Add(link);
        m_context.SaveChanges();

        Credentials credentials = new Credentials(link, questradeCredentials);
        m_context.Credentials.Add(credentials);
        m_context.SaveChanges();

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
  }
}
