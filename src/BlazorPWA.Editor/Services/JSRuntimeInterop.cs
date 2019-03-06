using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorPWA.Components.Editor.Services
{
    public static class JSRuntimeInterop
    {
        public static Task<bool> EditorInitialize(this IJSRuntime js, EditorModel editorModel)
            => js.InvokeAsync<bool>("BlazorPWA.Components.Monaco.Interop.EditorInitialize", new[] { editorModel });

        public static Task<EditorModel> EditorGet(this IJSRuntime js, EditorModel editorModel)
            => js.InvokeAsync<EditorModel>("BlazorPWA.Components.Monaco.Interop.EditorGet", new[] { editorModel });

        public static Task<EditorModel> EditorSet(this IJSRuntime js, EditorModel editorModel)
            => js.InvokeAsync<EditorModel>("BlazorPWA.Components.Monaco.Interop.EditorSet", new[] { editorModel });
    }
}
