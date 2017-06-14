namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV29 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Quotations", "CategoryId");
            CreateIndex("dbo.Quotations", "TypeId");
            CreateIndex("dbo.Quotations", "ProductId");
            CreateIndex("dbo.Quotations", "SubCategoryId");
            AddForeignKey("dbo.Quotations", "CategoryId", "dbo.Categories", "Id");
            AddForeignKey("dbo.Quotations", "TypeId", "dbo.DeviceTypes", "Id");
            AddForeignKey("dbo.Quotations", "ProductId", "dbo.Products", "Id");
            AddForeignKey("dbo.Quotations", "SubCategoryId", "dbo.SubCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quotations", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.Quotations", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Quotations", "TypeId", "dbo.DeviceTypes");
            DropForeignKey("dbo.Quotations", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Quotations", new[] { "SubCategoryId" });
            DropIndex("dbo.Quotations", new[] { "ProductId" });
            DropIndex("dbo.Quotations", new[] { "TypeId" });
            DropIndex("dbo.Quotations", new[] { "CategoryId" });
        }
    }
}
