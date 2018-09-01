using System;
using System.ComponentModel;

using AiOption.Domain.Customers;

using Microsoft.AspNetCore.Identity;

namespace AiOption.Infrastructure.DataAccess {

    public class CustomerLevelDto : IdentityRole<Guid> {

        [DefaultValue(CustomerLevel.None)]
        public CustomerLevel Level { get; set; }

        public string LevelName { get; set; }

    }

}