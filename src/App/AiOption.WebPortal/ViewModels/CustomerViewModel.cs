using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AiOption.WebPortal.ViewModels
{
    public class CustomerViewModel
    {
        [JsonProperty("customerId")]
        public string CustomerId { get; set; }
        
        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("invitationCode")]
        public string InvitationCode { get; set; }
    }
}
