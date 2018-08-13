
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Core;
using Newtonsoft.Json;

namespace iqoption.domain.IqOption.Commands {
    public class IqLoginCommand : Command<IqAggregate, IqIdentity, IqLoginCommandResult> {
    

        public IqLoginCommand(IqIdentity identity, string email,  string password): base(identity) {
            Email = email;
            Password = password;
            Id = identity;
        }
        public IqIdentity Id { get; }
        public string Email { get; }
        public string Password { get; }
    }

    public class IqLoginCommandResult : IExecutionResult {
        public IqLoginCommandResult(string ssid, bool isSuccess, string message) {
            Ssid = ssid;
            IsSuccess = isSuccess;
            Message = message;
        }

        public string Ssid { get; }
        public bool IsSuccess { get; }
        public string Message { get; }
    }


    #region Errors

    public class LoginErrorCommandResult {
        [JsonProperty("errors")]
        public Error[] Errors { get; set; }
    }

    public class Error {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }

    #endregion

  
}