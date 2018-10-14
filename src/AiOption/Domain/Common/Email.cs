using EventFlow.Extensions;
using EventFlow.ValueObjects;

namespace AiOption.Domain.Common
{
    /// <summary>
    /// The email describe for <see cref="Email"/> purpose
    /// </summary>
    public class Email : SingleValueObject<string>
    {
        public string EmailAddress { get; }

        public Email(string emailAddress) : base(emailAddress)
        {
            EmailAddress = emailAddress;

            Specs
                .IsValidEmail
                .ThrowDomainErrorIfNotSatisfied(this);
        }

        public static Email New(string emailAddress) => new Email(emailAddress);
    }
}