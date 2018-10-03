using AiOption.Domain.IqAccounts;
using EventFlow.Queries;

namespace AiOption.Query.IqAccounts
{
    public class QueryIqAccountByEmailAddress : IQuery<IqAccount>
    {
        public string EmailAddress { get; }
        public QueryIqAccountByEmailAddress(string emailAddress)
        {
            EmailAddress = emailAddress;
        }
    }
}