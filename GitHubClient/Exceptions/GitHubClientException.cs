﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GitHubClient.Exceptions
{
    public class GitHubClientException : Exception
    {
        public GitHubClientException()
        {
        }
        public GitHubClientException(string message) : base(message)
        {
        }

        public GitHubClientException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
