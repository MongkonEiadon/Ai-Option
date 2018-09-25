using System.Threading.Tasks;

using AiOption.Domain.Customers;

namespace AiOption.Application.Repositories.ReadOnly {

    public interface IReadCustomerRepository {

        Task<CustomerReadModel> GetAuthorizedCustomerAsync(string emailAddress);

    }

}