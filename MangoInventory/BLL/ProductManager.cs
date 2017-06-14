using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MangoInventory.DAL;
using MangoInventory.Models;

namespace MangoInventory.BLL
{
    public class ProductManager
    {
        ProductGateway productGateway=new ProductGateway();

        public string SaveProduct(Product product)
        {
            if (productGateway.GetProducts().Find(a=>a.Model==product.Model)!=null)
            {
                return "product already exist";
            }
            else
            {
                if (productGateway.SaveProduct(product)>0)
                {
                    return "Product has been Saved to Database";
                }
                return "Failed";
            }
        }

        public string EditProduct(Product product)
        {
            if (productGateway.EditProduct(product)>0)
            {
                return "Edited";
            }
            return "Failed";
        }

        public List<Product> GetProducts()
        {
            return productGateway.GetProducts().ToList();
        } 
        
    }
}