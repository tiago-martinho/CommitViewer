using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using CommitViewer.CommitProcessors;
using CommitViewer.Utils;
using Domain.Models;
using Domain.Utils;
using Microsoft.Extensions.Logging;
using WebApi.Pagination;

namespace WebApi.Services
{
    public class CommitService : ICommitService
    {
        /// <summary>
        /// We only need to instantiate the ICommitProcessor once when we instantiate the commit service
        /// However, it's not a static class so that we have can multiple implementations and change which we want to use easily
        /// </summary>
        private readonly ICommitProcessor _commitProcessor = new GitLogProcessor();
        private readonly ILogger _logger;

        public CommitService(ILogger<CommitService> logger)
        {
            _logger = logger;
        }

        public ICollection<Commit> GetCommitCollection(string workingDir, string githubUrl)
        {
            _logger.LogInformation("Starting cloning process...");
            workingDir = ProcessUtils.StartCloneProcess(workingDir, githubUrl);
            _logger.LogInformation("Repository was cloned. Starting log process...");
            ICollection<Commit> commits = ProcessUtils.StartLogProcess(workingDir, _commitProcessor).ToImmutableList();
            DirectoryUtils.DeleteDirectory(workingDir);
            return commits;

        }

        public IPage<Commit> GetPagedCommits(string workingDir, string githubUrl, int pageNumber, int pageSize)
        {
            GitLogProcessor gitLogProcessor = new GitLogProcessor();  //paged methods have to use the base GitLogProcessor
            workingDir = ProcessUtils.StartCloneProcess(workingDir, githubUrl);
            int totalElements = ProcessUtils.StartCommitCountProcess(workingDir);
            IEnumerable<Commit> commits = ProcessUtils.StartPagedLogProcess(workingDir, gitLogProcessor, pageSize, CalculateSkipNumber(pageNumber, pageSize));
            DirectoryUtils.DeleteDirectory(workingDir);
            return new Page<Commit>(pageNumber, pageSize, totalElements, commits);

        }

        private int CalculateSkipNumber(int pageNumber, int pageSize)
        {
            return (pageNumber - 1) * pageSize;
        }
    }
}
