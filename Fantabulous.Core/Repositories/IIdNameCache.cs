using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.Models;

namespace Fantabulous.Core.Repositories
{
    public interface IIdNameCache<T> where T: HasName
    {
        Task<T> GetAsync(long id);
        Task<T> GetAsync(string name);
        Task<T[]> GetAsync(IEnumerable<long> ids);
        Task<string> GetJsonAsync(long id);
        Task<string> GetJsonAsync(string name);
        Task<string[]> GetJsonAsync(IEnumerable<long> ids);
        void SetInBackground(long id, string name, string json);
        string SetInBackground(T value);
    }
}
