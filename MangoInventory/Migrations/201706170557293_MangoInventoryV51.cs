namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV51 : DbMigration
    {
        public override void Up()
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
            //            Description = c.String(),
            //            Brand = c.String(),
            //            Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            Total = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            Employee = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            Sql(@"CREATE VIEW QuotationView
AS
SELECT     q.Id, q.ContactPerson, q.CompanyName, q.Address, q.Date, q.QuotationId AS Quotation, p.Name AS Product, p.Model, q.Quantity, q.UnitPrice, q.UnitPrice * q.Quantity AS Total, e.Name AS Employee, 
                      p.Brand, p.Description
FROM         Quotations AS q INNER JOIN
                      Products AS p ON q.ProductId = p.Id INNER JOIN
                      Employees AS e ON q.EmployeeId = e.Id");
        }
        
        public override void Down()
        {
            //DropTable("dbo.QuotationViews");
        }
    }
}
