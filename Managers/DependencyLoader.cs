using Microsoft.Extensions.DependencyInjection;
using System;

namespace ParityService.Managers
{
  public static class DependencyLoader
  {
    public static void AddManagers(this IServiceCollection services)
    {
      services.AddScoped<ServiceLinksManager>();
      services.AddScoped<CredentialsManager>();
      services.AddScoped<AccountsManager>();
      services.AddScoped<AllocationsManager>();
      services.AddScoped<SymbolsManager>();
    }
  }
}
