using Markdig;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorPWA.Core.Renders
{

    internal class MarkdownHtmlParser : IHtmlParser
    {
        private readonly IOptionsSnapshot<MarkdownPipelineBuilder> _snapshot;
        private readonly IDictionary<string, MarkdownPipeline> _pipelines;
        public MarkdownHtmlParser(IOptionsSnapshot<MarkdownPipelineBuilder> snapshot)
        {
            _snapshot = snapshot;
            _pipelines = new Dictionary<string, MarkdownPipeline>();
        }

        public string ToHtml(string name, string md)
            => Markdown.ToHtml(md, GetOrAdd(name));

        private MarkdownPipeline GetOrAdd(string name)
        {
            if (_pipelines.ContainsKey(name)) return _pipelines[name];
            _pipelines.Add(name, _snapshot.Get(name).Build());
            return _pipelines[name];

        }
    }

}
