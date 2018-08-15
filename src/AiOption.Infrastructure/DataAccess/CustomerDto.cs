using Microsoft.AspNetCore.Identity;

namespace AiOption.Infrastructure.DataAccess {

    public class CustomerDto : IdentityUser {

        public string InviationCode { get; set; }
    }

}