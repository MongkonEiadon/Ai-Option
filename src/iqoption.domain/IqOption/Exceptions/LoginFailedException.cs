using System;

namespace iqoption.domain.IqOption.Exceptions {
    public class LoginFailedException : Exception {
        public LoginFailedException(string message) : base(message) {
        }
    }
}