using System;
using System.IO;
using Serilog;

namespace CommitViewer
{
    class Program
    {
        private static string workingDir;
        private static string gitHubUrl;

        /// <summary>
        /// Main method that calls the GitHub API first and uses the initial implemented system as a fallback
        /// To test the commit viewer console app in isolation simply remove the try catch and call the CommitViewer app method directly
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .CreateLogger();

            ReadUserInput();
            try
            {
              
                //GitHub API call
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
                CommitViewer.Start(workingDir, gitHubUrl);
            }
            
        }

        /// <summary>
        /// Reads the necessary information for processing the commit list
        /// </summary>
        private static void ReadUserInput()
        {
            while (string.IsNullOrWhiteSpace(workingDir) || string.IsNullOrWhiteSpace(gitHubUrl))
            {
                Console.WriteLine("Please provide the GitHub url that you want to process...");
                gitHubUrl = Console.ReadLine();
                Console.WriteLine("Please provide your working directory");
                workingDir = Console.ReadLine();
            }
        }
    }

}
