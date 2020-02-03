using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Models;
using GitHubClient.Exceptions;

namespace GitHubClient.Services
{
    /// <summary>
    /// Main GitHubService
    /// This service could be decomposed into smaller services for a bigger project like (CommitService, RepositoryService, UserService, etc)
    /// </summary>
    public class GitHubService : IGitHubService
    {
        private HttpClient Client { get; }

        public GitHubService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://api.github.com/");
            client.DefaultRequestHeaders.Add("Accept", 
                "application/vnd.github.v3+json");
            client.DefaultRequestHeaders.Add("User-Agent", 
                "CommitViewer GitHubClient");

            Client = client;
        }

        /// <summary>
        /// Gets repository commits for a given repository and username
        /// </summary>
        /// <param name="username"></param>
        /// <param name="repositoryName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Commit>> GetRepositoryCommits(string username, string repositoryName)
        {

            IEnumerable<Commit> results = default;

            try
            {
                using (HttpResponseMessage response = await Client.GetAsync("repos/"+username+"/"+repositoryName+"/commits"))
                {
                    response.EnsureSuccessStatusCode();
                    if (response.Content != null)
                        results = await response.Content.ReadAsAsync<IEnumerable<Commit>>();
                }
            }
            catch (HttpRequestException httpException)
            {
                throw new GitHubClientException("An exception was thrown while making the request", httpException);
            }


            return results;
        }
    }
}
