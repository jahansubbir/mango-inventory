namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV32 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "TypeId", "dbo.DeviceTypes");
            DropIndex("dbo.Products", new[] { "TypeId" });
            CreateIndex("dbo.Products", "TypeId");
            AddForeignKey("dbo.Products", "TypeId", "dbo.DeviceTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "TypeId", "dbo.DeviceTypes");
            DropIndex("dbo.Products", new[] { "TypeId" });
            CreateIndex("dbo.Products", "TypeId");
            AddForeignKey("dbo.Products", "TypeId", "dbo.DeviceTypes", "Id", cascadeDelete: true);
        }
    }
}
