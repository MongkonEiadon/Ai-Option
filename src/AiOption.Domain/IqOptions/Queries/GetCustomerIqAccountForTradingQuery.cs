using System;
using System.Collections.Generic;
using System.Text;

using EventFlow.Queries;

namespace AiOption.Domain.Accounts.Queries
{
    public class GetCustomerAccountsForTradingQuery : IQuery<IEnumerable<Account>> { }
}
