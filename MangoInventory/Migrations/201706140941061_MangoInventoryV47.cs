namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV47 : DbMigration
    {
        public override void Up()
        {
           
            Sql(@"DROP VIEW StockView");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StockView",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ProductName = c.String(),
                        Model = c.String(),
                        MovementDate = c.DateTime(nullable: false),
                        Movement = c.String(),
                        Debit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
