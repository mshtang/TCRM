using System.Data.Entity;
using TCRMDataManager.Library.Models;

namespace TCRMDataManager.Library.DataAccess
{
    class TCRMContext : DbContext
    {
        public DbSet<UserModel> User { get; set; }
        public DbSet<ProductModel> Product { get; set; }

        public TCRMContext() : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<UserModel>().ToTable("User", "dbo");
            modelBuilder.Entity<ProductModel>().ToTable("Product", "dbo");
        }
    }
}
