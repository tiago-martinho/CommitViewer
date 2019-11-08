using System;
using Serilog;

namespace CommitViewer
{
    public static class Git
    {
        public static string GitPath { get; private set; }

        public static void Setup()
        {
            GitPath = Environment.GetEnvironmentVariable("GIT");
            if (string.IsNullOrWhiteSpace(GitPath))
            {
                throw new InvalidOperationException("The GIT environment variable is not set. Please set the environment variable pointing to your git.exe");
            }

            Log.Debug("Git environment variable is set. GitPath is now defined with value: {0}", GitPath);
        }
    }
}
