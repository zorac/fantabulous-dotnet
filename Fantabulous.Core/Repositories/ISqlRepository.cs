using System.Threading.Tasks;

namespace Fantabulous.Core.Repositories
{
    public interface ISqlRepository
    {
        Task<ISqlDb> GetDatabaseAsync();
    }
}
