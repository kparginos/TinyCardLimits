using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TinyCardLimits.Core.Services.Extentions;

namespace TinyCardLimits.Core.Test
{
    public class TinyCardLimitsFixture : IDisposable
    {
        public IServiceScope Scope { get; private set; }

        public TinyCardLimitsFixture()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath($"{AppDomain.CurrentDomain.BaseDirectory}")
                .AddJsonFile("appsettings.json", false)
                .Build();

            // Initialize Depedency Conttainer
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAppServices(config);

            Scope = serviceCollection
                .BuildServiceProvider()
                .CreateScope();
        }

        public void Dispose()
        {
            Scope.Dispose();
        }
    }
}
