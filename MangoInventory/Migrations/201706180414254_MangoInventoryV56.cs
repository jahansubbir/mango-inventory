namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV56 : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE View QuotationView
AS
SELECT q.Id,q.ContactPerson, q.CompanyName,q.Address,q.Date,q.QuotationId,p.Name as Product,p.Model,p.Description,p.Brand,q.Quantity,q.UnitPrice,q.Quantity*q.UnitPrice as Total,e.Name as Employee FROM Quotations as q
INNER JOIN Products as p
ON
q.ProductId=p.Id
INNER JOIN Employees as e
ON
q.EmployeeId=e.Id");
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
        
        public override void Down()
        {
            //DropTable("dbo.QuotationViews");
        }
    }
}
