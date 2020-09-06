using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnderstandingDotNet.IHttpClientFactory.AspNet.Models;

namespace UnderstandingDotNet.IHttpClientFactory.AspNet.HttpClientConsumers
{
    /// <summary>
    /// For IHttpClientFactory usage
    /// </summary>
    public sealed class GitHubAccountClient
    {
        public HttpClient Client { get; }

        public GitHubAccountClient(System.Net.Http.IHttpClientFactory clientFactory)
        {
            var client = clientFactory.CreateClient();
            client.BaseAddress = new Uri("https://api.github.com/");
            client.DefaultRequestHeaders.Add("Accept",
                "application/vnd.github.v3+json");
            client.DefaultRequestHeaders.Add("User-Agent",
                "HttpClient-Example");

            Client = client;
        }

        public async Task<GitHubUser> Get(string userName, CancellationToken cancellationToken = default)
        {
            userName = string.IsNullOrWhiteSpace(userName) ? "insightappdev" : userName;
            var response = await Client.GetAsync(
                $"/users/{userName}", cancellationToken);

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GitHubUser>(responseStream);
        }
    }
}