using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorPWA.Components.Services.Http
{
    public static class JSInterop
    {

        public static Task Table<T>(this IJSRuntime js, IEnumerable<T> seq)
            => js.InvokeAsync<object>("console.table", seq);

        async public static Task<string> Log(this IJSRuntime js, string msg)
        {
            await js.InvokeAsync<string>("console.log", msg);
            return msg;
        }

    }

}

