using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPWA.Core
{
    public class NavModel
    {
        public IReadOnlyList<string> Tokens { get; set; }
        public string DisplayName => Tokens.Last();
        public string Url => string.Join("/", Tokens);
        public IEnumerable<string> Deep => Tokens.Take(Tokens.Count - 1);
    }
}
