using System;

namespace AiOption.Domain {

    public class DomainException : Exception {

        public DomainException(string businessMessage) {
            BusinessMessage = businessMessage;
        }

        public string BusinessMessage { get; }

    }

}