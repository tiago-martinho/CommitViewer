using System;

namespace CommitViewer.Models
{
    /// <summary>
    /// Model class that represents an author in a commit
    /// </summary>
    public class Author
    {

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Email)}: {Email}, {nameof(Date)}: {Date}";
        }
    }
}
