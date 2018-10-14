using System;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using AiOption.Domain.IqAccounts;
using EventFlow.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;

namespace AiOption.Infrasturcture.ReadStores
{
    public class AiOptionDbContext : DbContext
    {
        private readonly DbContextOptions<AiOptionDbContext> _options;

        public AiOptionDbContext() { }

        public AiOptionDbContext(DbContextOptions<AiOptionDbContext> options) : base(options)
        {
            _options = options;
        }

        //read models
        public DbSet<CustomerReadModel> Customers { get; set; }
        public DbSet<IqAccountReadModel> IqAccounts { get; set; }

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
            modelBuilder.AddEventFlowEvents().AddEventFlowSnapshots();

            ApplyBuilder<CustomerReadModel>(modelBuilder,
                x => x.ToTable("Customers"),
                x => x.HasKey(y => y.AggregateId),
                x => x.Property(e => e.AggregateId).HasColumnName("Id"),
                x => x.Property(e => e.Password).HasConversion(PasswordConverter),
                x => x.Property(e => e.UserName).HasConversion(v => v.Value, v => new Email(v)),
                x => x.Property(e => e.Level).HasConversion(v => v.Value, v => new Level(v)),
                x => x.Property(e => e.Token).HasConversion(v => v.Value, v => new Token(v)));

            ApplyBuilder<IqAccountReadModel>(modelBuilder,
                x => x.ToTable("IqAccounts"),
                x => x.HasKey(y => y.AggregateId),
                x => x.Property(e => e.AggregateId).HasColumnName("Id"),
                x => x.Property(e => e.UserName).HasConversion(v => v.Value, v => new Email(v)),
                x => x.Property(e => e.Password).HasConversion(PasswordConverter),
                x => x.Property(e => e.CustomerId).HasConversion(e => e.Value, v => CustomerId.With(v)),
                x => x.Property(e => e.Type).HasConversion(new EnumToStringConverter<AccountType>()));

            base.OnModelCreating(modelBuilder);
        }

        private void ApplyBuilder<TReadModel>(ModelBuilder builder, params Action<EntityTypeBuilder<TReadModel>>[] actions)
            where TReadModel : class
        {
            foreach (var action in actions)
            {
                action(builder.Entity<TReadModel>());
            }
        }

        private ValueConverter<Password, string> PasswordConverter =>
            new ValueConverter<Password, string>(
                v => v.DecryptPassword(),
                v => Password.With(v));
    }
}