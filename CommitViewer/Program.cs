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
        /// To test the commit viewer console app in isolation simply remove the try catch and call the CommitViewer app method directly
        /// </summary>
        /// <param name="args"></param>
        private static async Task Main(string[] args)
        {

            IServiceCollection services = new ServiceCollection();
            ConfigureLogger();
            ConfigureServices(services);
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            
            try
            {
                //Use GitHubService
                GitHubService gitHubService = serviceProvider.GetRequiredService<GitHubService>();
                await gitHubService.GetRepositoryCommits("tiago-martinho", "PIDESCO-Search-Component");
            }
            // Assuming the commit viewer flow is meant to be always used in case of error
            // If it's meant to be used only when GitHubAPI is unavailable catching a TimeoutException would be ideal
            catch (Exception e)
            {
                Log.Warning("A problem has occurred while trying to use the GitHub API.", e);
                Log.Warning("Using CommitViewer app process as a fallback...");
                CommitViewer.Start();
            }

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
