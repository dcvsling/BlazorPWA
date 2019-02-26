
using BlazorPWA.Core.Renders;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Markdig;
using Microsoft.Extensions.Configuration;

namespace BlazorPWA.Core
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
            => services
                .AddSingleton<IConfiguration>(_ => new ConfigurationBuilder().AddJsonFile("App.settings.json", true, true).Build())
                .AddOptions()
                .AddTransient<IContentProvider, GithubMdContentProvider>()
                .AddSingleton<IHtmlParser, MarkdownHtmlParser>()
                .Configure<MarkdownPipelineBuilder>(builder => builder.UseFooters().UseAbbreviations().UseCustomContainers());

        public void Configure(IComponentsApplicationBuilder app) => app.AddComponent<App>("app");
    }
}
