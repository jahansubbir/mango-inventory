namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV27 : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW MrView
AS
SELECT     M.Id,M.Supplier, P.Name, P.Model, M.Quantity, M.UnitPrice, M.MRNo, M.ReceiveDate
FROM         MRs AS M INNER JOIN
           Products AS P ON M.ProductId = P.Id
");
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MrView", "Id", c => c.Int(nullable: false, identity: true));
        }
    }
}
