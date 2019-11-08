using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CommitViewer.Models;

namespace CommitViewer
{
    internal class GitHubClient
    {
        public HttpClient Client { get; }

        public GitHubClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://api.github.com/");
            // GitHub API versioning
            client.DefaultRequestHeaders.Add("Accept", 
                "application/vnd.github.v3+json");
            // GitHub requires a user-agent
            client.DefaultRequestHeaders.Add("User-Agent", 
                "HttpClientFactory-Sample");

            Client = client;
        }

        //public async Task<IEnumerable<Commit>> GetRepositoryCommits(string workingDir, string githubUrl)
        //{
        //    var response = await Client.GetAsync("url");

        //    response.EnsureSuccessStatusCode();

        //    var result = await response.Content
        //        .ReadAsAsync<IEnumerable<Commit>>();

        //    return result;
        //}
    }
}
