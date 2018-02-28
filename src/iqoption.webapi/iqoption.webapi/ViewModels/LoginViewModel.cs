using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace iqoption.webapi.ViewModels
{

    public class LoginViewModel
    {
        [JsonProperty(propertyName: "email")]
        public string Email { get; set; }

        [JsonProperty(propertyName: "password")]
        public string Password { get; set; }
    }
}
