using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq.Expressions;
using DesafioStoneTemperatura.Domain.Models.Data;
using System.Data.Entity.Infrastructure.Annotations;

namespace DesafioStoneTemperatura.Data
{
    public class DataContext : DbContext
    {
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Temperature> Temperatures { get; set; }

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
            modelBuilder.Entity<City>()
                .Property(c => c.Name).HasColumnType("VARCHAR").HasMaxLength(250);

            modelBuilder
                .Entity<City>()
                .Property(c => c.Name)
                .HasColumnAnnotation(
                    "Index",
                    new IndexAnnotation(new IndexAttribute("IX_Name") { IsUnique = true }));

            base.OnModelCreating(modelBuilder);
        }
    }
}