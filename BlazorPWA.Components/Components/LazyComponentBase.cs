using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Threading.Tasks;

namespace BlazorPWA.Components.Components
{
    public class LazyComponentBase : ComponentBase
    {
        private static RenderFragment _empty = _ => { };
        private RenderFragment _content;
        [Parameter]
        protected RenderFragment Loading { get; set; }
        [Parameter]
        protected RenderFragment<RenderFragment> Loaded { get; set; }
        [Parameter]
        protected RenderFragment<Exception> Error { get; set; }

        protected virtual Task<RenderFragment> GetContentAsync()
            => Task.FromResult(_empty);

        protected override void OnInit()
        {
            base.OnInit();
            _content = Loading;
        }

        async protected override Task OnAfterRenderAsync()
        {
            base.OnAfterRender();
            try
            {
                var render = await GetContentAsync();
                _content = Loaded(render);//(b => b.AddContent(0, content));
            }
            catch (Exception ex)
            {
                _content = Error(ex);
            }
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            builder.AddContent(0, _content);
        }
    }
}
