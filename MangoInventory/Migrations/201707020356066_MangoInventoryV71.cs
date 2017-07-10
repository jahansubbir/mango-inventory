namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV71 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requisitions", "WorkOrderId", c => c.Int(nullable: false));
            AlterColumn("dbo.Requisitions", "RequisitionId", c => c.String());
            CreateIndex("dbo.Requisitions", "ProductId");
            AddForeignKey("dbo.Requisitions", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requisitions", "ProductId", "dbo.Products");
            DropIndex("dbo.Requisitions", new[] { "ProductId" });
            AlterColumn("dbo.Requisitions", "RequisitionId", c => c.String(nullable: false));
            DropColumn("dbo.Requisitions", "WorkOrderId");
        }
    }
}
