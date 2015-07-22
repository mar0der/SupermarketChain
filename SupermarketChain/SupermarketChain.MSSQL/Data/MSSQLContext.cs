namespace MSSQLDB
{
    #region

    using System;
    using System.Data.Entity;

    using MSSQLModels.Models;

    using SupermarketChain.MSSQL;
    using SupermarketChain.MSSQL.Migrations;
    using SupermarketChain.MSSQL.Models;

    #endregion

    public class MSSQLContext : DbContext
    {
        public MSSQLContext()
            : base("MSSQLContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MSSQLContext, Configuration>());
        }

        public virtual DbSet<Vendor> Vendors { get; set; }

        public virtual DbSet<Measure> Measures { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Supermarket> Supermarkets { get; set; }

        public virtual DbSet<SupermarketProduct> SupermarketProducts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<SupermarketProduct>()
                .HasKey(sp => new { sp.SupermarketId, sp.ProductId });

            modelBuilder.Entity<Supermarket>()
                .HasMany(s => s.SupermarketProducts)
                .WithRequired(sp => sp.Supermarket)
                .HasForeignKey(sp => sp.SupermarketId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.SupermarketProducts)
                .WithRequired(sp => sp.Product)
                .HasForeignKey(sp => sp.ProductId);

            modelBuilder.Entity<SupermarketProduct>()
                .HasKey(sp => new {sp.ProductId, sp.SupermarketId, sp.Quantity, sp.SaleDate});
            base.OnModelCreating(modelBuilder);
        }
    }
}