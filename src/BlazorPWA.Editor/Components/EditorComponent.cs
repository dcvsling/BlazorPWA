using BlazorPWA.Components.Editor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorPWA.Components.Editor.Components
{
    public class EditorComponent : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Parameter]
        protected internal EditorModel Model { get; set; }

        private bool _alreadyRendered = false;

        protected override void OnAfterRender()
        {
            if (!_alreadyRendered)
            {
                JSRuntime.EditorInitialize(Model);
                _alreadyRendered = true;
            }
        }

        async public Task EditorUpdate()
        {
            Model = await JSRuntime.EditorGet(Model);
            Console.WriteLine($"Script is now: {Model.Script}");
        }

        async public Task EditorSetValue(string newScript)
        {
            Model.Script = newScript;
            Model = await JSRuntime.EditorSet(Model);
        }
    }
}
