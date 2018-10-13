using AiOption.Domain.Common;
using EventFlow.Entities;

namespace AiOption.Domain.Customers
{
    public enum CustomerStatus
    {
        Undefined = 0,
        RequestedRegister = 1,
        RegisterSucceeded = 2,


        RegisterFailed = -1
    }

    public class Customer : Entity<CustomerId>
    {
        public Customer(CustomerId id, Email userName, Password password) : base(id)
        {
            UserName = userName;
            Password = password;
        }

        public Email UserName { get; }
        public Password Password { get; }
        public Level Level { get; }
        public Token Token { get; }
    }
}