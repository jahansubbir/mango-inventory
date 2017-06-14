namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV20 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MRs", "ModelId", "dbo.Products");
            DropIndex("dbo.MRs", new[] { "ModelId" });
            AddColumn("dbo.MRs", "Product_Id", c => c.Int());
            CreateIndex("dbo.MRs", "Product_Id");
            AddForeignKey("dbo.MRs", "Product_Id", "dbo.Products", "Id");
            DropColumn("dbo.MRs", "ProductId");
            DropColumn("dbo.MRs", "ModelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MRs", "ModelId", c => c.Int(nullable: false));
            AddColumn("dbo.MRs", "ProductId", c => c.Int(nullable: false));
            DropForeignKey("dbo.MRs", "Product_Id", "dbo.Products");
            DropIndex("dbo.MRs", new[] { "Product_Id" });
            DropColumn("dbo.MRs", "Product_Id");
            CreateIndex("dbo.MRs", "ModelId");
            AddForeignKey("dbo.MRs", "ModelId", "dbo.Products", "Id");
        }
    }
}
