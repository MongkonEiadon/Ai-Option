using System;
using System.Text;
using iqoption.data.Configurations;
using iqoption.data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace iqoption.data
{
    public class AiOptionContext : IdentityDbContext<UserDto>
    {
        public DbSet<IqOptionAccountDto> IqOptionUsers { get; set; }

        public AiOptionContext(DbContextOptions<AiOptionContext> options): base(options)
        {
        }


      
        protected override void OnModelCreating(ModelBuilder builder) {

            //builder.Entity<UserDto>().HasData(new UserDto() {
            //    Id = "1A200000-0000-0000-0000-F10A50111111",
            //    UserName = "m@email.com",
            //    Email = "m@email.com",
            //    PasswordHash = "AN6/sSV3Rm6I8LsvAGXNo5YpJLbowHnVw9OhVk5WDojp9Vc+nc9ZDbwSusJfUMf+Yg=="
            //});

            base.OnModelCreating(builder);
        }
    }
    
}