using System;
using System.Data.Common;
using System.Threading.Tasks;

using Fantabulous.Core.DataAccess;

namespace Fantabulous.Core.Repositories
{
    public interface ISqlDb : IDisposable
    {
        IUserDao Users { get; }

        Task BeginAsync();

        DbCommand Command(string sql);

        Task CommitAsync();

        Task RollbackAsync();
    }
}
