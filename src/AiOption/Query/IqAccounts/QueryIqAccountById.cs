using AiOption.Domain.IqAccounts;
using EventFlow.Queries;

namespace AiOption.Query.IqAccounts
{
    public class QueryIqAccountById : IQuery<IqAccount>
    {
        public QueryIqAccountById(IqAccountId accountId)
        {
            AccountId = accountId;
        }

        public IqAccountId AccountId { get; }
    }
}