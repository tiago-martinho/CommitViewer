using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace GitHubClient.Services
{
    /// <summary>
    /// Main GitHubService
    /// This service could be decomposed into smaller services for a bigger project like (CommitService, RepositoryService, UserService, etc)
    /// </summary>
    public interface IGitHubService
    {

        /// <summary>
        /// Gets repository commits for a given repository and username
        /// </summary>
        /// <param name="username"></param>
        /// <param name="repositoryName"></param>
        /// <returns></returns>
        Task<IEnumerable<Commit>> GetRepositoryCommits(string username, string repositoryName);
    }
}
