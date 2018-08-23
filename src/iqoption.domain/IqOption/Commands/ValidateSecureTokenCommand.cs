using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace iqoption.domain.IqOption.Commands {

    public class ValidateSecuredTokenCommandResult : IExecutionResult {
        public Profile Profile { get; }
        public bool IsSuccess { get; }

        public ValidateSecuredTokenCommandResult(bool isSuccess, Profile profile) {
            IsSuccess = isSuccess;
            Profile = profile;
        }
    }

    public class ValidateSecureTokenCommand : Command<IqAggregate, IqIdentity, ValidateSecuredTokenCommandResult> {
        public string SecuredToken { get; }

        public ValidateSecureTokenCommand(IqIdentity aggregateId, string securedToken) : base(aggregateId) {
            SecuredToken = securedToken;
        }
        


       
    }
}