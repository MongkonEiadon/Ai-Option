using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace iqoption.domain.IqOption
{
    public class IqHttpResult<T> 
    {
        [JsonProperty("isSuccessful")]
        public bool IsSuccessful { get; set; }

        [JsonProperty("message")]
        public object Message { get; set; }

        [JsonProperty("result")]
        public T Result { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

    }

    public class LoginFailedResultMessage
    {
        [JsonProperty("isSuccessful")]
        public bool IsSuccessful { get; set; }

        [JsonProperty("message")]
        public LoginFailedMessage Message { get; set; }

        [JsonProperty("result")]
        public string[] Result { get; set; }

    }


    public class LoginFailedMessage
    {
        [JsonProperty("email")]
        public string[] Email { get; set; }

        [JsonProperty("password")]
        public string[] Password { get; set; }
    }


    public class LoginTooMuchResultMessage 
    {

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("ttl")]
        public int Ttl { get; set; }

    }
}
