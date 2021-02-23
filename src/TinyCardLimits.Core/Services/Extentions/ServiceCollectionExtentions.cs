using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TinyCardLimits.Core.Config;
using TinyCardLimits.Core.Config.Extentions;
using TinyCardLimits.Core.Data;
using TinyCardLimits.Core.Services.Interfaces;

namespace TinyCardLimits.Core.Services.Extentions
{
    public static class ServiceCollectionExtentions
    {
        public static void AddAppServices(
            this IServiceCollection @this, IConfiguration config)
        {
            @this.AddSingleton<AppConfig>(
                config.ReadAppConfiguration());

            @this.AddDbContext<TinyCardLimitsDBContext>(
                (serviceProvider, optionsBuilder) =>
                {
                    var appConfig = serviceProvider.GetRequiredService<AppConfig>();

                    optionsBuilder.UseSqlServer(appConfig.ConnString);
                });

            var appConfig = config.ReadAppConfiguration();
            if (appConfig.Environment == "Production")
            {
                @this.AddScoped<ICardService, CardService>();
                @this.AddScoped<ICardLimitService, CardLimitService>();
            }
            else
            {
                @this.AddScoped<ICardService, CardService>();
                @this.AddScoped<ICardLimitService, CardLimitService>();
            }

        }
    }
}
