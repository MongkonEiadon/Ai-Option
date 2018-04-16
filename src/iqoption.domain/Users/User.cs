using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace iqoption.domain.Users {
    public class User {

        [Required]
        [JsonProperty("user-id")]
        public string UserId { get; set; } 

        [Required]
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}