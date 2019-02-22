using Microsoft.JSInterop;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BlazorPWA.Components.Models
{
    public readonly struct Optional<T> : IEnumerable<T>, IEqualityComparer<T>, IEqualityComparer<Optional<T>>
    {
        private readonly List<T> _values;
        private const int _seed = 287;

        private Optional(T t)
        {
            _values = new List<T> { t };
        }

        private Optional(params T[] ts)
        {
            _values = ts.ToList();
        }

        private Optional(IEnumerable<T> ts)
        {
            _values = ts.ToList();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
            => _values.Select(x => x.GetHashCode())
                .Aggregate(_seed, (left, right) => (left << 1) ^ right);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString()
            => Json.Serialize(_values);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj)
            => GetHashCode() == obj.GetHashCode();

        bool IEqualityComparer<T>.Equals(T x, T y)
            => x?.GetHashCode() == y?.GetHashCode();

        bool IEqualityComparer<Optional<T>>.Equals(Optional<T> x, Optional<T> y)
            => x.GetHashCode() == y.GetHashCode();

        public IEnumerator<T> GetEnumerator()
            => _values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        int IEqualityComparer<T>.GetHashCode(T obj)
            => obj?.GetHashCode() ?? 0;

        int IEqualityComparer<Optional<T>>.GetHashCode(Optional<T> obj)
            => obj.GetHashCode();

        public static implicit operator List<T>(in Optional<T> optional)
            => optional._values;

        public static implicit operator T(in Optional<T> optional)
            => optional._values.FirstOrDefault();

        public static implicit operator T[] (in Optional<T> optional)
            => optional._values.ToArray();

        public static implicit operator Optional<T>(List<T> list)
            => new Optional<T>(list);

        public static implicit operator Optional<T>(T t)
            => new Optional<T>(t);

        public static implicit operator Optional<T>(T[] ts)
            => new Optional<T>(ts);
    }
}
