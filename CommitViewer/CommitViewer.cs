using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommitViewer.CommitProcessors;
using CommitViewer.Utils;
using Domain.Models;
using Domain.Utils;
using GitHubClient;
using Serilog;

namespace CommitViewer
{

    static class CommitViewer
    {
        private static string _workingDir;
        private static string _gitHubUrl;
        private static string _username;
        private static string _repoName;

        /// <summary>
        /// We only need to instantiate the GitLogProcessor once when we start the CommitViewer app
        /// However, it's not a static class so that we have can multiple implementations and change which we want to use easily
        /// </summary>
        private static readonly ICommitProcessor CommitProcessor = new GitLogProcessor();

        /// <summary>
        /// Starts the CommitViewer main process. Structured data is serialized and logged in both the console and a log file
        /// </summary>

        internal static async Task Start(GitHubService gitHubService)
        {
            IEnumerable<Commit> commits;
            try
            {
                ReadGitHubRequestFlowUserInput();
                Log.Information("Retrieving commits for user {0} and repository {1}", _username, _repoName);
                commits = await gitHubService.GetRepositoryCommits(_username, _repoName);
            }
            // Assuming the commit viewer flow is meant to be always used in case of error
            // If it's meant to be used only when GitHubAPI is unavailable catching a TimeoutException would be ideal
            catch (Exception e)
            {
                Log.Warning("A problem has occurred while trying to use the GitHub API.", e);
                Log.Warning("Using git process as a fallback...");
                commits = StartGitCommitLogProcess();
            }

            var commitList = commits.ToList(); // to avoid multiple enumeration
            Log.Information("{0} commits", commitList.Count);
            Log.Information("Retrieved git commits and parsed into respective data structure: {0}", commitList);

        }

        /// <summary>
        /// Fallback system process that returns a commit list structure
        /// </summary>
        private static IEnumerable<Commit> StartGitCommitLogProcess()
        {
            Git.Setup();
            ReadGitProcessUserInput();
            Log.Debug("Starting cloning process ...");
            _workingDir = ProcessUtils.StartCloneProcess(_workingDir, _gitHubUrl);
            Log.Debug("Finished cloning process. Retrieving commits....");
            IEnumerable<Commit> commits = ProcessUtils.StartLogProcess(_workingDir, CommitProcessor);
            DirectoryUtils.DeleteDirectory(_workingDir); //optional
            return commits;
        }

        /// <summary>
        /// Reads the necessary information for starting the GitProcess
        /// This reader could be improved with warning messages and directory/url validation
        /// </summary>
        private static void ReadGitProcessUserInput()
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
        /// Reads the necessary information for starting the StartGitHubRequestFlow
        /// This reader could be improved with warning messages and directory/url validation
        /// </summary>
        private static void ReadGitHubRequestFlowUserInput()
        {
            while (string.IsNullOrWhiteSpace(_username) || string.IsNullOrWhiteSpace(_repoName))
            {
                Console.WriteLine("Please provide the GitHub username...");
                _username = Console.ReadLine();
                Console.WriteLine("Please provide the repository for that user...");
                _repoName = Console.ReadLine();
            }
        }
    }
}
