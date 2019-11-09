using Newtonsoft.Json;

namespace Domain.Models
{
    /// <summary>
    /// Base model class that represents a commit
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
