using System.Collections.Generic;
using AiOption.Domain.Customers;
using EventFlow.Specifications;

namespace AiOption.Domain.Accounts
{
    public static class CustomerSpecs
    {
        public static readonly ISpecification<CustomerAggregate> NotCorrectEmailAddress = new AusgeglichenSpec();
        //public static readonly ISpecification<CustomerAggregate> NichtAufgeloest = new NichtAufgeloestSpec();

        private class AusgeglichenSpec : Specification<CustomerAggregate>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(CustomerAggregate obj)
            {
                if (!string.IsNullOrWhiteSpace(obj.EmailAddress))
                    yield return $"UserName {obj.EmailAddress} isn't correct.";
            }
        }

        //private class NichtAufgeloestSpec : Specification<CustomerAggregate>
        //{
        //    protected override IEnumerable<string> IsNotSatisfiedBecause(CustomerAggregate obj)
        //    {
        //        if (obj.IstAufgeloest)
        //        {
        //            yield return "Konto ist aufgel�st.";
        //        }
        //    }
        //}
    }
}