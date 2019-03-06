Window: {
    Logger: {
        write: msg => console.log(msg);
    }
    Cache: {
        getOrAdd: async (key, fn) => window.sessionStorage[key] || window.sessionStorage.setItem(key, await fn(key));
    }
}