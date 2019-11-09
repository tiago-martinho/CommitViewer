using System;
using System.Collections.Generic;
using System.Linq;
using CommitViewer.CommitProcessors;
using CommitViewer.Utils;
using Domain.Models;
using Domain.Utils;
using Newtonsoft.Json;
using Serilog;

namespace CommitViewer
{

    /// Open the log file and copy json contents to a json viewer to visualize the output better: https://jsonformatter.org/json-viewer
    static class CommitViewer
    {
        private static string _workingDir;
        private static string _gitHubUrl;

        /// <summary>
        /// We only need to instantiate the GitLogProcessor once when we start the CommitViewer app
        /// However, it's not a static class so that we have can multiple implementations and change which we want to use easily
        /// </summary>
        private static readonly ICommitProcessor CommitProcessor = new GitLogProcessor();

        /// <summary>
        /// Starts the CommitViewer main process. Structured data is serialized and logged in both the console and a log file
        /// </summary>

        internal static void Start()
        {
            Git.Setup();
            ReadUserInput();
            Log.Debug("Starting cloning process ...");
            _workingDir = ProcessUtils.StartCloneProcess(_workingDir, _gitHubUrl);
            Log.Debug("Finished cloning process. Retrieving commits....");
            IEnumerable<Commit> commits = ProcessUtils.StartLogProcess(_workingDir, CommitProcessor);
            DirectoryUtils.DeleteDirectory(_workingDir); //optional
            var commitList = commits.ToList(); // just to avoid multiple enumeration
            Log.Information("Retrieved git commits and parsed into respective data structure: {0}", commitList);
            Log.Information("{0} commits", commitList.Count);
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

    }
}
