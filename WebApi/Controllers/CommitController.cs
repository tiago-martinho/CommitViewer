using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using CommitViewer.Models;
using WebApi.Pagination;
using WebApi.Services;

namespace WebApi.Controllers
{

    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class CommitController : ControllerBase
    {

        private readonly ILogger logger;
        private readonly ICommitService commitService;

        public CommitController(ILogger<CommitController> logger, ICommitService commitService)
        {
            this.logger = logger;
            this.commitService = commitService;
        }

        [HttpGet]
        public ActionResult<IReadOnlyCollection<Commit>> GetCommitCollection(string workingDir, string githubUrl)
        {
            logger.LogDebug("GetCommitCollection called with workingDir: {0} and githubUrl: {1}", workingDir, githubUrl);

            if (string.IsNullOrWhiteSpace(workingDir) || string.IsNullOrWhiteSpace(githubUrl))
            {
                const string exceptionMessage = "Please provide valid parameter values. Working directory and GitHub url cannot be empty or null.";
                logger.LogInformation(exceptionMessage);
                throw new ArgumentException(exceptionMessage);
            }

            ICollection<Commit> commits = commitService.GetCommitCollection(workingDir, githubUrl);
            return Ok(commits);
        }

        [HttpGet("paged")]
        public ActionResult<IPage<Commit>> GetPagedCommits(string workingDir, string githubUrl, int pageNumber, int pageSize)
        {
            logger.LogDebug("GetPagedCommits called with workingDir: {0}, githubUrl: {1}, pageNumber: {2} and pageSize: {3}", workingDir, githubUrl, pageNumber, pageSize);
            
            if (string.IsNullOrWhiteSpace(workingDir) || string.IsNullOrWhiteSpace(githubUrl) || pageNumber <= 0 ||
                pageSize <= 0)
            {
                const string exceptionMessage = "Please provide valid parameter values. Working directory and GitHub url cannot be empty or null. " +
                                                "Page number and page size must be positive.";
                logger.LogInformation(exceptionMessage);
                throw new ArgumentException(exceptionMessage);
            }

            IPage<Commit> commits = commitService.GetPagedCommits(workingDir, githubUrl, pageNumber, pageSize);
            return Ok(commits);
        }
        
    }
}
