using System;

namespace Domain.Utils
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

        }
    }
}
