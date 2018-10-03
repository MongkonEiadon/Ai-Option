using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Customers;
using AiOption.Infrastructure.DataAccess;
using AiOption.Query.Customers;
using EventFlow.EntityFramework;
using EventFlow.Exceptions;
using EventFlow.Queries;
using Microsoft.EntityFrameworkCore;

namespace AiOption.Infrastructure.ReadStores.QueryHandlers.Customers
{
    class QueryCustomerByEmailAddressQueryHandler : IQueryHandler<QueryCustomerByEmailAddress, Customer>
    {
        private readonly IDbContextProvider<AiOptionDbContext> _dbContextProvider;

        public QueryCustomerByEmailAddressQueryHandler(IDbContextProvider<AiOptionDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public async Task<Customer> ExecuteQueryAsync(QueryCustomerByEmailAddress query, CancellationToken cancellationToken)
        {
            using (var db = _dbContextProvider.CreateContext())
            {
                var entity = (await db.Customers
                    .FirstOrDefaultAsync(x => x.EmailAddressNormalize == query.User.Value,
                        cancellationToken: cancellationToken));

                if (query.ThrowIfNotFound && entity == null) {
                    throw DomainError.With($"{query.User} not found!");
                }

                return entity?.ToCustomer();
            }
        }
    }
}
