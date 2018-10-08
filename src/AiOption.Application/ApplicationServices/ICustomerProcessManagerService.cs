using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;

namespace AiOption.Application.ApplicationServices {
    public interface ICustomerProcessManagerService
    {
        Task<Customer> RegisterCustomerAsync(string userName, string password, string invitationCode);

        Task<Customer> ChangeCustomerLevel(CustomerId customerId, Level level);
    }
}