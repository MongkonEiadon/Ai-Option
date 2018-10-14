using System.Collections.Generic;
using System.Linq;
using AiOption.Domain.Common;
using EventFlow.Aggregates;
using EventFlow.Provided.Specifications;
using EventFlow.Specifications;
using FluentValidation;

namespace AiOption
{
    public partial class Specs
    {
        public static ISpecification<Email> IsValidEmail { get; } = new EmailValidSpecification();
        public static ISpecification<IAggregateRoot> Exists { get; } = new AggregateIsCreatedSpecification();
        public static ISpecification<IAggregateRoot> IsNew { get; } = new AggregateIsNewSpecification();

        private class AggregateIsCreatedSpecification : Specification<IAggregateRoot>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(IAggregateRoot obj)
            {
                if (obj.IsNew) yield return $"Aggregate '{obj.Name}' with ID '{obj.GetIdentity()}' is new";
            }
        }

        private class EmailValidSpecification : ISpecification<Email>
        {
            private readonly EmailValidation _validator = new EmailValidation();

            public bool IsSatisfiedBy(Email obj)
            {
                return _validator.Validate(obj).IsValid;
            }

            public IEnumerable<string> WhyIsNotSatisfiedBy(Email obj)
            {
                return _validator.Validate(obj).Errors.Select(x => x.ErrorMessage);
            }

            private class EmailValidation : FluentValidation.AbstractValidator<Email>
            {
                public EmailValidation()
                {
                    RuleFor(x => x.EmailAddress).NotNull();
                    RuleFor(x => x.EmailAddress).NotEmpty();
                    RuleFor(x => x.EmailAddress).EmailAddress().WithMessage((email => $"{email.EmailAddress} invalid format"));
                }
            }
        }

    }
}