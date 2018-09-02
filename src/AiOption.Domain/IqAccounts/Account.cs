using System;

using AiOption.Domain.Customers;

namespace AiOption.Domain.IqAccounts {

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


    public static class AccountExtensions {

        public static int GetMultipler(this Account This) {
            switch (This.Level) {

                case CustomerLevel.None:return 1;
                case CustomerLevel.Baned: return 0;
                case CustomerLevel.Standard: return 1;
                case CustomerLevel.Silver:return 2;
                case CustomerLevel.Gold:return 3;
                case CustomerLevel.Platinum:return 4;
                case CustomerLevel.Vip:return 5;
                case CustomerLevel.Administrator:return 1;
                case CustomerLevel.Traders:return 1;
                default:throw new ArgumentOutOfRangeException();
            }
        }

    }


}