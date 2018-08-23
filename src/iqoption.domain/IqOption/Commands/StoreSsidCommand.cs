using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace iqoption.domain.IqOption.Commands {

    public class StoreSsidCommand : Command<IqAggregate, IqIdentity, CommandResult> {
        public string Ssid { get; }
        public string EmailAddress { get; set; }
        

        public StoreSsidCommand(IqIdentity aggregateId, string email, string ssid) : base(aggregateId)
        {
            EmailAddress = email;
            Ssid = ssid;
        }
    }
}