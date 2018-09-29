using AiOption.Domain.Accounts;
using AiOption.Domain.Common;
using EventFlow.Entities;

namespace AiOption.Domain.Customers
{
    public enum CustomerStatus
    {
        Undefined = 0,
        RequestedRegister = 1,
        RegisterSucceeded = 2,


        RegisterFailed = -1,
    }

    public class Customer : Entity<CustomerId>
    {
        public User UserName { get; }
        public Password Password { get; }
        public Level Level { get; }

        public Customer(CustomerId id, User userName, Password password) : base(id) {
            UserName = userName;
            Password = password;
        }
    }
}