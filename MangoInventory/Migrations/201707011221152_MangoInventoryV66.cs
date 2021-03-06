namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV66 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkOrders",
               c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
            //            ContactPerson = c.String(),
            //            CompanyName = c.String(),
            //            Address = c.String(),
            //            Date = c.DateTime(nullable: false),
                        QuotationId = c.String(),
                       WorkOrderId = c.String(),
            //            Product = c.String(),
            //            Model = c.String(),
            //            Description = c.String(),
            //            Brand = c.String(),
            //            Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            Total = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            Employee = c.String(),
            //            Designation = c.String(),
            //            Department = c.String(),
                   })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WorkOrders");
        }
    }
}
