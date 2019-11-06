using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommitViewer.Models;
using WebApi.Pagination;

namespace WebApi.Services
{
    public interface ICommitService
    {
        /// <summary>
        /// Returns a collection of commits for a given local working directory and a GitHub url
        /// </summary>
        /// <param name="workingDir"></param>
        /// <param name="githubUrl"></param>
        /// <returns></returns>
        ICollection<Commit> GetCommitCollection(string workingDir, string githubUrl);
        /// <summary>
        /// Returns a page of commits for a given local working directory and a GitHub url
        /// </summary>
        /// <param name="workingDir"></param>
        /// <param name="githubUrl"></param>
        /// <param name="pageRequest"></param>
        /// <returns></returns>
        IPage<Commit> GetPagedCommits(string workingDir, string githubUrl, PageRequest pageRequest);
    }
}
