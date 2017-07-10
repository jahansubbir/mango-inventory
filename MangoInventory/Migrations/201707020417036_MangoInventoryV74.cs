namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV74 : DbMigration
    {
        public override void Up()
        {
            //DropTable("dbo.WorkOrders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.WorkOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuotationId = c.String(),
                        WorkOrderNo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
