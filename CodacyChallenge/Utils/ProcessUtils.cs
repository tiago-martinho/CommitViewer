using CodacyChallenge.CommitProcessors;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CodacyChallenge
{
    public static class ProcessUtils
    {

        /// <summary>
        /// Validates and returns the git environment variable value if it's valid.
        /// The user must have git installed and an env variable with key = GIT in order to run the program
        /// </summary>
        public static string ValidateGitInstallation()
        {
            string gitEnvValue = Environment.GetEnvironmentVariable("GIT");
            if (string.IsNullOrWhiteSpace(gitEnvValue))
            {
                throw new InvalidOperationException("The GIT environment variable is not set. Please set the environment variable pointing to your git directory");
            }

            return gitEnvValue;
        }

        /// <summary>
        /// Starts the repo clone process. Waits until the process is finished. Returns the working directory
        /// </summary>
        public static string StartCloneProcess(string gitEnvPath, string workingDir, string githubUrl)
        {
            ProcessStartInfo cloneProcessInfo = GetCloneProcessInfo(gitEnvPath, workingDir, githubUrl);
            Process cloneProcess = new Process { StartInfo = cloneProcessInfo };
            cloneProcess.Start();
            cloneProcess.WaitForExit();
            return UpdateWorkingDirectory(githubUrl, workingDir);
        }


        /// <summary>
        /// Starts the repo log process. Waits until process is finished.
        /// Returns a commit enumerable
        /// </summary>
        public static IEnumerable<Commit> StartLogProcess(string gitEnvPath, string workingDir)
        {
            ProcessStartInfo logProcessInfo = GetLogProcessInfo(gitEnvPath, workingDir);
            Process logProcess = new Process { StartInfo = logProcessInfo };
            logProcess.Start();
            ICommitProcessor commitProcessor = new CommitProcessor();
            IEnumerable<Commit> commits = commitProcessor.ProcessCommitStream(logProcess.StandardOutput);
            logProcess.WaitForExit();
            return commits;
        }

        /// <summary>
        /// Updates the work directory after cloning the repo. Alternatively a process for "cd" could be made.
        /// Returns the updated working directory
        /// </summary>
        private static string UpdateWorkingDirectory(string githubUrl, string workingDir)
        {
            string[] gitHubUrlArray = githubUrl.Split('/');
            workingDir += gitHubUrlArray[gitHubUrlArray.Length - 1];
            return workingDir;
        }

        private static ProcessStartInfo GetCloneProcessInfo(string gitEnvPath, string workingDir, string githubUrl)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(gitEnvPath)
            {
                UseShellExecute = false,
                WorkingDirectory = workingDir,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                Arguments = @"clone " + githubUrl
            };

            return startInfo;
        }

        private static ProcessStartInfo GetLogProcessInfo(string gitEnvPath, string workingDir)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(gitEnvPath)
            {
                UseShellExecute = false,
                WorkingDirectory = workingDir,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                Arguments = "log"
            };

            return startInfo;
        }
    }
}
