
using BlazorPWA.Core.Renders;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Markdig;

namespace BlazorPWA.Core
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
            => services.AddOptions()
                .AddTransient<IContentProvider, GithubMdContentProvider>()
                .AddSingleton<IHtmlParser, MarkdownHtmlParser>()
                .Configure<MarkdownPipelineBuilder>(builder => builder.UseFooters().UseAbbreviations().UseCustomContainers());

        public void Configure(IComponentsApplicationBuilder app) => app.AddComponent<App>("app");
    }
}
