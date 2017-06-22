namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV55 : DbMigration
    {
        public override void Up()
        {
            //DropTable("dbo.QuotationViews");
        }
        
        public override void Down()
        {
            //CreateTable(
            //    "dbo.QuotationViews",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            ContactPerson = c.String(),
            //            CompanyName = c.String(),
            //            Address = c.String(),
            //            Date = c.DateTime(nullable: false),
            //            QuotationId = c.String(),
            //            Product = c.String(),
            //            Model = c.String(),
            //            Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            Total = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            Employee = c.String(),
            //            Brand = c.String(),
            //            Description = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
        }
    }
}
