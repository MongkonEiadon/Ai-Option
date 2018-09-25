using EventFlow.Queries;

namespace AiOption.Domain.Customers.Queries {

    public class GetAuthorizeCustomerQuery : IQuery<CustomerReadModel> {

        public GetAuthorizeCustomerQuery(string emailAddress) {
            EmailAddress = emailAddress;

        }

        public string EmailAddress { get; }

    }

}