using EventFlow.Core;
using EventFlow.Extensions;

namespace AiOption.Domain.Customers {

    public class CustomerId : Identity<CustomerId> {

        public CustomerId(string value) : base(value) {
        }

    }

}