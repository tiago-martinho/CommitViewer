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
            ICollection<Commit> commits = commitService.GetCommitCollection(workingDir, githubUrl);
            return Ok(commits);
        }

        [HttpGet("paged")]
        public ActionResult<IPage<Commit>> GetPagedCommits(string workingDir, string githubUrl, [FromHeader(Name = "PageRequest")] PageRequest pageRequest)
        {
            logger.LogDebug("GetCommitCollection called with workingDir: {0}, githubUrl: {1} and pageRequest: {2}", workingDir, githubUrl, pageRequest);
            IPage<Commit> commits = commitService.GetPagedCommits(workingDir, githubUrl, pageRequest);
            return Ok(commits);
        }
        
    }
}
