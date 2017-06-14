namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV50 : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.StockView", "Name", c => c.String());
            //AddColumn("dbo.StockView", "Date", c => c.DateTime(nullable: false));
            //DropColumn("dbo.StockView", "ProductName");
            //DropColumn("dbo.StockView", "MovementDate");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.StockView", "MovementDate", c => c.DateTime(nullable: false));
            //AddColumn("dbo.StockView", "ProductName", c => c.String());
            //DropColumn("dbo.StockView", "Date");
            //DropColumn("dbo.StockView", "Name");
        }
    }
}
