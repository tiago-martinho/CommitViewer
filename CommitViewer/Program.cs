using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Models;
using GitHubClient;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.SystemConsole.Themes;

namespace CommitViewer
{
    internal static class Program
    {

        /// <summary>
        /// Main method that calls the GitHub API first and uses the initial implemented system as a fallback
        /// </summary>
        /// <param name="args"></param>
        private static async Task Main(string[] args)
        {

            IServiceCollection services = new ServiceCollection();
            ConfigureLogger();
            ConfigureServices(services);
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            await CommitViewer.Start(serviceProvider.GetRequiredService<GitHubService>());
        }

        private static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Minute)
                .WriteTo.Console()
                .CreateLogger();

        }

        private static void ConfigureServices(IServiceCollection services)
        {

            // A circuit breaker pattern could be added in distributed environments
            services.AddHttpClient<GitHubService>()
                .AddPolicyHandler(GetRetryPolicy());
        }


        /// <summary>
        /// Retry policy that defines how many retries and how long between each retry will the HttpClient do for each unsuccessful http request
        /// </summary>
        /// <returns></returns>
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions.HandleTransientHttpError().WaitAndRetryAsync(3,
                retryWaitTime => TimeSpan.FromSeconds(Math.Pow(2, retryWaitTime)));
        }
    }

}
