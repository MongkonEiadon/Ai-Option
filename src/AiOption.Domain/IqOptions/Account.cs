using AiOption.Domain.Customers;

namespace AiOption.Domain.Accounts {

    public class Account {

        public int UserId { get; set; }

        public int EmailAddress { get; set; }

        public string Password { get; set; }

        public CustomerLevel Level { get; set; }

        public SecuredToken SecuredToken { get; set; }

        public bool IsActive { get; set; }
    }

}