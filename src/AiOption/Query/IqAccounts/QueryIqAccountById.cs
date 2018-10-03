using AiOption.Domain.IqAccounts;
using EventFlow.Queries;

namespace AiOption.Query.IqAccounts
{
    public class QueryIqAccountById : IQuery<IqAccount>
    {
        public IqAccountId AccountId { get; }

        public QueryIqAccountById(IqAccountId accountId)
        {
            AccountId = accountId;
        }
    }
}
