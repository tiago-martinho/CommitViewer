using System.Collections.Generic;
using System.IO;
using CommitViewer.Models;

namespace CommitViewer.CommitProcessors
{
    public interface ICommitProcessor
    {
        IEnumerable<Commit> ProcessCommitStream(TextReader textReader);
    }
}
