namespace MSSQLDB
{
    #region

    using System.Data.Entity;

    using MSSQLModels.Models;

    #endregion

    public class MSSQLContext : DbContext
    {
        // Your context has been configured to use a 'MSSQLContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'SupermarketChain.MSSQLData.MSSQLContext' database on your LocalDb instance. 
        // If you wish to target a different database and/or database provider, modify the 'MSSQLContext' 
        // connection string in the application configuration file.
        public MSSQLContext()
            : base("name=MSSQLContext")
        {

        }

        public virtual DbSet<Vendor> Vendors { get; set; }

        public virtual DbSet<Measure> Mesures { get; set; }

        public virtual DbSet<Product> Products { get; set; }
    }
}