namespace CommitViewer.Models
{
    public class Author
    {

        public string Username { get; set; }

        public string Email { get; set; }

        public override string ToString()
        {
            return $"{nameof(Username)}: {Username}, {nameof(Email)}: {Email}";
        }
    }
}
