using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using AiOption.Domain.Customers;

namespace AiOption.Application.Services
{

    public interface ICustomerAccountServices {

        Task<bool> LoginAsync(Customer customer);

    }
    public class CustomerAccountServices : ICustomerAccountServices
    {

        public Task<bool> LoginAsync(Customer customer) {
            throw new NotImplementedException();
        }

    }
}
