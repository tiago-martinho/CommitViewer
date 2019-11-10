using System;
using System.Collections.Generic;
using System.Text;

namespace CommitViewer.Exceptions
{
    /// <summary>
    /// Base exception for CommitViewer 
    /// </summary>
    public class CommitViewerException : Exception
    {
        public CommitViewerException()
        {
        }
        public CommitViewerException(string message) : base(message)
        {
        }

        public CommitViewerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
