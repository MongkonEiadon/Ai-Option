using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Command;
using EventFlow.Queries;
using EventFlow.ReadStores;

namespace AiOption.Query.Account
{
    public class GetAccountByEmailAddressQuery : IQuery<Domain.Account.Account>
    {
        public User User { get; }
        public bool ThrowIfNotFound { get; }

        public GetAccountByEmailAddressQuery(User user, bool throwIfNotFound = true)
        {
            User = user;
            ThrowIfNotFound = throwIfNotFound;
        }
    }
}
