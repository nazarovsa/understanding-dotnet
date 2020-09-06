using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UnderstandingDotNet.IHttpClientFactory.AspNet.HttpClientConsumers;

namespace UnderstandingDotNet.IHttpClientFactory.AspNet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpClient();

            // For GitHubAccountClient
            services.AddHttpClient();
            services.AddScoped<GitHubAccountClient>();

            // For GitHubRepositoryClient - Register GitHubRepositoryClient automatically
            services.AddHttpClient<GitHubRepositoryClient>();

            // For GitHubIssuesClient
            services.AddHttpClient("github_issues", client =>
            {
                client.BaseAddress = new Uri("https://api.github.com");
                client.DefaultRequestHeaders.Add("Accept",
                    "application/vnd.github.v3+json");
                client.DefaultRequestHeaders.Add("User-Agent",
                    "HttpClient-Example");
            });
            services.AddScoped<GitHubIssuesClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}