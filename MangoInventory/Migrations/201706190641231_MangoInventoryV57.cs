namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV57 : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.QuotationView", "Designation", c => c.String());
            //AddColumn("dbo.QuotationView", "Department", c => c.String());
        }
        
        public override void Down()
        {
            //DropColumn("dbo.QuotationView", "Department");
            //DropColumn("dbo.QuotationView", "Designation");
        }
    }
}
