using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorPWA.Components.Editor.Services
{
    public static class JSRuntimeInterop
    {
        public static Task<bool> EditorInitialize(this EditorModel editorModel)
            => JSRuntime.Current.InvokeAsync<bool>("BlazorPWA.Components.Monaco.Interop.EditorInitialize", new[] { editorModel });

        public static Task<EditorModel> EditorGet(this EditorModel editorModel)
            => JSRuntime.Current.InvokeAsync<EditorModel>("BlazorPWA.Components.Monaco.Interop.EditorGet", new[] { editorModel });

        public static Task<EditorModel> EditorSet(this EditorModel editorModel)
            => JSRuntime.Current.InvokeAsync<EditorModel>("BlazorPWA.Components.Monaco.Interop.EditorSet", new[] { editorModel });
    }
}
