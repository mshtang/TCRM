using Microsoft.EntityFrameworkCore;
using System.Configuration;
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

        private readonly string _name;

        public TCRMContext(string name) : base()
        {
            _name = name;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings[_name].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<Product>(m =>
            {
                //m.ToTable("Product");
                m.HasOne(p => p.Tax).WithMany();
                //m.Property(p => p.Tax).HasColumnName("Tax");
            });


            modelBuilder.Entity<TaxCategory>().ToTable("TaxCategory");

            modelBuilder.Entity<Sale>(m =>
            {
                m.ToTable("Sale");
                m.HasOne(s => s.User).WithMany().IsRequired().HasForeignKey("CashierId");
                //m.Property(s => s.User).HasColumnName("CashierId");

                m.HasMany(s => s.SaleDetails)
                 .WithOne(sd => sd.Sale)
                 .IsRequired();
            });

            modelBuilder.Entity<SaleDetail>(m =>
            {
                //m.ToTable()
                m.HasOne(sd => sd.Sale)
                 .WithMany(s => s.SaleDetails)
                 .HasForeignKey("SaleId");
                //m.Property(sd => sd.Sale).HasColumnName("SaleId");
            });

            modelBuilder.Entity<Inventory>().ToTable("Inventory");
        }
    }
}
