using System.Threading;
using System.Threading.Tasks;

using AiOption.Application.Repositories;
using AiOption.Application.Repositories.ReadOnly;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Queries;

using EventFlow.Queries;

namespace AiOption.Application.QueryHandlers {

    public class CustomerQueryHandlers
        : IQueryHandler<GetAuthorizeCustomerQuery, CustomerState> {

        private readonly IReadCustomerRepository _customerReadRepository;

        public CustomerQueryHandlers(IReadCustomerRepository customerReadRepository) {
            _customerReadRepository = customerReadRepository;

        }


        public Task<CustomerState> ExecuteQueryAsync(GetAuthorizeCustomerQuery query, CancellationToken cancellationToken) {
            return _customerReadRepository.GetAuthorizedCustomerAsync(query.EmailAddress);
        }

    }

}