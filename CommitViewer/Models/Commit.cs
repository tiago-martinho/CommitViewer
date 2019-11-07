using System;

namespace CommitViewer.Models
{
    public class Commit
    {
        public Author Author { get; set; }

        public DateTime Date { get; set; }

        public string Hash { get; set; }

        public string Message { get; set; }

        public bool IsMerge { get; set; }
        public string MergeId { get; set; }

        public override string ToString()
        {
            return $"{nameof(Author)}: {Author}, {nameof(Date)}: {Date}, {nameof(Hash)}: {Hash}, {nameof(Message)}: {Message}, {nameof(IsMerge)}: {IsMerge}, {nameof(MergeId)}: {MergeId}";
        }
    }
}
