using System;
using System.Data;
using EventFlow.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace AiOption.Infrasturcture.ReadStores
{
    public class MsSqlDbContextProvider : IDbContextProvider<AiOptionDbContext>, IDisposable
    {
        private readonly DbContextOptions<AiOptionDbContext> _options;

        public MsSqlDbContextProvider(IDbConnection msSqlConnectionString)
        {
            _options = new DbContextOptionsBuilder<AiOptionDbContext>()
                .UseSqlServer(msSqlConnectionString.ConnectionString)
                .Options;
        }

        public AiOptionDbContext CreateContext()
        {
            var context = new AiOptionDbContext(_options);
            context.Database.EnsureCreated();
            return context;
        }

        public void Dispose()
        {
        }
    }
}