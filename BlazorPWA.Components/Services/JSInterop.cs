using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorPWA.Components.Services.Http
{
    public static class JSInterop
    {

        public static Task Table<T>(this IEnumerable<T> seq)
            => JSRuntime.Current.InvokeAsync<object>("console.table", seq);

        async public static Task<string> Log(this string msg)
        {
            await JSRuntime.Current.InvokeAsync<string>("console.log", msg);
            return msg;
        }

    }

}

