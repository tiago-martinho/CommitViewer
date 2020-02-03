namespace Domain.Models
{
    /// <summary>
    /// Model class that represents commit information nested inside a commit object
    /// </summary>
    public class CommitInfo
    {
        public Author Author { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return $"{nameof(Author)}: {Author}, {nameof(Message)}: {Message}";
        }
    }
}
