using System.Collections.Generic;
using System.IO;
using CommitViewer.Models;

namespace CommitViewer.CommitProcessors
{
    public interface ICommitProcessor
    {
        /// <summary>
        /// Arguments to be given to the commit processor
        /// For example, when using the "git" process the user can provide additional arguments for this process
        /// </summary>
        string GetProcessorArguments();

        IEnumerable<Commit> ProcessCommitStream(TextReader textReader);
    }
}
