using System;
using System.Threading;
using System.Threading.Tasks;

namespace iqoption.core.data
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken token = default(CancellationToken));
    }
}