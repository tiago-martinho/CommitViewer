using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommitViewer.Models;
using CommitViewer.Utils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;

namespace CommitViewer
{

    /// Open the log file and copy json contents to a json viewer to visualize the output better: https://jsonformatter.org/json-viewer
    static class CommitViewer
    {
        /// <summary>
        /// Starts the CommitViewer main process. Structured data is serialized and logged in both the console and a log file
        /// </summary>
        /// <param name="workingDir"></param>
        /// <param name="githubUrl"></param>
        internal static void Start(string workingDir, string githubUrl)
        {
            Git.Setup();
            Log.Information("Starting cloning process ...");
            workingDir = ProcessUtils.StartCloneProcess(workingDir, githubUrl);
            Log.Information("Finished cloning process. Retrieving commits....");
            IEnumerable<Commit> commits = ProcessUtils.StartLogProcess(workingDir);
            DirectoryUtils.DeleteDirectory(workingDir); //optional
            Log.Information("Retrieved git commits. Returning the list of commits:");
            Log.Information(JsonConvert.SerializeObject(commits));
        }

    
    }
}
