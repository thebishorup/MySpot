using Microsoft.Extensions.Configuration;

using MySpot.Infrastructure;

namespace MySpot.Tests.Integration
{
    public sealed class OptionsProvider
    {
        private readonly IConfigurationRoot _config;

        public OptionsProvider()
        {
            _config = GetConfigurationRoot();
        }

        public T Get<T>(string sectionName) where T : class, new()
            => _config.GetOptions<T>(sectionName);

        private static IConfigurationRoot GetConfigurationRoot()
            => new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json", true)
            .AddEnvironmentVariables()
            .Build();
    }
}
