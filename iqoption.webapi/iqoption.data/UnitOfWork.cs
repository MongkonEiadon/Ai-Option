using System;
using System.Threading;
using System.Threading.Tasks;
using iqoption.core.data;
using iqoption.data.Model;
using Microsoft.EntityFrameworkCore;

namespace iqoption.data
{
    public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        private readonly iqOptionContext _dbContext;

        public UnitOfWork(iqOptionContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync(CancellationToken token = default(CancellationToken))
        {
            return _dbContext.SaveChangesAsync(token);
        }
    }
}