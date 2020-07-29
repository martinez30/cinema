using Microsoft.Extensions.Configuration;
using System;

namespace Infra
{
    public static class Configuration
    {
        public static IConfigurationRoot _configuration;

        public static void Build(string pathjsonfile)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var build = new ConfigurationBuilder()
                .SetBasePath(pathjsonfile)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{environment}.json", true, true)
                .AddEnvironmentVariables();

            _configuration = build.Build();
        }

        public static string ConnectionString => _configuration.GetConnectionString("cineconnectionstring");
    }
}
