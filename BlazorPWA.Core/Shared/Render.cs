using BlazorPWA.Components.Components;
using BlazorPWA.Core.Renders;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorPWA.Core.Shared
{
    public class Render : LazyComponentBase
    {
        [Inject]
        public IContentProvider ContentProvider { get; set; }
        [Parameter]
        private string Renderer { get; set; }
        [Parameter]
        private string Path { get; set; }

        async protected override Task<RenderFragment> GetContentAsync()
        {
            var content = await ContentProvider.GetAsync(Renderer, Path);
            return builder => builder.AddMarkupContent(0, content);
        }
    }
}
