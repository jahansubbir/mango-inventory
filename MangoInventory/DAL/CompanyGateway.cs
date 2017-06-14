using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MangoInventory.DBContext;
using MangoInventory.Models;

namespace MangoInventory.DAL
{
    public class CompanyGateway
    {
        MangoDbContext db =new MangoDbContext();
        public int CreateCompany(Company company)
        {
            db.Companies.Add(company);
           return db.SaveChanges();
        }

        public List<Company> GetCompanies()
        {
            return db.Companies.ToList();
        }

        public int Edit(Company company)
        {
            db.Entry(company).State = EntityState.Modified;
           return db.SaveChanges();
        }

        public int Delete(Company company)
        {
            db.Companies.Remove(company);
            return db.SaveChanges();
        }
    }
}