using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Core;

namespace iqoption.domain.IqOption.Command {


    public class ValidateSecuredTokenCommandResult : IExecutionResult {
        public Profile Profile { get; }
        public bool IsSuccess { get; }

        public ValidateSecuredTokenCommandResult(bool isSuccess, Profile profile) {
            IsSuccess = isSuccess;
            Profile = profile;
        }
    }

    public class ValidateSecureTokenCommand : Command<IqOptionAggregate, IqOptionIdentity, ValidateSecuredTokenCommandResult> {
        public string SecuredToken { get; }

        public ValidateSecureTokenCommand(IqOptionIdentity aggregateId, string securedToken) : base(aggregateId) {
            SecuredToken = securedToken;
        }



       
    }
}