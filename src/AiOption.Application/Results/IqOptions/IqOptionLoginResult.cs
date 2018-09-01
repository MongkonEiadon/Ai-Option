namespace AiOption.Application.Results.IqOptions {

    public class IqOptionLoginResult {

        public IqOptionLoginResult(string emailAddress, string securedToken) {
            EmailAddress = emailAddress;
            SecuredToken = securedToken;
        }

        public string EmailAddress { get; }
        public string SecuredToken { get; }

    }

}