using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MangoInventory.DBContext;
using MangoInventory.Models;

namespace MangoInventory.DAL
{
    public class CategoryGateway
    {
        private MangoDbContext db = new MangoDbContext();

        public int CreateCategory(Category category)
        {
            db.Categories.Add(category);
            return db.SaveChanges();
        }

        public List<Category> GetCategories()
        {
            return db.Categories.ToList();
        }

        public int EditCategory(Category category)
        {
            db.Entry(category).State = EntityState.Modified;
           return db.SaveChanges();
        }

        public int CreateSubCategory(SubCategory subCategory)
        {
            db.SubCategories.Add(subCategory);
           return db.SaveChanges();
        }

        public List<SubCategory> GetSubCategories()
        {
            db.SubCategories.Include(s => s.Name);
            return db.SubCategories.ToList();
        }

        public int EditSubCategory(SubCategory subCategory)
        {

            db.Entry(subCategory).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public int CreateType(DeviceType type)
        {
            db.Types.Add(type);
           return db.SaveChanges();
        }

        public int EditType(DeviceType type)
        {
            db.Entry(type).State = EntityState.Modified;
            return db.SaveChanges();
        }
    }
}