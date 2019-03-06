using Markdig;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorPWA.Core.Renders
{

    public interface IHtmlParser
    {
        string ToHtml(string name, string md);
    }

}
