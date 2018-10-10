using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Customers;
using AiOption.Infrasturcture.ReadStores.ReadModels;
using AiOption.Query;
using AiOption.Query.Customers;
using EventFlow.EntityFramework;
using EventFlow.Exceptions;
using EventFlow.Queries;
using Microsoft.EntityFrameworkCore;

namespace AiOption.Infrasturcture.ReadStores.QueryHandlers.Customers
{
    internal class QueryCustomerByEmailAddressQueryHandler : IQueryHandler<QueryCustomerByEmailAddress, Customer>
    {
        private readonly ISearchableReadModelStore<CustomerReadModelDto> _readStore;
        private readonly IDbContextProvider<AiOptionDbContext> _dbContextProvider;

        public QueryCustomerByEmailAddressQueryHandler(ISearchableReadModelStore<CustomerReadModelDto> readStore)
        {
            _readStore = readStore;
        }

        public async Task<Customer> ExecuteQueryAsync(QueryCustomerByEmailAddress query,
            CancellationToken cancellationToken)
        {
            using (var db = _dbContextProvider.CreateContext())
            {
                var entity = await db.Customers
                    .FirstOrDefaultAsync(x => x.EmailAddressNormalize == query.User.Value,
                        cancellationToken);

                if (query.ThrowIfNotFound && entity == null) throw DomainError.With($"{query.User} not found!");

                return entity?.ToCustomer();
            }
        }
    }
}