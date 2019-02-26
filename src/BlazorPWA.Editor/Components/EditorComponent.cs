using BlazorPWA.Components.Editor.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorPWA.Components.Editor.Components
{
    public class EditorComponent : ComponentBase
    {
        [Parameter]
        protected internal EditorModel Model { get; set; }

        private bool _alreadyRendered = false;

        protected override void OnAfterRender()
        {
            if (!_alreadyRendered)
            {
                Model.EditorInitialize();
                _alreadyRendered = true;
            }
        }

        async public Task EditorUpdate()
        {
            Model = await Model.EditorGet();
            Console.WriteLine($"Script is now: {Model.Script}");
        }

        async public Task EditorSetValue(string newScript)
        {
            Model.Script = newScript;
            Model = await Model.EditorSet();
        }
    }
}
