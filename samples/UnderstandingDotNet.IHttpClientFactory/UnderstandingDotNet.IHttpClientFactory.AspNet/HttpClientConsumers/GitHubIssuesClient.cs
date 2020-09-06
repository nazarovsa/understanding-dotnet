using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnderstandingDotNet.IHttpClientFactory.AspNet.Models;

namespace UnderstandingDotNet.IHttpClientFactory.AspNet.HttpClientConsumers
{
    /// <summary>
    /// For named client usage
    /// </summary>
    public sealed class GitHubIssuesClient
    {
        public HttpClient Client { get; }

        public GitHubIssuesClient(System.Net.Http.IHttpClientFactory clientFactory)
        {
            Client = clientFactory.CreateClient("github_issues");
        }

        public async Task<IReadOnlyCollection<GitHubIssue>> Get(string id, CancellationToken cancellationToken = default)
        {
            id = string.IsNullOrWhiteSpace(id) ? "244436497" : id;
            var response = await Client.GetAsync(
                $"/repositories/{id}/issues", cancellationToken);

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<GitHubIssue>>(responseStream);
        }
    }
}