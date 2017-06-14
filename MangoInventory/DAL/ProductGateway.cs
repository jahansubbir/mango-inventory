using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MangoInventory.DBContext;
using MangoInventory.Models;

namespace MangoInventory.DAL
{
    public class ProductGateway
    {
        MangoDbContext db = new MangoDbContext();

        public int SaveProduct(Product product)
        {
            db.Products.Add(product);
            return db.SaveChanges();
        }

        public int EditProduct(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
           return db.SaveChanges();
        }

        public List<Product> GetProducts()
        {
            return db.Products.ToList();
        } 
    }
}