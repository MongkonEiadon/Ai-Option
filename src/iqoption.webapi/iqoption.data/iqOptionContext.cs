using iqoption.data.Configurations;
using iqoption.data.Model;
using Microsoft.EntityFrameworkCore;

namespace iqoption.data
{
    public class iqOptionContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public iqOptionContext(DbContextOptions<iqOptionContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Person>().HasKey(c => c.Id);
            
        }
    }


}