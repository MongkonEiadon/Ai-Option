using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace iqoption.domain.Users {
    public class User {

        [JsonProperty("id")]
        public string Id { get; set; }

        [Required]
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("userlevel")]
        public string Level { get; set; }

        [JsonProperty("invitationcode")]
        public string InvitationCode { get; set; }
        
    }
}