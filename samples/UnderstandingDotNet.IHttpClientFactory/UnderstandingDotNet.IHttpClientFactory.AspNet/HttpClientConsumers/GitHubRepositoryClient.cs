using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnderstandingDotNet.IHttpClientFactory.AspNet.Models;

namespace UnderstandingDotNet.IHttpClientFactory.AspNet.HttpClientConsumers
{
    /// <summary>
    /// For typed usage
    /// </summary>
    public sealed class GitHubRepositoryClient
    {
        public HttpClient Client { get; }

        public GitHubRepositoryClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://api.github.com/");
            client.DefaultRequestHeaders.Add("Accept",
                "application/vnd.github.v3+json");
            client.DefaultRequestHeaders.Add("User-Agent",
                "HttpClient-Example");

            Client = client;
        }

        public async Task<GitHubRepository> Get(string id, CancellationToken cancellationToken = default)
        {
            id = string.IsNullOrWhiteSpace(id) ? "244436497" : id;
            var response = await Client.GetAsync(
                $"/repositories/{id}", cancellationToken);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GitHubRepository>(responseContent);
        }
    }
}