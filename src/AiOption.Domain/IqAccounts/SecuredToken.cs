using System;

namespace AiOption.Domain.IqAccounts {

    public class SecuredToken {

        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime GeneratedDateTime { get; set; }

    }

}