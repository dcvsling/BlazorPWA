using System.Collections.Concurrent;
using System.Linq;

namespace BlazorPWA.Components.Models
{
    public class Context : ConcurrentDictionary<string, Optional<object>>
    {
        public new Optional<object> this[string name]
        {
            get => Get<object>(name);
            set => Set(name, value.ToArray());
        }

        public Optional<T> Get<T>(string name)
            => GetOrAdd(name, _ => new Optional<object>()).OfType<T>().ToArray();

        public void Set<T>(string name, params T[] ts)
            => AddOrUpdate(name, ts, (_, old) => old.Concat(ts.OfType<object>()).ToList());
    }
}
