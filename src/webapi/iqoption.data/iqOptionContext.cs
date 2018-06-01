using System;
using iqoption.data.Configurations;
using iqoption.data.Model;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
        }
    }


}