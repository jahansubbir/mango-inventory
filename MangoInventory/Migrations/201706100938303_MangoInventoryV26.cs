namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV26 : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER PROCEDURE SpStock
@ReceiveDate date,
@MRNo varchar(20),
@Supplier varchar(50),
@ProductId int,
@Quantity decimal,
@UnitPrice decimal,
@Status int,
@Balance decimal,
@Remarks varchar(30)
--@MRRowEffected int OUTPUT,
--@StockRowEffected int OUTPUT
AS
BEGIN
BEGIN TRAN
INSERT INTO MRs (ReceiveDate,MRNo,Supplier,ProductId,Quantity,UnitPrice,Status) VALUES(@ReceiveDate,@MRNo,@Supplier,@ProductId,@Quantity,@UnitPrice,@Status);
INSERT INTO Stocks VALUES(@ProductId,@ReceiveDate,@MRNo,@Quantity,0,@Balance,@Remarks);
COMMIT;
END;");
        }
        
        public override void Down()
        {
        }
    }
}
