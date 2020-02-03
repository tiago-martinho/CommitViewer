using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using CommitViewer.Utils;
using Domain.Models;
using Serilog;

namespace CommitViewer.CommitProcessors
{
    /// <summary>
    /// Commit processor for "git log --pretty=format:"%H %aI %aN %ae %s" process
    /// </summary>
    public class GitLogPrettyFormatProcessor : ICommitProcessor
    {

        private const char Separator = '|';
        private const int FormatArgumentCount = 5;

        public string GetProcessorArguments()
        {
            return @"log --pretty=format:""%H|%aI|%aN|%ae|%s""";
        }

        public IEnumerable<Commit> ProcessCommitStream(TextReader textReader)
        {
            Log.Debug("Processing the commit stream...");
            string line = textReader.ReadLine();
            Log.Verbose("Processing commit: {0}", line);
            ICollection<Commit> commitCollection = new Collection<Commit>();
            while (line != null)
            {
                string[] commitLine = line.Split(Separator);
                if (commitLine.Length != FormatArgumentCount)
                {
                    throw new InvalidOperationException(nameof(GitLogPrettyFormatProcessor) + " is not meant for this kind of commit stream.");
                }
                commitCollection.Add(CommitUtils.GetCommit(commitLine));
                line = textReader.ReadLine();
            }

            return commitCollection;
        }
    }
}
