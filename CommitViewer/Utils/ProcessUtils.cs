using System;
using System.Collections.Generic;
using System.Diagnostics;
using CommitViewer.CommitProcessors;
using CommitViewer.Models;

namespace CommitViewer.Utils
{
    public static class ProcessUtils
    {

        /// <summary>
        /// Starts the repo clone process. Waits until the process is finished. Returns the working directory
        /// </summary>
        public static string StartCloneProcess(string workingDir, string githubUrl)
        {
            ProcessStartInfo cloneProcessInfo = GetCloneProcessInfo(workingDir, githubUrl);
            Process cloneProcess = new Process { StartInfo = cloneProcessInfo };
            cloneProcess.Start();
            cloneProcess.WaitForExit();
            return UpdateWorkingDirectory(githubUrl, workingDir);
        }

        public static int StartCommitCountProcess(string workingDir)
        {
            ProcessStartInfo cloneProcessInfo = GetCommitCountProcessInfo(workingDir);
            Process countProcess = new Process { StartInfo = cloneProcessInfo };
            countProcess.Start();
            countProcess.WaitForExit();
            string countResult = countProcess.StandardOutput.ReadLine();
            return int.Parse(countResult ?? throw new InvalidOperationException());
        }

        /// <summary>
        /// Starts the repo log process and processes the logs using a given commit processor. Waits until process is finished.
        /// Returns a commit enumerable
        /// </summary>
        public static IEnumerable<Commit> StartLogProcess(string workingDir, ICommitProcessor commitProcessor)
        {
            ProcessStartInfo logProcessInfo = GetLogProcessInfo(workingDir, commitProcessor.GetProcessorArguments());
            Process logProcess = new Process { StartInfo = logProcessInfo };
            logProcess.Start();
            IEnumerable<Commit>commits = commitProcessor.ProcessCommitStream(logProcess.StandardOutput);
            logProcess.WaitForExit();
            return commits;
        }

        /// <summary>
        /// Starts the repo log process. Waits until process is finished.
        /// Returns a commit enumerable
        /// </summary>
        public static IEnumerable<Commit> StartPagedLogProcess(string workingDir, ICommitProcessor commitProcessor, int maxCount, int skipNumber)
        {
            ProcessStartInfo logProcessInfo = GetPagedLogProcessInfo(workingDir, commitProcessor.GetProcessorArguments(), maxCount, skipNumber);
            Process logProcess = new Process { StartInfo = logProcessInfo };
            logProcess.Start();
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

        private static ProcessStartInfo GetCloneProcessInfo(string workingDir, string githubUrl)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(Git.GitPath)
            {
                UseShellExecute = false,
                WorkingDirectory = workingDir,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                Arguments = "clone " + githubUrl
            };

            return startInfo;
        }

        private static ProcessStartInfo GetLogProcessInfo(string workingDir, string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(Git.GitPath)
            {
                UseShellExecute = false,
                WorkingDirectory = workingDir,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                Arguments = arguments + " HEAD"
            };

            return startInfo;
        }

        private static ProcessStartInfo GetPagedLogProcessInfo(string workingDir, string arguments, int maxCount, int skipNumber)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(Git.GitPath)
            {
                UseShellExecute = false,
                WorkingDirectory = workingDir,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                Arguments = arguments + " --max-count=" + maxCount + " --skip=" + skipNumber + " HEAD"
            };

            return startInfo;
        }

        private static ProcessStartInfo GetCommitCountProcessInfo(string workingDir)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(Git.GitPath)
            {
                UseShellExecute = false,
                WorkingDirectory = workingDir,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                Arguments = "rev-list --count HEAD"
            };

            return startInfo;
        }

    }
}
