using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Account;
using AiOption.Infrastructure.DataAccess;
using AiOption.Infrastructure.ReadStores.ReadModels;
using AiOption.Query.Account;
using EventFlow.EntityFramework;
using EventFlow.Queries;
using EventFlow.ReadStores;
using Microsoft.EntityFrameworkCore;

namespace AiOption.Infrastructure.ReadStores.QueryHandlers
{
    class GetAccountByNameQueryHandler : IQueryHandler<GetAccountByEmailAddressQuery, Account>
    {
        private readonly IDbContextProvider<AiOptionDbContext> _dbContextProvider;

        public GetAccountByNameQueryHandler(IDbContextProvider<AiOptionDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public async Task<Account> ExecuteQueryAsync(GetAccountByEmailAddressQuery query, CancellationToken cancellationToken)
        {
            using (var db = _dbContextProvider.CreateContext())
            {
                var entity = (await db.AccountReadModels
                        .FirstOrDefaultAsync( x => x.EmailAddressNormalize == query.User.Value, cancellationToken: cancellationToken))
                    .ToAccount();

                return entity;
            }
        }
    }
}
