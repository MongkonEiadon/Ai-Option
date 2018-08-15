using System;

namespace AiOption.Domain {

    public class DomainException : Exception {

        public string BusinessMessage { get;  }
        public DomainException(string businessMessage) {
            BusinessMessage = businessMessage;
        }

    }

}