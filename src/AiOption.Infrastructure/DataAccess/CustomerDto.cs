using System;

using AiOption.Domain.Common;

using Microsoft.AspNetCore.Identity;

namespace AiOption.Infrastructure.DataAccess {


    public class CustomerDto : IdentityUser<Guid>, IDbEntity<Guid> {

        public string InviationCode { get; set; }

    }

}