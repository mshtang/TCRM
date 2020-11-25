﻿using System.Configuration;
using System.Data.Entity;
using TCRMDataManager.Library.Models;

namespace TCRMDataManager.Library.DataAccess
{
    class TCRMContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<TaxCategory> TaxCategories { get; set; }
        public DbSet<SaleDb> Sales { get; set; }
        public DbSet<SaleDetailDb> SaleDetails { get; set; }

        public TCRMContext(string name) : base(ConfigurationManager.ConnectionStrings[name].ConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<Product>().ToTable("Product")
                                          .HasRequired(p => p.Tax)
                                          .WithMany()
                                          .Map(m => m.MapKey("Tax"));

            modelBuilder.Entity<TaxCategory>().ToTable("TaxCategory");

            modelBuilder.Entity<SaleDb>().ToTable("Sale");

            modelBuilder.Entity<SaleDetailDb>().ToTable("SaleDetail");
        }
    }
}
