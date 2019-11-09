using System;
using System.IO;
using System.Net.Http;
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
        private static string _workingDir;
        private static string _gitHubUrl;

        /// <summary>
        /// Main method that calls the GitHub API first and uses the initial implemented system as a fallback
        /// To test the commit viewer console app in isolation simply remove the try catch and call the CommitViewer app method directly
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            ConfigureLogger();
            ConfigureServices();
            ReadUserInput();

            try
            {
                //Use GitHubClient
                throw new IOException();
            }

            /* No details were given regarding the type of failure that should be caught (besides a network timeout)
            My reasoning here is that the flow previously implemented should only be used in case where the GitHub API is unavailable
            If it's available but there's an exception or error (invalid arguments, etc) there isn't much of a reason for using the console
            app as it will most likely just return the same error. If the point is to always use the console app as a fallback just remove
            the specific exceptions and simply catch the general one */
            catch (Exception e) when (e is TimeoutException || e is IOException)
            {
                Log.Error("An error has occurred while trying to use the GitHub API.", e);
                Log.Warning("Using CommitViewer app process as a fallback...");
                CommitViewer.Start(_workingDir, _gitHubUrl);
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

        private static void ConfigureServices()
        {

            var services = new ServiceCollection();

            // A circuit breaker pattern could be added in distributed environments
            services.AddHttpClient<GitHubClient>()
                .AddPolicyHandler(GetRetryPolicy());
        }

        /// <summary>
        /// Reads the necessary information for processing the commit list
        /// </summary>
        private static void ReadUserInput()
        {
            while (string.IsNullOrWhiteSpace(_workingDir) || string.IsNullOrWhiteSpace(_gitHubUrl))
            {
                Console.WriteLine("Please provide the GitHub url that you want to process...");
                _gitHubUrl = Console.ReadLine();
                Console.WriteLine("Please provide your working directory");
                _workingDir = Console.ReadLine();
            }
        }

        /// <summary>
        /// Retry policy that defines how many retries and how long between each retry will the HttpClient do for each unsuccessful http request
        /// </summary>
        /// <returns></returns>
        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions.HandleTransientHttpError().WaitAndRetryAsync(3,
                retryWaitTime => TimeSpan.FromSeconds(Math.Pow(2, retryWaitTime)));
        }
    }

}
