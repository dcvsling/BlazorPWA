using System;
using System.Threading.Tasks;

namespace BlazorPWA.Components.Services.Http
{

    public static class Logger
    {
        async public static Task<T> Log<T>(this T t, Func<T, string> getMsg)
        {
            await getMsg(t).Log();
            return t;
        }
        public static Task Log(string msg)
           => msg.Log();
    }
}

