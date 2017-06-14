using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MangoInventory.DBContext;
using MangoInventory.Models;
using Microsoft.Ajax.Utilities;

namespace MangoInventory.DAL
{
    public class MrGateway
    {
        MangoDbContext db=new MangoDbContext();

        public int Create(MR mr)
        {
            db.Mrs.Add(mr);
            return db.SaveChanges();
        }

        public int Edit(MR mr)
        {
            db.Entry(mr).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public List<MR> GetMr()
        {
            return db.Mrs.ToList();
        }

        public string GetMrNo(string supplier, DateTime date)
        {
            var mrs = db.Mrs.Where(a => a.ReceiveDate == date && a.Supplier == supplier).DistinctBy(a => a.MRNo);

            //return mrs.Count(a => a.Id);
            int count = mrs.Count() + 1;
            return count.ToString("0000");
            //return mrs.ToString("0000");
        }
        public int ReceiveMaterial(MR mr, Stock stock)
        {
            int rowEffected=    db.Database.ExecuteSqlCommand(
                    "Exec SpStock @ReceiveDate={0},@MRNo={1},@Supplier={2},@ProductId={3},@Quantity={4},@UnitPrice={5},@Status={6},@Balance={7},@Remarks={8}",mr.ReceiveDate,mr.MRNo,mr.Supplier,mr.ProductId,mr.Quantity,mr.UnitPrice,mr.Status,stock.Balance,stock.Remarks);
            return rowEffected;
            /* db.Database.SqlQuery("SpStock", mr.ReceiveDate, mr.MRNo, mr.Supplier, mr.ProductId, mr.Quantity,
                mr.UnitPrice, mr.Status, stock.Balance, stock.Remarks);*/
        }
    }
}