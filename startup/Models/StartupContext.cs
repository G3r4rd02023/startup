﻿using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace startup.Models
{
    public class StartupContext : DbContext
    {
        public StartupContext() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }
    }
}