using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CodacyChallenge
{
    static class ProcessUtils
    {
        public static ProcessStartInfo GetCloneProcessInfo(string gitEnvPath, string workingDir, string gitHubUrl)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(gitEnvPath)
            {
                UseShellExecute = false,
                WorkingDirectory = workingDir,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                Arguments = @"clone " + gitHubUrl
            };

            return startInfo;
        }

        public static ProcessStartInfo GetLogProcessInfo(string gitEnvPath, string workingDir)
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
