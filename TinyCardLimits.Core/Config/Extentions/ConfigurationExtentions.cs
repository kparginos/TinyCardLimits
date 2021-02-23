using Microsoft.Extensions.Configuration;

namespace TinyCardLimits.Core.Config.Extentions
{
    public static class ConfigurationExtentions
    {
        public static AppConfig ReadAppConfiguration(
            this IConfiguration @this)
        {
            var connString = @this.GetSection("ConnectionStrings").GetSection("TinyBankDatabase").Value;
            var environment = @this.GetSection("Environment").Value;

            return new AppConfig()
            {
                ConnString = connString,
                Environment = environment
            };
        }
    }
}
