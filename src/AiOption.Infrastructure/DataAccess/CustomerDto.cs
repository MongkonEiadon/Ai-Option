using System;

using Microsoft.AspNetCore.Identity;

namespace AiOption.Infrastructure.DataAccess {


    public class CustomerDto : IdentityUser<Guid> {

        public string InviationCode { get; set; }
    }

}