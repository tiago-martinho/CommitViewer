using System.Collections.Generic;
using System.Linq;
using CommitViewer.CommitProcessors;
using CommitViewer.Models;
using CommitViewer.Utils;
using Newtonsoft.Json;
using Serilog;

namespace CommitViewer
{

    /// Open the log file and copy json contents to a json viewer to visualize the output better: https://jsonformatter.org/json-viewer
    static class CommitViewer
    {
        /// <summary>
        /// We only need to instantiate the GitLogProcessor once when we start the CommitViewer app
        /// However, it's not a static class so that we have can multiple implementations and change which we want to use easily
        /// </summary>
        private static readonly ICommitProcessor CommitProcessor = new GitLogProcessor();

        /// <summary>
        /// Starts the CommitViewer main process. Structured data is serialized and logged in both the console and a log file
        /// </summary>
        /// <param name="workingDir"></param>
        /// <param name="githubUrl"></param>
        internal static void Start(string workingDir, string githubUrl)
        {
            Git.Setup();
            Log.Debug("Starting cloning process ...");
            workingDir = ProcessUtils.StartCloneProcess(workingDir, githubUrl);
            Log.Debug("Finished cloning process. Retrieving commits....");
            IEnumerable<Commit> commits = ProcessUtils.StartLogProcess(workingDir, CommitProcessor);
            DirectoryUtils.DeleteDirectory(workingDir); //optional
            var commitList = commits.ToList(); // just to avoid multiple enumeration
            Log.Information("Retrieved git commits and parsed into respective data structure: {0}", commitList);
            Log.Information("{0} commits", commitList.Count);
        }

    
    }
}
