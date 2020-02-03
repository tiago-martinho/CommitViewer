using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Domain.Models;
using WebApi.Exceptions;
using WebApi.Pagination;
using WebApi.Services;

namespace WebApi.Controllers
{

    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class CommitController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly ICommitService _commitService;

        public CommitController(ILogger<CommitController> logger, ICommitService commitService)
        {
            _logger = logger;
            _commitService = commitService;
        }

        /// <summary>
        /// Returns a commit collection
        /// </summary>
        /// <param name="workingDir"></param>
        /// <param name="githubUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IReadOnlyCollection<Commit>> GetCommitCollection(string workingDir, string githubUrl)
        {
            _logger.LogDebug("GetCommitCollection called with workingDir: {0} and githubUrl: {1}", workingDir, githubUrl);

            if (string.IsNullOrWhiteSpace(workingDir) || string.IsNullOrWhiteSpace(githubUrl))
            {
                const string exceptionMessage = "Please provide valid parameter values. Working directory and GitHub url cannot be empty or null.";
                _logger.LogInformation(exceptionMessage);
                throw new ArgumentException(exceptionMessage);
            }

            ICollection<Commit> commits;
            try
            {
                commits = _commitService.GetCommitCollection(workingDir, githubUrl);
            }
            catch (TimeoutException e)
            {
                throw new CommitViewerWebApiException("Took too long to fetch the commits. Please try again later.", e);
            }
            
            return Ok(commits);
        }

        /// <summary>
        /// Returns paginated commit results
        /// </summary>
        /// <param name="workingDir"></param>
        /// <param name="githubUrl"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("paged")]
        public ActionResult<IPage<Commit>> GetPagedCommits(string workingDir, string githubUrl, int pageNumber, int pageSize)
        {
            _logger.LogDebug("GetPagedCommits called with workingDir: {0}, githubUrl: {1}, pageNumber: {2} and pageSize: {3}", workingDir, githubUrl, pageNumber, pageSize);
            
            if (string.IsNullOrWhiteSpace(workingDir) || string.IsNullOrWhiteSpace(githubUrl) || pageNumber <= 0 ||
                pageSize <= 0)
            {
                const string exceptionMessage = "Please provide valid parameter values. Working directory and GitHub url cannot be empty or null. " +
                                                "Page number and page size must be positive.";
                _logger.LogInformation(exceptionMessage);
                throw new ArgumentException(exceptionMessage);
            }

            IPage<Commit> commits;
            try
            {
                commits = _commitService.GetPagedCommits(workingDir, githubUrl, pageNumber, pageSize);
            }
            catch (TimeoutException e)
            {
                throw new CommitViewerWebApiException("Took too long to fetch the commits. Please try again later.", e);
            }

            return Ok(commits);
        }
        
    }
}
