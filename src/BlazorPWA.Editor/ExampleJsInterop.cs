using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorPWA.Editor
{
    public static class ExampleJsInterop
    {
        public static Task<string> Prompt(IJSRuntime js, string message)
        {
            // Implemented in exampleJsInterop.js
            return js.InvokeAsync<string>(
                "exampleJsFunctions.showPrompt",
                message);
        }
    }
}
