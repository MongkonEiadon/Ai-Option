using System;
using System.Text.RegularExpressions;
using AiOption.Domain.Customers;
using EventFlow.Sagas;
using EventFlow.ValueObjects;

namespace AiOption.Domain.Sagas.Terminate
{
    public class TerminateSagaId : ValueObject, ISagaId
    {
        private static readonly Regex Regex =
            new Regex("^saga-(customer-[0-9a-f-]+)$",
                RegexOptions.Compiled);

        public TerminateSagaId(CustomerId customer)
        {
            Customer = customer;
        }

        public TerminateSagaId(string value)
        {
            var match = Regex.Match(value);
            if (!match.Success) throw new ArgumentException("Could not parse id.");

            Customer = new CustomerId(match.Groups[1].Value);
        }

        public CustomerId Customer { get; }

        public string Value => $"saga-{Customer.Value}";
    }
}