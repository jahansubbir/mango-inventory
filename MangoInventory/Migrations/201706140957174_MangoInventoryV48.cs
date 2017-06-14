namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV48 : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.StockView",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false),
            //            ProductName = c.String(),
            //            Model = c.String(),
            //            MovementDate = c.DateTime(nullable: false),
            //            Movement = c.String(),
            //            Debit = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            Remarks = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
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
            //DropTable("dbo.StockView");
        }
    }
}
