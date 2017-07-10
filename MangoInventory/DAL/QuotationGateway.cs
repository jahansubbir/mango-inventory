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
            //db.Entry(quotation).State = EntityState.Modified;
            //return db.SaveChanges();
            int rowEffected = db.Database.ExecuteSqlCommand("UPDATE Quotations SET ContactPerson={0},CompanyName={1},Address={2},Date={3}, " +
                                                            "Quantity={4},UnitPrice={5} WHERE Id={6}",
               quotation.ContactPerson,quotation.CompanyName,quotation.Address,quotation.Date, quotation.Quantity, quotation.UnitPrice, quotation.Id);
            return rowEffected;
        }

        public List<Quotation> GetQuotations()
        {
            return db.Quotations.ToList();
        }

        public int OrderWork(WorkOrder work)
        {
            db.WorkOrders.Add(work);
            int rowEffected = db.SaveChanges();
            return rowEffected;
        }

        public List<WorkOrder> GetWorkOrder()
        {
           return db.WorkOrders.ToList();
        }

        public List<WorkOrderView> GetWorkOrderViews()
        {
            return db.WorkOrderView.ToList();
        } 

        
    }
}