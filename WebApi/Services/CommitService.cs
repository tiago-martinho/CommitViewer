using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using CommitViewer;
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
            return ProcessUtils.StartLogProcess(workingDir).ToImmutableList();
        }

        public IPage<Commit> GetPagedCommits(string workingDir, string githubUrl, PageRequest pageRequest)
        {
            Page<Commit> commitPage = new Page<Commit>();
            commitPage.PageNumber = pageRequest.RequestedPage;
            commitPage.PageSize = pageRequest.RequestedNumberOfResults;

            workingDir = ProcessUtils.StartCloneProcess(workingDir, githubUrl);
            commitPage.Content = ProcessUtils.StartLogProcess(workingDir, pageRequest.RequestedNumberOfResults, CalculateSkipNumber(pageRequest));

            return commitPage;
        }

        private int CalculateSkipNumber(PageRequest pageRequest)
        {
            return (pageRequest.RequestedPage - 1) * pageRequest.RequestedNumberOfResults;
        }
    }
}
