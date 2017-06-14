namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV19 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Model", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Model", c => c.String(nullable: false));
        }
    }
}
