namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV5 : DbMigration
    {
        public override void Up()
        {
            //RenameTable(name: "dbo.QuotationViews", newName: "QuotationView");
        }
        
        public override void Down()
        {
            //RenameTable(name: "dbo.QuotationView", newName: "QuotationViews");
        }
    }
}
