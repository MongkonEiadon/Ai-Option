using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace iqoption.domain.IqOption.Commands {

    public class StoreSsidCommand : Command<IqOptionAggregate, IqOptionIdentity, StoreSsidResult> {
        public string Ssid { get; }
        public string EmailAddress { get; set; }
        

        public StoreSsidCommand(IqOptionIdentity aggregateId, string email, string ssid) : base(aggregateId)
        {
            EmailAddress = email;
            Ssid = ssid;
        }
    }

    public class StoreSsidResult : IExecutionResult {
        public bool IsSuccess { get; }

        public StoreSsidResult(bool isSuccess) {
            IsSuccess = isSuccess;
        }
    }
}