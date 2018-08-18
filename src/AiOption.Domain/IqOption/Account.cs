using AiOption.Domain.Customer;

namespace AiOption.Domain.IqOption {

    public class Account {

        public int UserId { get; set; }

        public int EmailAddress { get; set; }

        public string Password { get; set; }

        public CustomerLevel Level { get; set; }

        public SecuredToken SecuredToken { get; set; }
    }

}