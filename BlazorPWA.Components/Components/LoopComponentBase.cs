using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Collections.Generic;
using System.Linq;

namespace BlazorPWA.Components.Components
{
    public class LoopComponentBase<T> : ComponentBase
    {
        [Parameter]
        protected IReadOnlyList<T> Source { get; set; }
        [Parameter]
        protected RenderFragment<T> Item { get; set; }
        [Parameter]
        protected RenderFragment<RenderFragment> Body { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
            => builder.AddContent(0, Body, Source.Loop().Invoke(Item));
    }
}
