﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq.Expressions;
using DesafioStoneTemperatura.Domain.Models;

namespace DesafioStoneTemperatura.Data
{
    public class DataContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Temperature> Temperatures { get; set; }

        public DataContext() : base("DefaultConnection")
        {
        }


        public static DataContext Create()
        {
            return new DataContext("DefaultConnection");
        }

        public DataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Temperature>()
                .HasKey(c => new { c.Id, c.CityId });

            modelBuilder.Entity<Temperature>()
                .Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<City>().HasMany(p => p.Temperatures)
                .WithRequired()
                .HasForeignKey(c => c.CityId)
                .WillCascadeOnDelete();

            base.OnModelCreating(modelBuilder);
        }
    }
}