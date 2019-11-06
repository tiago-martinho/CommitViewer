using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace CodacyChallenge
{
    class Program
    {
        private static string workingDir;
        private static string gitHubUrl;

        static void Main(string[] args)
        {
            string gitEnvPath = ProcessUtils.ValidateGitInstallation();
            ReadUserInput();
            Console.WriteLine("*************** Starting cloning process ***************");
            workingDir = ProcessUtils.StartCloneProcess(gitEnvPath, workingDir, gitHubUrl);
            Console.WriteLine("*************** Finished cloning process. Starting log process ***************");
            IEnumerable<Commit> commits = ProcessUtils.StartLogProcess(gitEnvPath, workingDir);
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
