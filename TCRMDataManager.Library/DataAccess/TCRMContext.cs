using System.Configuration;
using System.Data.Entity;
using TCRMDataManager.Library.Models;

namespace TCRMDataManager.Library.DataAccess
{
    class TCRMContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<TaxCategory> TaxCategories { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<Inventory> Inventories { get; set; }

        public TCRMContext(string name) : base(ConfigurationManager.ConnectionStrings[name].ConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<Product>()
                .ToTable("Product")
                .HasRequired(p => p.Tax)
                .WithMany()
                .Map(m => m.MapKey("Tax"));

            modelBuilder.Entity<TaxCategory>().ToTable("TaxCategory");

            modelBuilder.Entity<Sale>()
                .ToTable("Sale")
                .HasRequired(s => s.User)
                .WithMany()
                .Map(m => m.MapKey("CashierId"));

            modelBuilder.Entity<SaleDetail>()
                .ToTable("SaleDetail")
                .HasRequired(sd => sd.Sale)
                .WithMany(s => s.SaleDetails)
                .Map(m => m.MapKey("SaleId"));

            modelBuilder.Entity<SaleDetail>()
                .HasRequired(sd => sd.Product)
                .WithMany()
                .Map(m => m.MapKey("ProductId"));

            modelBuilder.Entity<Inventory>().ToTable("Inventory");
        }
    }
}
