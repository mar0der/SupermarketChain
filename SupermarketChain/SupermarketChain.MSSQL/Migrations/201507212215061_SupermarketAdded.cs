namespace SupermarketChain.MSSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SupermarketAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Supermarkets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SupermarketProducts",
                c => new
                    {
                        Supermarket_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Supermarket_Id, t.Product_Id })
                .ForeignKey("dbo.Supermarkets", t => t.Supermarket_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Supermarket_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SupermarketProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.SupermarketProducts", "Supermarket_Id", "dbo.Supermarkets");
            DropIndex("dbo.SupermarketProducts", new[] { "Product_Id" });
            DropIndex("dbo.SupermarketProducts", new[] { "Supermarket_Id" });
            DropTable("dbo.SupermarketProducts");
            DropTable("dbo.Supermarkets");
        }
    }
}
