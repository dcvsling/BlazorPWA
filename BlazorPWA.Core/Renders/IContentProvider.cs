using System.Threading.Tasks;

namespace BlazorPWA.Core.Renders
{
    public interface IContentProvider
    {
        Task<string> GetAsync(string type, string path);
    }
}
