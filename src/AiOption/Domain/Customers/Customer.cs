using AiOption.Domain.Common;
using EventFlow.Entities;

namespace AiOption.Domain.Customers
{
    public enum CustomerStatus
    {
        Undefined,
        Register,
        RegisterSucceeded,
        RegisterFailed,
        AddIqAccount,
        ChangeLevel,
        LoggedIn,
        Deleted
    }

    public class Customer : Entity<CustomerId>
    {
        public Customer(CustomerId id, Email userName, Password password, Level level, Token token) : base(id)
        {
            UserName = userName;
            Password = password;
            Level = level;
            Token = token;
        }

        public Email UserName { get; }
        public Password Password { get; }
        public Level Level { get; }
        public Token Token { get; }

        public string InvitationCode { get; set; }
    }
}