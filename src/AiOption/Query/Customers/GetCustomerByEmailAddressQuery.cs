using AiOption.Domain.Accounts;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using EventFlow.Queries;

namespace AiOption.Query.Customers
{
    public class GetCustomerByEmailAddressQuery : IQuery<Customer>
    {
        public User User { get; }
        public bool ThrowIfNotFound { get; }

        public GetCustomerByEmailAddressQuery(User user, bool throwIfNotFound = true)
        {
            User = user;
            ThrowIfNotFound = throwIfNotFound;
        }
    }
}
