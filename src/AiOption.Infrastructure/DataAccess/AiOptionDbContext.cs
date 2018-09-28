using System;
using AiOption.Infrastructure.ReadStores.ReadModels;
using EventFlow.EntityFramework.EventStores;
using EventFlow.EntityFramework.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.Extensions.Configuration;

namespace AiOption.Infrastructure.DataAccess {

    public class AiOptionDbContext : IdentityDbContext<CustomerDto, CustomerLevelDto, Guid> {

        private readonly DbContextOptions<AiOptionDbContext> _options;

        public AiOptionDbContext() {
        }

        public AiOptionDbContext(DbContextOptions<AiOptionDbContext> options) : base(options) {
            _options = options;
        }

        public DbSet<IqAccountDto> IqAccounts { get; set; }
        public DbSet<CustomerDto> Customers { get; set; }
        public DbSet<CustomerLevelDto> CustomerLevels { get; set; }

        //read models
        public DbSet<AccountReadModel> AccountReadModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            if (_options == null) {
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

            base.OnModelCreating(modelBuilder);

        }

    }

}