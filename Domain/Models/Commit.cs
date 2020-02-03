using Newtonsoft.Json;

namespace Domain.Models
{
    /// <summary>
    /// Base model class that represents a commit with the fundamental commit properties
    /// More properties could be obviously added.
    /// </summary>
    public class Commit
    {
        [JsonProperty("sha")]
        public string Hash { get; set; }

        [JsonProperty("commit")]
        public CommitInfo CommitInfo { get; set; }

        public override string ToString()
        {
            return $"{nameof(Hash)}: {Hash}, {nameof(CommitInfo)}: {CommitInfo}";
        }
    }
}
