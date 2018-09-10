using System.Threading.Tasks;

namespace AiOption.Domain.Customers.DomainServices {

    public interface ICustomerAuthorizeDomainService
    {
        Task<Customer> SigninWithPasswordAsync(string email, string password);
        Task<Customer> RegisterCustomerAsync(NewCustomer newCustomer);

    }

}