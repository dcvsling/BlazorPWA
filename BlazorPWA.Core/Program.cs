﻿using Microsoft.AspNetCore.Blazor.Hosting;
using System;

namespace BlazorPWA.Core
{
    public static class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IWebAssemblyHostBuilder CreateHostBuilder(string[] _) => 
            BlazorWebAssemblyHost.CreateDefaultBuilder()
                .UseBlazorStartup<Startup>();
    }
}
