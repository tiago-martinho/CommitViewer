using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommitViewer.Models;
using CommitViewer.Utils;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CommitViewer
{
    static class CommitViewer
    {

        internal static void Start(string workingDir, string githubUrl)
        {
            Git.Setup();
            Log.Information("Starting cloning process ...");
            workingDir = ProcessUtils.StartCloneProcess(workingDir, githubUrl);
            Log.Information("Finished cloning process. Retrieving commits....");
            IEnumerable<Commit> commits = ProcessUtils.StartLogProcess(workingDir);
            DirectoryUtils.DeleteDirectory(workingDir); //optional
            Log.Information("Retrieved git commits. Returning the list of commits:");
            Log.Information(commits.ToList().ToString());
        }

    
    }
}
