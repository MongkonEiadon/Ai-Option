using AiOption.Domain.Common;

namespace AiOption.Domain.IqAccounts.Results {

    public class LoginCommandResult : BaseResult {

        public LoginCommandResult(bool isSuccess, string ssid) : base(isSuccess) {
            Ssid = ssid;
        }

        public string Ssid { get; }

    }

}