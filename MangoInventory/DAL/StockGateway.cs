using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MangoInventory.DBContext;
using MangoInventory.Models;

namespace MangoInventory.DAL
{
    public class StockGateway
    {
        MangoDbContext db=new MangoDbContext();
        public decimal CountProduct(int productId)
        {
            decimal balance = 0;
            var product = GetStockStatusByProductId(productId).FindLast(a => a.ProductId == productId);
            if (product != null)
            {
                balance = product.Balance;
            }
            return balance;
        }

        public List<Stock> GetStockStatusByProductId(int productId)
        {
           return db.Stocks.ToList().Where(a=>a.ProductId==productId).ToList();
        }

    }
}