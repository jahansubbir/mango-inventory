using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MangoInventory.DBContext;
using MangoInventory.Models;

namespace MangoInventory.DAL
{
    public class QuotationGateway
    {
        MangoDbContext db =new MangoDbContext();

        public int Create(Quotation quotation)
        {
            db.Quotations.Add(quotation);
            return db.SaveChanges();
        }

        public int Edit(Quotation quotation)
        {
           // db.Entry(quotation).State = EntityState.Modified;
           //return db.SaveChanges();
            int rowEffected = db.Database.ExecuteSqlCommand("UPDATE Quotations SET Quantity={0},UnitPrice={1} WHERE Id={2}",
                quotation.Quantity, quotation.UnitPrice, quotation.Id);
            return rowEffected;
        }

        public List<Quotation> GetQuotations()
        {
            return db.Quotations.ToList();
        } 
    }
}