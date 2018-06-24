using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace iqoption.domain.IqOption.Command
{

    public interface ILoginCommand: ICommand {

        string Email { get; }
        string Password { get; }
    }
    public class LoginCommand : ICommand, ILoginCommand
    {
        public string Email { get; }
        public string Password { get; }

        public LoginCommand(string email, string password) {
            Email = email;
            Password = password;
        }
    }


    #region Errors


    public class LoginErrorCommandResult
    {

        [JsonProperty("errors")]
        public Error[] Errors { get; set; }
    }

    public class Error
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }

    #endregion

    public class LoginCommandResult : HttpCommandResult<SsidResult> {
    }

    public class HttpCommandResult<T>
    {
        [JsonProperty("data")]
        public T Result { get; set; }
    }

    public class SsidResult
    {
        [JsonProperty("ssid")]
        public string Ssid { get; set; }
    }
}
