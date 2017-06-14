namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV46 : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW StockView 
AS
SELECT s.Id,p.Name,p.Model,s.MovementDate as Date,s.Movement,s.Debit,s.Credit,s.Balance,u.Name as Unit, s.Remarks FROM Products as p
INNER JOIN Stocks as s
ON
s.ProductId=p.Id
INNER JOIN Units as u 
ON
p.UnitId=u.Id ");
        }
        
        public override void Down()
        {
        }
    }
}
