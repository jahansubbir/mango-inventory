using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MangoInventory.Models;

namespace MangoInventory.DBContext
{
    public class MangoDbContext : DbContext
    {
        public MangoDbContext()
            : base("mangoConnection")
        {

        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<DeviceType> Types { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<MR> Mrs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<Requisition> Requisitions { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<MrView> MrView { get; set; }
        public DbSet<StockView> StockView { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasRequired(m => m.Unit)
                .WithMany(t => t.Products)
                .HasForeignKey(m => m.UnitId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasRequired(m => m.Category)
                .WithMany(t => t.Products)
                .HasForeignKey(m => m.CategoryId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Product>()
                .HasRequired(m => m.SubCategory)
                .WithMany(t => t.Products)
                .HasForeignKey(m => m.SubCategoryId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Product>()
                .HasRequired(m => m.Type)
                .WithMany(t => t.Products)
                .HasForeignKey(m => m.TypeId)
                .WillCascadeOnDelete(false);
            //test
            modelBuilder.Entity<Category>()
                .HasMany(p => p.Products);
            modelBuilder.Entity<SubCategory>()
                .HasMany(p => p.Products);
            modelBuilder.Entity<DeviceType>()
                .HasMany(p => p.Products);
            //

            modelBuilder.Entity<DeviceType>()
                .HasRequired(m => m.Category)
                .WithMany(t => t.DeviceTypes)
                .HasForeignKey(m => m.CategoryId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<DeviceType>()
                .HasRequired(m => m.SubCategory)
                .WithMany(t => t.Types)
                .HasForeignKey(m => m.SubCategoryId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Quotation>()
                .HasRequired(q=>q.Category)
                .WithMany(q=>q.Quotations)
                .HasForeignKey(q=>q.CategoryId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Quotation>()
                .HasRequired(q=>q.SubCategory)
                .WithMany(q=>q.Quotations)
                .HasForeignKey(q=>q.SubCategoryId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Quotation>()
               .HasRequired(q => q.DeviceType)
               .WithMany(q => q.Quotations)
               .HasForeignKey(q => q.TypeId)
               .WillCascadeOnDelete(false);
            modelBuilder.Entity<Quotation>()
              .HasRequired(q => q.Product)
              .WithMany(q => q.Quotations)
              .HasForeignKey(q => q.ProductId)
              .WillCascadeOnDelete(false);
          
        }

       

    }
}