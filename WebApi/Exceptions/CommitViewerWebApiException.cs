using System;

namespace WebApi.Exceptions
{
    /// <summary>
    /// Base exception for WebApi
    /// </summary>
    public class CommitViewerWebApiException : Exception
    {
        public CommitViewerWebApiException()
        {
        }
        public CommitViewerWebApiException(string message) : base(message)
        {
        }

        public CommitViewerWebApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}