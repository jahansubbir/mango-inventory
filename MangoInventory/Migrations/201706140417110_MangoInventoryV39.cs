namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV39 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "PasswordHash", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "PasswordHash", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
