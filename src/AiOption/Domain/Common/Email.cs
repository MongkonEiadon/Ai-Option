using System;
using System.Collections;
using System.Collections.Generic;
using EventFlow.Extensions;
using EventFlow.ValueObjects;

namespace AiOption.Domain.Common
{
    /// <summary>
    ///     The email describe for <see cref="Email" /> purpose
    /// </summary>
    public class Email : SingleValueObject<string>, IEquatable<Email>
    {
        public Email(string emailAddress) : base(emailAddress)
        {
            EmailAddress = emailAddress;

            Specs
                .IsValidEmail
                .ThrowDomainErrorIfNotSatisfied(this);
        }

        public string EmailAddress { get; }

        public static Email New(string emailAddress) => new Email(emailAddress);


        public bool Equals(Email other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) || string.Equals(EmailAddress, other.EmailAddress, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Email) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (EmailAddress != null ? EmailAddress.GetHashCode() : 0);
            }
        }
    }
}