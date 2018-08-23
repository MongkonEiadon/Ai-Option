using System;
using System.Collections.Generic;
using System.Text;
using EventFlow.Aggregates;
using EventFlow.Core;

namespace iqoption.domain.IqOption {

    public class IqAggregate : AggregateRoot<IqAggregate, IqIdentity> {
        public IqAggregate(IqIdentity identity) : base(identity) {
        }

       
    }
}
