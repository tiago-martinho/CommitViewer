using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace CodacyChallenge
{
    static class CommitProcessor
    {

        public static ICollection<Commit> ProcessCommitStream(StreamReader streamReader)
        {
            string line = streamReader.ReadLine();
            ICollection<Commit> commitCollection = new Collection<Commit>();
            Commit commit = new Commit();
            while (line != null)
            {
                if (ProcessCommitLine(line, commit))
                {
                    commitCollection.Add(commit);
                    commit = new Commit();
                }
                line = streamReader.ReadLine();
            }

            return commitCollection;

        }
    
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

                commit.Message = line.Trim();
                return true;

            }

            switch (commitLineType)
            {
                case CommitLineType.Merge:
                {
                    commit.IsMerge = true;
                    commit.MergeId = CommitUtils.GetMergeId(line);
                    return false;
                }
                case CommitLineType.Author: 
                    commit.Author = CommitUtils.GetAuthor(line);
                    return false;
                case CommitLineType.Date:
                    commit.Date = CommitUtils.GetDateTime(line);
                    return false;
            }

            Console.WriteLine("Couldn't process this particular line. It will be ignored.");
            return false;
        }
    }
}
