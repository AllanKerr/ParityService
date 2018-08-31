
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ParityService.Models.View;
using ParityService.Models;
using ParityService.Extensions;
using Microsoft.Extensions.Logging;
using ParityService.Data;
using ParityService.Questrade.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using ParityService.Questrade;
using ParityService.Questrade.Models.Responses;
using System.Linq;

namespace ParityService.Managers
{
  public sealed class ServiceLinkManager
  {
    private readonly AppDbContext m_context;
    private readonly QuestradeClientFactory m_clientFactory;

    public ServiceLinkManager(AppDbContext context, QuestradeClientFactory clientFactory)
    {
      m_context = context;
      m_clientFactory = clientFactory;
    }

    public ServiceLink CreateLink(string userId, bool isPractice, AuthToken token)
    {

      ServiceLink link = new ServiceLink(userId, isPractice);

      using (IDbContextTransaction transaction = m_context.Database.BeginTransaction())
      {
        m_context.ServiceLinks.Add(link);
        m_context.SaveChanges();

        Credentials credentials = new Credentials(link, token);
        m_context.Credentials.Add(credentials);
        m_context.SaveChanges();

        transaction.Commit();
      }
      return link;
    }

    public ServiceLink GetLink(string userId, int id) {

        return m_context.ServiceLinks.Find(id, userId);
    }

    public IEnumerable<ServiceLink> GetLinks(string userId) {
      return m_context.ServiceLinks.Where(ServiceLink => ServiceLink.UserId == userId);
    }
  }
}
