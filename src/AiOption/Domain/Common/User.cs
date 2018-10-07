using System;
using System.Linq;
using EventFlow.ValueObjects;

namespace AiOption.Domain.Common
{
    public class User : SingleValueObject<string>
    {
        public User(string value) : base(ValidateAndConvert(value))
        {
        }

        public override string ToString()
        {
            return string.Join(" ", Value
                .Split(' ')
                .Select(token => char.ToUpper(token[0]) + token.Substring(1)));
        }

        private static string ValidateAndConvert(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));

            return value.ToUpper();
        }

        public static User New(string value)
        {
            return new User(value);
        }
    }
}