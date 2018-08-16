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
            services.AddDbContext<AppDbContext>(opts => opts.UseInMemoryDatabase("default"));
            services.AddDefaultIdentity<AppUser>()
                    .AddEntityFrameworkStores<AppDbContext>();

            services.AddEntityFrameworkInMemoryDatabase().AddDbContext<AppDbContext>(opts => opts.UseInMemoryDatabase("db name"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
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

            app.Use(next => context => {
                string path = context.Request.Path.Value;
                if (
                    string.Equals(path, "/") ||
                    string.Equals(path, "/index.html", StringComparison.OrdinalIgnoreCase)) {
                    var token = antiforgery.GetAndStoreTokens(context).RequestToken;
                    context.Response.Cookies.Append("XSRF-TOKEN", token, new CookieOptions { HttpOnly = false });
                }

                return next(context);
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
