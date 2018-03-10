using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace iqoption.web.Models
{
    public class LoginViewModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Id { get; set; }
    }
}