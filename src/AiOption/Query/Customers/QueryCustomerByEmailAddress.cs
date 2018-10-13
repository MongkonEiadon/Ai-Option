using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using EventFlow.Queries;

namespace AiOption.Query.Customers
{
    public class QueryCustomerByEmailAddress : IQuery<Customer>
    {
        public QueryCustomerByEmailAddress(Email emailAddress, bool throwIfNotFound = true)
        {
            EmailAddress = emailAddress;
            ThrowIfNotFound = throwIfNotFound;
        }

        public Email EmailAddress { get; }
        public bool ThrowIfNotFound { get; }
    }
}