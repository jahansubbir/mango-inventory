namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV38 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "PasswordHash", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "PasswordHash");
        }
    }
}
