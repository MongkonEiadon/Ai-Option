using System;

using Newtonsoft.Json;

namespace AiOption.Domain.Customers {

    public class Customer {

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("email")]
        public string EmailAddress { get; set; }
    }


    public class AuthorizedCustomer : Customer {

        [JsonProperty("token")]
        public string Token { get; set; }

    }

}