using System;
using System.Collections.Generic;
using System.Text;

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

            Console.WriteLine("Git environment variable is set");
        }
    }
}
