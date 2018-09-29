using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Accounts;
using AiOption.Domain.Customers;
using AiOption.Infrastructure.DataAccess;
using AiOption.Infrastructure.ReadStores.ReadModels;
using AiOption.Query.Customers;
using EventFlow.EntityFramework;
using EventFlow.Queries;
using EventFlow.ReadStores;
using Microsoft.EntityFrameworkCore;

namespace AiOption.Infrastructure.ReadStores.QueryHandlers
{
    class GetAccountByNameQueryHandler : IQueryHandler<GetCustomerByEmailAddressQuery, Customer>
    {
        private readonly IDbContextProvider<AiOptionDbContext> _dbContextProvider;

        public GetAccountByNameQueryHandler(IDbContextProvider<AiOptionDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public async Task<Customer> ExecuteQueryAsync(GetCustomerByEmailAddressQuery query, CancellationToken cancellationToken)
        {
            using (var db = _dbContextProvider.CreateContext())
            {
                var entity = (await db.Customers
                    .FirstOrDefaultAsync(x => x.EmailAddressNormalize == query.User.Value,
                        cancellationToken: cancellationToken));


                return entity?.ToAccount();
            }
        }
    }
}
