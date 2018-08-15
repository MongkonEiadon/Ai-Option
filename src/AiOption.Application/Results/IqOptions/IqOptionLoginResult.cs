namespace AiOption.Application.Results.IqOptions {
    public class IqOptionLoginResult {

        public string EmailAddress { get; }
        public string SecuredToken { get; }

        public IqOptionLoginResult(string emailAddress, string securedToken) {
            EmailAddress = emailAddress;
            SecuredToken = securedToken;
        }
    }
}
