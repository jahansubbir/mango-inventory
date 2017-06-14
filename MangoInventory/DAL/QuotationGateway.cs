using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            db.Entry(quotation).State = EntityState.Modified;
           return db.SaveChanges();
        }

        public List<Quotation> GetQuotations()
        {
            return db.Quotations.ToList();
        } 
    }
}