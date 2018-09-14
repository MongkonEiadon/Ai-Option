using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using AiOption.Domain.Customers;

namespace AiOption.Application.Repositories.ReadOnly
{
    public interface IReadCustomerRepository {

        Task<CustomerState> GetAuthorizedCustomerAsync(string emailAddress);

    }
}
