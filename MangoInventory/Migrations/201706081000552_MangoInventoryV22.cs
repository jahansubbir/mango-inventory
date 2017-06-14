namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV22 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MRs", "Product_Id", "dbo.Products");
            DropIndex("dbo.MRs", new[] { "Product_Id" });
            AddColumn("dbo.MRs", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.MRs", "ProductId");
            AddForeignKey("dbo.MRs", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            DropColumn("dbo.MRs", "Product_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MRs", "Product_Id", c => c.Int());
            DropForeignKey("dbo.MRs", "ProductId", "dbo.Products");
            DropIndex("dbo.MRs", new[] { "ProductId" });
            DropColumn("dbo.MRs", "ProductId");
            CreateIndex("dbo.MRs", "Product_Id");
            AddForeignKey("dbo.MRs", "Product_Id", "dbo.Products", "Id");
        }
    }
}
