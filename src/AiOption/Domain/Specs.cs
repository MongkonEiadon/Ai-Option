using System.Collections.Generic;
using System.Text;
using EventFlow.Aggregates;
using EventFlow.Provided.Specifications;
using EventFlow.Specifications;

namespace AiOption.Domain
{
    public class Specs
    {
        public static ISpecification<IAggregateRoot> Exists { get; } = new AggregateIsCreatedSpecification();

        public static ISpecification<IAggregateRoot> IsNew { get; } = new AggregateIsNewSpecification();

        private class AggregateIsCreatedSpecification : Specification<IAggregateRoot> {
            protected override IEnumerable<string> IsNotSatisfiedBecause(IAggregateRoot obj) {
                if (obj.IsNew) {
                    yield return $"Aggregate '{obj.Name}' with ID '{obj.GetIdentity()}' is new";
                }
            }
        }
    }
}
