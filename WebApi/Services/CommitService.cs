using System.Collections.Generic;
using System.Collections.Immutable;
using CommitViewer.Models;
using CommitViewer.Utils;
using WebApi.Pagination;

namespace WebApi.Services
{
    public class CommitService : ICommitService
    {
        public ICollection<Commit> GetCommitCollection(string workingDir, string githubUrl)
        {
            workingDir = ProcessUtils.StartCloneProcess(workingDir, githubUrl);
            ICollection<Commit> commits = ProcessUtils.StartLogProcess(workingDir).ToImmutableList();
            DirectoryUtils.DeleteDirectory(workingDir);
            return commits;

        }

        public IPage<Commit> GetPagedCommits(string workingDir, string githubUrl, int pageNumber, int pageSize)
        {
            workingDir = ProcessUtils.StartCloneProcess(workingDir, githubUrl);
            int totalElements = ProcessUtils.StartCommitCountProcess(workingDir);
            IEnumerable<Commit> commits = ProcessUtils.StartLogProcess(workingDir, pageSize, CalculateSkipNumber(pageNumber, pageSize));
            DirectoryUtils.DeleteDirectory(workingDir);
            return new Page<Commit>(pageNumber, pageSize, totalElements, commits);
        }

        private int CalculateSkipNumber(int pageNumber, int pageSize)
        {
            return (pageNumber - 1) * pageSize;
        }
    }
}
