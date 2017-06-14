namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV30 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "TypeId", "dbo.DeviceTypes");
            DropForeignKey("dbo.Products", "SubCategoryId", "dbo.SubCategories");
            DropIndex("dbo.Products", new[] { "TypeId" });
            DropIndex("dbo.Products", new[] { "SubCategoryId" });
            CreateIndex("dbo.Products", "TypeId");
            CreateIndex("dbo.Products", "SubCategoryId");
            AddForeignKey("dbo.Products", "TypeId", "dbo.DeviceTypes", "Id");
            AddForeignKey("dbo.Products", "SubCategoryId", "dbo.SubCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.Products", "TypeId", "dbo.DeviceTypes");
            DropIndex("dbo.Products", new[] { "SubCategoryId" });
            DropIndex("dbo.Products", new[] { "TypeId" });
            CreateIndex("dbo.Products", "SubCategoryId");
            CreateIndex("dbo.Products", "TypeId");
            AddForeignKey("dbo.Products", "SubCategoryId", "dbo.SubCategories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "TypeId", "dbo.DeviceTypes", "Id", cascadeDelete: true);
        }
    }
}
