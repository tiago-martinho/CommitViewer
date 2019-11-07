using System;
using System.Collections.Generic;
using System.Linq;
using CommitViewer.Models;
using CommitViewer.Utils;

namespace CommitViewer
{
    class Program
    {
        private static string workingDir;
        private static string gitHubUrl;

        static void Main(string[] args)
        {
            Git.Setup();
            ReadUserInput();
            Console.WriteLine("*************** Starting cloning process ***************");
            workingDir = ProcessUtils.StartCloneProcess(workingDir, gitHubUrl);
            Console.WriteLine("*************** Finished cloning process. Starting log process ***************");
            IEnumerable<Commit> commits = ProcessUtils.StartLogProcess(workingDir);
            Console.WriteLine("*************** Deleting cloned repo locally ***************");
            DirectoryUtils.DeleteDirectory(workingDir);
            Console.WriteLine("*************** Retrieved git commits. Returning the list of commits: ***************");
            Console.WriteLine(commits.ToList());

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
