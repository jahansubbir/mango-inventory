namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV60 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Status", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Status", c => c.Int(nullable: false));
        }
    }
}
