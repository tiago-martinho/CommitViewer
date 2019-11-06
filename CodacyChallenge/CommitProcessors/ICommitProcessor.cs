using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodacyChallenge.CommitProcessors
{
    internal interface ICommitProcessor
    {
        IEnumerable<Commit> ProcessCommitStream(TextReader textReader);
    }
}
