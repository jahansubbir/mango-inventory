using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MangoInventory.DAL;
using MangoInventory.Models;

namespace MangoInventory.BLL
{
    public class MrManager
    {
        MrGateway mrGateway=new MrGateway();
        ProductGateway productGateway=new ProductGateway();
        Stock stock=new Stock();
        StockGateway stockGateway=new StockGateway();
        public string CreateMR(List<MR> mrs)
        {
            int i = 0;
            foreach (MR mr in mrs)
            {

                stock.ProductId = mr.ProductId;
                stock.MovementDate = mr.ReceiveDate;
                stock.Movement = mr.MRNo;
                stock.Debit = mr.Quantity;
                stock.Balance = stock.Debit + stockGateway.CountProduct(stock.ProductId);
                stock.Remarks = "New Purchased";
                i += mrGateway.ReceiveMaterial(mr, stock);
                //j=stockGateway.StockProduct(stock);
                stock.Debit = 0;
                stock.Credit = 0;
                stock.Balance = 0;

            }
            if (i > 0)
            {
                return i + " types materials have been received and send to stock";
            }
            else
            {
                return "Reception Failed";
            }
        }

        public List<MR> GetMrs()
        {
            return mrGateway.GetMr();
        }
        public List<MR> GetMrs(string mrNo)
        {
            return mrGateway.GetMr().Where(a=>a.MRNo==mrNo).ToList();
        }

        public string GetMrNo(string supplier, DateTime date)
        {
            return mrGateway.GetMrNo(supplier,date);
        }

        public MrView SessionReceive(MR mr)
        {
            // List<MrViewModel> mrList=new List<MrViewModel>();
            Product product = productGateway.GetProducts().Find(a => a.Id == mr.ProductId);
            MrView model = new MrView();
            model.Name = product.Name;
            model.Model = product.Model;
            model.MRNo = mr.MRNo;
            model.Quantity = mr.Quantity;
            model.ReceiveDate = mr.ReceiveDate;
            model.Supplier = mr.Supplier;
            model.UnitPrice = mr.UnitPrice;
            // mrList.Add(model);
            return model;

        }
    }
}