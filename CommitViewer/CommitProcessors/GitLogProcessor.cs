using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using CommitViewer.Enums;
using CommitViewer.Utils;
using Domain.Models;
using Serilog;

namespace CommitViewer.CommitProcessors
{

    /// <summary>
    /// Commit processor for "git log --date=iso-strict" process
    /// </summary>
    public class GitLogProcessor : ICommitProcessor
    {

        public string GetProcessorArguments()
        {
            return "log --date=iso-strict";
        }

        /// <summary>
        /// Processes the commit stream and returns a structured commit collection
        /// It ignores the merge line for simplicity and consistency with the data structure and other processors
        /// </summary>
        /// <param name="textReader"></param>
        /// <returns></returns>
        public IEnumerable<Commit> ProcessCommitStream(TextReader textReader)
        {
            Log.Debug("Processing the commit stream...");
            string line = textReader.ReadLine();
            Log.Verbose("Processing line: {0}", line);
            ICollection<Commit> commitCollection = new Collection<Commit>();
            Commit commit = new Commit { CommitInfo = new CommitInfo() };
            while (line != null)
            {
                if (ProcessCommitLine(line, commit))
                {
                    commitCollection.Add(commit);
                    Log.Verbose("Finished processing the commit: {0}", commit);
                    commit = new Commit { CommitInfo = new CommitInfo() };
                }
                line = textReader.ReadLine();
            }

            return commitCollection;

        }

        /// <summary>
        /// Processes each commit line from a single commit
        /// Returns true if it processed the last relevant information of the commit (the message), returns false otherwise
        /// </summary>
        /// <param name="line"></param>
        /// <param name="commit"></param>
        /// <returns></returns>
        private static bool ProcessCommitLine(string line, Commit commit)
        {

            string[] lineArray = line.Split(CommitUtils.LineTypeSeparator, 2);

            if (!Enum.TryParse(lineArray[0], out CommitLineType commitLineType)) // it's the commit hash, the commit message or empty lines
            {

                if (string.IsNullOrWhiteSpace(line))
                {
                    return false;
                }

                if (line.StartsWith("commit")) // it's the hash
                {
                    commit.Hash = CommitUtils.GetHash(line);
                    return false;
                }

                if (commit.CommitInfo != null)
                {
                    commit.CommitInfo.Message = line.Trim();
                    return true;
                }

            }

            switch (commitLineType)
            {
                case CommitLineType.Author:
                    if (commit.CommitInfo != null)
                    {
                        commit.CommitInfo.Author = CommitUtils.GetAuthor(line);
                    }
                    return false;
                case CommitLineType.Date:
                    if (commit.CommitInfo?.Author != null)
                    {
                        commit.CommitInfo.Author.Date = CommitUtils.GetDateTime(line);
                    }
                    return false;
                default:
                    Log.Warning("Couldn't process this particular line: {0}. It will be ignored.", line);
                    return false;
            }
        }

    }
}
