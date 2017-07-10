namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV76 : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW WorkOrderView
AS
SELECT     w.Id, q.ContactPerson, q.CompanyName, q.Address, q.Date, q.QuotationId,w.WorkOrderNo, p.Name AS Product, p.Model, p.Description, p.Brand, q.Quantity, q.UnitPrice, q.Quantity * q.UnitPrice AS Total, 
                      e.Name AS Employee, d.Name AS Designation, de.Name AS Department
FROM                  Quotations AS q INNER JOIN
                      Products AS p ON q.ProductId = p.Id INNER JOIN
                      Employees AS e ON q.EmployeeId = e.Id INNER JOIN
                      Designations AS d ON e.DesignationId = d.Id INNER JOIN
                      Departments AS de ON e.DepartmentId = de.Id INNER JOIN
                      WorkOrders as w ON q.QuotationId=w.QuotationId");
        }
        
        public override void Down()
        {
            //DropTable("dbo.WorkOrderView");
        }
    }
}
