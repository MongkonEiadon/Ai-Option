using System.Threading.Tasks;

namespace AiOption.Domain.Customers.DomainServices {

    public interface IAuthorizationDomainService
    {
        Task<CustomerState> SigninWithPasswordAsync(string email, string password);
        Task<CustomerState> RegisterCustomerAsync(CustomerState newCustomer);
    }

}