using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.Models;

namespace Fantabulous.Core.Repositories
{
    public interface IIdCache<T> where T: HasId
    {
        Task<T> GetAsync(long id);
        Task<T[]> GetAsync(IEnumerable<long> ids);
        Task<string> GetJsonAsync(long id);
        Task<string[]> GetJsonAsync(IEnumerable<long> ids);
        void SetInBackground(long id, string json);
        string SetInBackground(T value);
    }
}
