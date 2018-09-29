﻿using System;
using AiOption.Domain.Common;
using AiOption.Infrastructure.ReadStores.ReadModels;
using EventFlow.EntityFramework.EventStores;
using EventFlow.EntityFramework.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.Extensions.Configuration;

namespace AiOption.Infrastructure.DataAccess {

    public class AiOptionDbContext : DbContext {

        private readonly DbContextOptions<AiOptionDbContext> _options;

        public AiOptionDbContext() {
        }

        public AiOptionDbContext(DbContextOptions<AiOptionDbContext> options) : base(options) {
            _options = options;
        }

        //read models
        public DbSet<CustomerReadModel> Customers { get; set; }
        public DbSet<IqAccountReadModel> IqAccounts { get; set; }

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

            modelBuilder.Entity<CustomerReadModel>()
                .Property(e => e.Password)
                .HasConversion(v => v.Value.Decrypt("AiOption"), v => Password.With(v));
            modelBuilder.Entity<CustomerReadModel>()
                .Property(e => e.UserName)
                .HasConversion(v => v.Value, v => new User(v));

            modelBuilder.Entity<IqAccountReadModel>()
                .Property(e => e.UserName)
                .HasConversion(v => v.Value, v => new User(v));
            modelBuilder.Entity<IqAccountReadModel>()
                .Property(e => e.Password)
                .HasConversion(v => v.Value, v => Password.With(v));


            base.OnModelCreating(modelBuilder);

        }

    }

}