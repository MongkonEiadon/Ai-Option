using System;
using iqoption.data.Configurations;
using iqoption.data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace iqoption.data
{
    public class iqOptionContext : IdentityDbContext<UserDto>
    {
        public DbSet<UserDto> Users { get; set; }
        public DbSet<FollowerDto> Followers { get; set; }
        public DbSet<TraderDto> Traders { get; set; }
        
        public DbSet<TraderFollwerDto> TraderFollowers { get; set; }

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