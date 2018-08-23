using System;
using System.Collections.Generic;
using System.Text;

namespace AiOption.Domain.Accounts.Results
{
    public class LoginCommandResult : BaseResult
    {
        public string Ssid { get; }

        public LoginCommandResult(bool isSuccess, string ssid) : base(isSuccess) {
            Ssid = ssid;
        }

    }
}
