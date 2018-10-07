using AiOption.Domain.Common;
using AiOption.Infrasturcture.ReadStores.ReadModels;
using EventFlow.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AiOption.Infrasturcture.ReadStores
{
    public class AiOptionDbContext : DbContext
    {
        private readonly DbContextOptions<AiOptionDbContext> _options;

        public AiOptionDbContext()
        {
        }

        public AiOptionDbContext(DbContextOptions<AiOptionDbContext> options) : base(options)
        {
            _options = options;
        }

        //read models
        public DbSet<CustomerReadModelDto> Customers { get; set; }
        public DbSet<IqAccountReadModelDto> IqAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_options == null)
            {
                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseSqlServer(config.GetConnectionString("aioptiondb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .AddEventFlowEvents()
                .AddEventFlowSnapshots();

            modelBuilder.Entity<CustomerReadModelDto>()
                .Property(e => e.Password)
                .HasConversion(v => v.Value.Decrypt("AiOption"), v => Password.With(v));
            modelBuilder.Entity<CustomerReadModelDto>()
                .Property(e => e.UserName)
                .HasConversion(v => v.Value, v => new User(v));
            modelBuilder.Entity<CustomerReadModelDto>()
                .Property(e => e.Level)
                .HasConversion(v => v.Value, v => new Level(v));
            modelBuilder.Entity<CustomerReadModelDto>()
                .Property(e => e.Token)
                .HasConversion(v => v.Value, v => new Token(v));


            modelBuilder.Entity<IqAccountReadModelDto>()
                .Property(e => e.UserName)
                .HasConversion(v => v.Value, v => new User(v));
            modelBuilder.Entity<IqAccountReadModelDto>()
                .Property(e => e.Password)
                .HasConversion(v => v.Value, v => Password.With(v));


            base.OnModelCreating(modelBuilder);
        }
    }
}