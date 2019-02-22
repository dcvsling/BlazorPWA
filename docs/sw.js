
let cacheName = 'BlazorPWA';
let filesToCache = [
    '/',
    '/favicon.ico',
    '/index.html',
    '/manifest.json',
    '/sw.js',
    '/css/site.css',
    '/css/bootstrap/bootstrap.min.css',
    '/css/bootstrap/bootstrap.min.css.map',
    '/css/open-iconic/font/css/open-iconic-bootstrap.min.css',
    '/css/open-iconic/font/fonts/open-iconic.woff',
    '/icons/icon-192x192.png',
    '/icons/icon-512x512.png',
    '/_framework/blazor.boot.json',
    '/_framework/components.webassembly.js',
    '/_framework/wasm/mono.js',
    '/_framework/wasm/mono.wasm',
    '/_framework/_bin/BlazorPWA.Components.dll',
    '/_framework/_bin/BlazorPWA.Components.pdb',
    '/_framework/_bin/BlazorPWA.Core.dll',
    '/_framework/_bin/BlazorPWA.Core.pdb',
    '/_framework/_bin/Microsoft.AspNetCore.Blazor.dll',
    '/_framework/_bin/Microsoft.AspNetCore.Components.Browser.dll',
    '/_framework/_bin/Microsoft.AspNetCore.Components.dll',
    '/_framework/_bin/Microsoft.Extensions.DependencyInjection.Abstractions.dll',
    '/_framework/_bin/Microsoft.Extensions.DependencyInjection.dll',
    '/_framework/_bin/Microsoft.Extensions.Http.dll',
    '/_framework/_bin/Microsoft.Extensions.Logging.Abstractions.dll',
    '/_framework/_bin/Microsoft.Extensions.Logging.dll',
    '/_framework/_bin/Microsoft.Extensions.Options.dll',
    '/_framework/_bin/Microsoft.Extensions.Primitives.dll',
    '/_framework/_bin/Microsoft.JSInterop.dll',
    '/_framework/_bin/Microsoft.MarkedNet.dll',
    '/_framework/_bin/Mono.Security.dll',
    '/_framework/_bin/Mono.WebAssembly.Interop.dll',
    '/_framework/_bin/mscorlib.dll',
    '/_framework/_bin/netstandard.dll',
    '/_framework/_bin/System.Collections.dll',
    '/_framework/_bin/System.ComponentModel.Composition.dll',
    '/_framework/_bin/System.Core.dll',
    '/_framework/_bin/System.Data.dll',
    '/_framework/_bin/System.Diagnostics.Debug.dll',
    '/_framework/_bin/System.dll',
    '/_framework/_bin/System.Drawing.dll',
    '/_framework/_bin/System.IO.Compression.dll',
    '/_framework/_bin/System.IO.Compression.FileSystem.dll',
    '/_framework/_bin/System.Linq.dll',
    '/_framework/_bin/System.Memory.dll',
    '/_framework/_bin/System.Net.Http.dll',
    '/_framework/_bin/System.Numerics.dll',
    '/_framework/_bin/System.Runtime.CompilerServices.Unsafe.dll',
    '/_framework/_bin/System.Runtime.dll',
    '/_framework/_bin/System.Runtime.Extensions.dll',
    '/_framework/_bin/System.Runtime.Serialization.dll',
    '/_framework/_bin/System.ServiceModel.Internals.dll',
    '/_framework/_bin/System.Text.RegularExpressions.dll',
    '/_framework/_bin/System.Transactions.dll',
    '/_framework/_bin/System.Web.Services.dll',
    '/_framework/_bin/System.Xml.dll',
    '/_framework/_bin/System.Xml.Linq.dll',
    '/_content/BlazorPWA.Components/JsInterop.js',
    '/md/README.md',
    '/md/angular-2/about-learning.md',
    '/md/angular-2/dynamically.md',
    '/md/angular-2/mvc.md',
    '/md/CSharp/timing.md',
    '/md/CSharp/AsyncAwait/bi-jiao-delegate-task-lazy-de-xing-wei-mo-shi.md',
    '/md/CSharp/AsyncAwait/task-yu-ienumerable-jian-de-chang-jian-wen-ti.md',
    '/md/CSharp/AsyncAwait/TaskAsyncAwait.md',
    '/md/design-pattern/action-and-filter-and-middleware.md',
    '/md/design-pattern/decorator-and-convention.md',
    '/md/design-pattern/options-pattern.md',
    '/md/design-pattern/pattern-in-di.md',
    '/md/design-pattern/dependency-inversion/constructor-injection.md',
    '/md/oop/jiang-if-switch-pan-duan-zhuan-wei-yong-wu-jian-lai-jie-jue.md',
    '/md/oop/oop.md',
    '/md/reactive-programming/different.md',
    '/md/reactive-programming/from-linq-through-interactive(ix)-to-reactive(rx).md',
    '/md/reactive-programming/observer-and-observable.md',
    '/md/reactive-programming/start-from-iterator.md',
    '/md/reactive-programming/observer-and-observable/ioc-point.md'
];

self.addEventListener('install', function (e) {
    console.log('[ServiceWorker] Install');
    e.waitUntil(
        caches.open(cacheName).then(function (cache) {
            console.log('[ServiceWorker] Caching app shell');
            cache.addAll(filesToCache);
        })
    );
});

self.addEventListener('activate', event => {
    console.log('[ServiceWorker] activate');
    event.waitUntil(self.clients.claim());
});

self.addEventListener('fetch', event => {
    console.log('[ServiceWorker] fetch ' + event.request, event.request);
    event.respondWith(
        caches.match(event.request, { ignoreSearch: true }).then(response => {
            return response || fetch(event.request);
        })
    );
});