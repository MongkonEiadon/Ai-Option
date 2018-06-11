using System;
using iqoption.data.Configurations;
using iqoption.data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace iqoption.data
{
    public class iqOptionContext : IdentityDbContext<UserDto>
    {

        public DbSet<IqOptionUserDto> IqOptionUsers { get; set; }

        public iqOptionContext(DbContextOptions<iqOptionContext> options): base(options)
        {
            
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder) {

            builder.Entity<UserDto>().HasData(new UserDto() {
                Id = "1A200000-0000-0000-0000-F10A50111111",
                UserName = "m@email.com",
                Email = "m@email.com",
                PasswordHash = "AN6/sSV3Rm6I8LsvAGXNo5YpJLbowHnVw9OhVk5WDojp9Vc+nc9ZDbwSusJfUMf+Yg=="
            });

            base.OnModelCreating(builder);
        }
    }


}