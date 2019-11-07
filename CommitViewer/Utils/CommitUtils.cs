using System;
using System.Globalization;
using CommitViewer.Models;

namespace CommitViewer.Utils
{
    internal static class CommitUtils
    {
        private const string DateFormat = "ddd MMM d HH:mm:ss yyyy zzz";
        public const char LineTypeSeparator = ':';

        internal static DateTime GetDateTime(string dateLine)
        {
            string[] split = dateLine.Split(LineTypeSeparator, 2);
            DateTime date = DateTime.ParseExact(split[1].TrimStart(), DateFormat, CultureInfo.InvariantCulture);
            return date;
        }

        internal static Author GetAuthor(string authorLine)
        {
            string[] split = authorLine.Split(LineTypeSeparator, 2)[1].Split('<');
            Author author = new Author {Username = split[0].Trim(), Email = split[1].Replace('>', ' ').Trim()};
            return author;
        }

        internal static string GetHash(string hashLine)
        {
            return hashLine.Split(' ')[1];
        }

        internal static string GetMergeId(string mergeIdLine)
        {
            return mergeIdLine.Split(LineTypeSeparator)[1].Trim();
        }
    }
}
