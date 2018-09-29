using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using EventFlow.ValueObjects;

namespace AiOption.Domain.Common
{
    public class User : SingleValueObject<string>
    {
        public User(string value) : base(ValidateAndConvert(value)) { }

        public override string ToString()
        {
            return string.Join(" ", this.Value
                .Split(' ')
                .Select(token => char.ToUpper(token[0]) + token.Substring(1)));
        }

        private static string ValidateAndConvert(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));

            return value.ToUpper();
        }
    }
}
