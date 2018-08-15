using System;
using System.Collections.Generic;
using System.Text;

using AiOption.Domain.Customer;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AiOption.Infrastructure.DataAccess
{
    public class AiContext : IdentityDbContext<CustomerDto>
    {

        public AiContext(DbContextOptions options) : base(options) {

        }

        public DbSet<IqAccountDto> IqAccounts { get; set; }
        public DbSet<CustomerDto> Customers { get; set; }
    }

}
