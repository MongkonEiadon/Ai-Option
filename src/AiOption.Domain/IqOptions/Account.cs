using System;

using AiOption.Domain.Customers;

namespace AiOption.Domain.Accounts {

    public class Account : IEquatable<Account> {

        public int UserId { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public CustomerLevel Level { get; set; }

        public string SecuredToken { get; set; }

        public bool IsActive { get; set; }

        public bool Equals(Account other) {
            return other.UserId == UserId;
        }

    }

}