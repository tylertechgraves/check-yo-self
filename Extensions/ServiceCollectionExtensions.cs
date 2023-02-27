using System;
using System.Net.Http;
using check_yo_self_api_client.Configuration;
using check_yo_self_api_client.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace check_yo_self.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEmployeesClient(this IServiceCollection services)
    {
        return services.AddSingleton<IEmployeesClient, EmployeesClient>(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
            var loggerFactory = provider.GetRequiredService<ILoggerFactory>();

            var baseUrl = configuration.GetValue<string>("CheckYoSelf:EmployeesApiBaseUrl");

            if (string.IsNullOrEmpty(baseUrl))
                throw new Exception("Unable to bootstrap API client; baseurl configuration is missing");

            var sdkConfig = new SdkConfiguration
            {
                BaseUrl = baseUrl,
                HttpClientFactory = httpClientFactory,
                LoggerFactory = loggerFactory,
            };

            var client = new EmployeesClient(sdkConfig);

            return client;
        });
    }
}
