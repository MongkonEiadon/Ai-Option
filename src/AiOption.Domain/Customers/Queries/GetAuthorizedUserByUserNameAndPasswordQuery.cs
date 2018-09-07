using System;
using System.Collections.Generic;
using System.Text;

using EventFlow.Queries;

namespace AiOption.Domain.Customers.Queries
{
    public class GetAuthorizeCustomerQuery : IQuery<AuthorizedCustomer> {

        public string EmailAddress { get; }

        public GetAuthorizeCustomerQuery(string emailAddress) {
            EmailAddress = emailAddress;

        }
    }
}
