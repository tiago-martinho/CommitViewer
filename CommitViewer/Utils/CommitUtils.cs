using System;
using System.Globalization;
using CommitViewer.Models;

namespace CommitViewer.Utils
{
    internal static class CommitUtils
    {
        public const char LineTypeSeparator = ':';

        /// <summary>
        /// Parses dates in strict ISO format given a typical commit date line (e.g Date: datevalue)
        /// </summary>
        /// <param name="dateLine"></param>
        /// <returns></returns>
        internal static DateTime GetDateTime(string dateLine)
        {
            string[] split = dateLine.Split(LineTypeSeparator, 2);
            DateTime date = DateTime.Parse(split[1].TrimStart());
            return date;
        }

        internal static Author GetAuthor(string authorLine)
        {
            string[] split = authorLine.Split(LineTypeSeparator, 2)[1].Split('<');
            Author author = new Author {Name = split[0].Trim(), Email = split[1].Replace('>', ' ').Trim()};
            return author;
        }

        internal static string GetHash(string hashLine)
        {
            return hashLine.Split(' ')[1];
        }

        internal static Commit GetCommit(string[] commitLineArray)
        {
            Commit commit = new Commit
            {
                Hash = commitLineArray[0],
                CommitInfo = new CommitInfo
                {
                    Author = new Author {Date = DateTime.Parse(commitLineArray[1]), Name = commitLineArray[2], Email = commitLineArray[3]},
                    Message = commitLineArray[4]

                }
            };
            return commit;
        }
    }
}
