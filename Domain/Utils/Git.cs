using System;

namespace Domain.Utils
{
    public static class Git
    {
        private const string GitEnvironmentVariableKey = "GIT";

        public static string GitPath { get; private set; }

        public static void Setup()
        {
            GitPath = Environment.GetEnvironmentVariable(GitEnvironmentVariableKey);
            if (string.IsNullOrWhiteSpace(GitPath))
            {
                throw new InvalidOperationException("The " + GitEnvironmentVariableKey + " environment variable is not set. Please set the environment variable with key: " + GitEnvironmentVariableKey + " and the value pointing to your git.exe");
            }

        }
    }
}
