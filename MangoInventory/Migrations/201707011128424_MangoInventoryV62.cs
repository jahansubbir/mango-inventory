namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV62 : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE View WorkOrderView
AS
SELECT w.Id,q.ContactPerson, q.CompanyName,q.Address,q.Date,q.QuotationId,w.WorkOrderId,p.Name as Product,p.Model,p.Description,p.Brand,q.Quantity,q.UnitPrice,q.Quantity*q.UnitPrice as Total,e.Name as Employee FROM Quotations as q
INNER JOIN Products as p
ON
q.ProductId=p.Id
INNER JOIN Employees as e
ON
q.EmployeeId=e.Id
INNER JOIN WorkOrders as w
ON
q.QuotationId=w.QuotationId");
            
        }
        
        public override void Down()
        {
           // DropTable("dbo.WorkOrderView");
        }
    }
}
