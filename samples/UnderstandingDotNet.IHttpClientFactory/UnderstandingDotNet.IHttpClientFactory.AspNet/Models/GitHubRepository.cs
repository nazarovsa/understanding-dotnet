namespace UnderstandingDotNet.IHttpClientFactory.AspNet.Models
{
    public sealed class GitHubRepository
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool Private { get; set; }

        public string Description { get; set; }
    }
}