using CodacyChallenge;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{

    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class CommitController : ControllerBase
    {

        private readonly ILogger logger;

        public CommitController(ILogger<CommitController> logger)
        {
            this.logger = logger;
        }

        [HttpGet("/")]
        public ActionResult<IReadOnlyCollection<Commit>> GetCommitCollection(string workingDir, string githubUrl)
        {
            logger.LogDebug("GetCommitCollection called with workingDir: {0} and githubUrl: {1}", workingDir, githubUrl);
            string gitEnvValue = ProcessUtils.ValidateGitInstallation();
            workingDir = ProcessUtils.StartCloneProcess(gitEnvValue, workingDir, githubUrl);
            IEnumerable<Commit> commits = ProcessUtils.StartLogProcess(gitEnvValue, workingDir);

            return Ok(commits.ToList());
        }
        
    }
}
