using System;
using System.Collections.Generic;
using System.Text;

using EventFlow.Queries;

namespace AiOption.Domain.IqOption.Queries
{
    public class GetCustomerAccountsForTradingQuery : IQuery<IEnumerable<Account>> { }
}
