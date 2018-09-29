using System;
using System.Collections.Generic;
using EventFlow.Specifications;

namespace AiOption.Domain.Account
{
    public static class AccountSpecs
    {
        public static readonly ISpecification<AccountAggregate> NotCorrectEmailAddress = new AusgeglichenSpec();
        //public static readonly ISpecification<AccountAggregate> NichtAufgeloest = new NichtAufgeloestSpec();

        private class AusgeglichenSpec : Specification<AccountAggregate>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(AccountAggregate obj)
            {
                if (!String.IsNullOrWhiteSpace(obj.EmailAddress))
                {
                    yield return $"EmailAddress {obj.EmailAddress} isn't correct.";
                }
            }
        }

        //private class NichtAufgeloestSpec : Specification<AccountAggregate>
        //{
        //    protected override IEnumerable<string> IsNotSatisfiedBecause(AccountAggregate obj)
        //    {
        //        if (obj.IstAufgeloest)
        //        {
        //            yield return "Konto ist aufgel�st.";
        //        }
        //    }
        //}
    }
}