using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParityUI.Data;
using Microsoft.EntityFrameworkCore;
using ParityUI.Models;
using Microsoft.AspNetCore.Antiforgery;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using ParityService.Questrade;

namespace ParityUI
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      string connectionString = Configuration.GetConnectionString("PostgresConnection");
      services.AddEntityFrameworkNpgsql()
              .AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));
      services.AddDefaultIdentity<AppUser>()
              .AddEntityFrameworkStores<AppDbContext>();

      services.AddQuestrade();

      services.AddMvc(options =>
      {
        options.Filters.Add(typeof(AntiforgeryCookieResultFilter));
      }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, IAntiforgery antiforgery)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
      }

      app.UseAuthentication();
      app.Use(next => context =>
          {
            if (!context.Request.Cookies.ContainsKey("XSRF-TOKEN"))
            {
              var tokens = antiforgery.GetAndStoreTokens(context);
              context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
                        new CookieOptions() { HttpOnly = false });
            }

            return next(context);
          });

      app.UseHttpsRedirection();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller}/{action=Index}/{id?}");
      });
    }
  }
}
