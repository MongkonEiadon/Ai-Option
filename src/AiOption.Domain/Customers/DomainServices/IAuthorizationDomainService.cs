using System.Threading.Tasks;

namespace AiOption.Domain.Customers.DomainServices {

    public interface IAuthorizationDomainService {

        Task<CustomerReadModel> SigninWithPasswordAsync(string email, string password);
        Task<CustomerReadModel> RegisterCustomerAsync(CustomerReadModel newCustomer);

    }

}