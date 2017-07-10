namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV61 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuotationId = c.String(),
                        WorkOrderId = c.String(),
                        Quotation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quotations", t => t.Quotation_Id)
                .Index(t => t.Quotation_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkOrders", "Quotation_Id", "dbo.Quotations");
            DropIndex("dbo.WorkOrders", new[] { "Quotation_Id" });
            DropTable("dbo.WorkOrders");
        }
    }
}
