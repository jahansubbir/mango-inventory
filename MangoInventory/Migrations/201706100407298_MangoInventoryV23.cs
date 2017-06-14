namespace MangoInventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MangoInventoryV23 : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW MrView
AS
SELECT     M.Supplier, P.Name, P.Model, M.Quantity, M.UnitPrice, M.MRNo, M.ReceiveDate
FROM         MRs AS M INNER JOIN
           Products AS P ON M.ProductId = P.Id
");
        }
        
        public override void Down()
        {
        }
    }
}
