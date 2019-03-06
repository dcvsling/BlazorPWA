using Markdig;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorPWA.Core.Renders
{
    public class GithubMdContentProvider : IContentProvider
    {
        private readonly HttpClient _http;
        private readonly IHtmlParser _parser;
        private readonly ContentOptions _options;

        public GithubMdContentProvider(HttpClient http, IHtmlParser parser,IOptions<ContentOptions> options)
        {
            _http = http;
            _parser = parser;
            _options = options.Value;
        }

        async public Task<string> GetAsync(string type, string path)
            => _parser.ToHtml(path, await _http.GetStringAsync($"{type}/{path}.{type}"));
    }

}
