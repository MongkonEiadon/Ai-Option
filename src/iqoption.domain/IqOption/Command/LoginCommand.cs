using iqoption.core;
using Newtonsoft.Json;

namespace iqoption.domain.IqOption.Command {
    public class LoginCommand : ICommand<LoginCommandResult> {
        public LoginCommand(string email, string password) {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }

    public class LoginCommandResult {
        public LoginCommandResult(string ssid, bool isSuccess, string message) {
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