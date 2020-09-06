using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnderstandingDotNet.IHttpClientFactory.AspNet.HttpClientConsumers;

namespace UnderstandingDotNet.IHttpClientFactory.AspNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExampleController : ControllerBase
    {
        private readonly GitHubRepositoryClient _repositoryClient;
        private readonly GitHubAccountClient _accountClient;
        private readonly GitHubIssuesClient _issuesClient;

        public ExampleController(GitHubRepositoryClient repositoryClient, GitHubAccountClient accountClient,
            GitHubIssuesClient issuesClient)
        {
            if (repositoryClient == null)
                throw new ArgumentNullException(nameof(repositoryClient));

            if (accountClient == null)
                throw new ArgumentNullException(nameof(accountClient));

            if (issuesClient == null)
                throw new ArgumentNullException(nameof(issuesClient));

            _repositoryClient = repositoryClient;
            _accountClient = accountClient;
            _issuesClient = issuesClient;
        }

        [HttpGet("repository")]
        public async Task<IActionResult> GetRepository([FromQuery] string id, CancellationToken cancellationToken)
        {
            var result = await _repositoryClient.Get(id, cancellationToken);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetAccount([FromQuery] string username, CancellationToken cancellationToken)
        {
            var result = await _accountClient.Get(username, cancellationToken);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpGet("issue")]
        public async Task<IActionResult> GetIssues([FromQuery] string repositoryId, CancellationToken cancellationToken)
        {
            var result = await _issuesClient.Get(repositoryId, cancellationToken);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }
    }
}