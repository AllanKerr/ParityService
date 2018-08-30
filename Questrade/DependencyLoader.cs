using Microsoft.Extensions.DependencyInjection;
using System;

namespace ParityService.Questrade
{
    public static class DependencyLoader
    {
        public static void AddQuestrade(this IServiceCollection services)
        {
            services.AddHttpClient<IPracticeSignInClient ,SignInClient>(client => {
                client.BaseAddress = new Uri("https://practicelogin.questrade.com/");
               });

            services.AddHttpClient<ILiveSignInClient, SignInClient>(client => {
                client.BaseAddress = new Uri("https://login.questrade.com/");
            });

            services.AddScoped<QuestradeClientFactory>();
            services.AddSingleton<ISignInService, SignInService>();
        }
    }
}
