namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV52 : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.QuotationViews", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            //DropColumn("dbo.QuotationViews", "TotalPrice");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.QuotationViews", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            //DropColumn("dbo.QuotationViews", "Total");
        }
    }
}
