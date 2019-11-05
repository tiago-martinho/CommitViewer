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
        private static string gitEnvPath;
        private static string workingDir;
        private static string gitHubUrl;

        static void Main(string[] args)
        {
            ValidateGitInstallation();
            ReadUserInput();
            StartCloneProcess();
            StartLogProcess();
        }


        /// <summary>
        /// The user must have git installed and and env variable with key = GIT in order to run the program
        /// The user could also provide his own env variable key in this case
        /// </summary>
        private static void ValidateGitInstallation()
        {
            gitEnvPath = Environment.GetEnvironmentVariable("GIT");

            if (string.IsNullOrWhiteSpace(gitEnvPath))
            {
                throw new InvalidOperationException("The GIT environment variable is not set. Please set the environment variable pointing to your git directory");
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


        /// <summary>
        /// Starts the repo clone process
        /// </summary>
        private static void StartCloneProcess()
        {
            ProcessStartInfo cloneProcessInfo = ProcessUtils.GetCloneProcessInfo(gitEnvPath, workingDir, gitHubUrl);
            Process cloneProcess = new Process { StartInfo = cloneProcessInfo };
            cloneProcess.Start();
            cloneProcess.WaitForExit();
            UpdateWorkingDirectory();
        }


        /// <summary>
        /// Starts the repo log process after having the working directory locally
        /// </summary>
        private static void StartLogProcess()
        {
            ProcessStartInfo logProcessInfo = ProcessUtils.GetLogProcessInfo(gitEnvPath, workingDir);
            Process logProcess = new Process { StartInfo = logProcessInfo };
            logProcess.Start();
            CommitProcessor.ProcessCommitStream(logProcess.StandardOutput);
            logProcess.WaitForExit();
        }

        /// <summary>
        /// Updates the work directory after cloning the repo. Alternatively a process for "cd" could be made
        /// </summary>
        private static void UpdateWorkingDirectory()
        {
            string[] gitHubUrlArray = gitHubUrl.Split('/');
            workingDir += gitHubUrlArray[gitHubUrlArray.Length - 1];
        }


    }

}
