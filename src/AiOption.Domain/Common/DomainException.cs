using System;

namespace AiOption.Domain.Common {

    public class DomainException : Exception {

        public DomainException(string businessMessage) {
            BusinessMessage = businessMessage;
        }

        public string BusinessMessage { get; }

    }

}