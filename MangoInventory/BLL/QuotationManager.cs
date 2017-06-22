using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MangoInventory.DAL;
using MangoInventory.Models;
using Microsoft.Ajax.Utilities;

namespace MangoInventory.BLL
{
    public class QuotationManager
    {
        QuotationGateway quotationGateway=new QuotationGateway();
        ProductGateway productGateway=new ProductGateway();
        EmployeeGateway employeeGateway=new EmployeeGateway();
        CategoryGateway categoryGateway=new CategoryGateway();

        public string Create(List<Quotation> quotations)
        {
            int total = quotations.Sum(quotation => quotationGateway.Create(quotation));
            return "Total Quantity: " + total;

        }

        public Quotation Edit(Quotation quotation)
        {
            if (quotationGateway.Edit(quotation)>0)
            {
                return quotationGateway.GetQuotations().Find(a => a.QuotationId == quotation.QuotationId);
            }
            else
            {
                return null;
            }
        }


        public string GetQuotationId(Quotation quotation)
        {
            var quotations =
                quotationGateway.GetQuotations()
                    .Where(a => a.Date == quotation.Date && a.CompanyName == quotation.CompanyName)
                    .DistinctBy(a => a.QuotationId);
            int count = quotations.Count() + 1;
            string category = categoryGateway.GetCategories().Find(a => a.Id == quotation.CategoryId).Name;
            string[] words = category.Split(' ');
            string quotationTitle;
            int length = words.Length;
            if (length>1)
            {
                quotationTitle = words[0].Substring(0, 1) + words[1].Substring(0, 1);
            }
            else
            {
                quotationTitle = words[0].Substring(0, 2);
            }
            string quotationId = "Q-" + quotationTitle + "-" + quotation.Date.ToString("yyMMdd") + "-" + count.ToString("000");
            return quotationId;

        }
        public QuotationView SessionReceive(Quotation quotation)
        {
            List<QuotationView> mrList = new List<QuotationView>();
            Product product = productGateway.GetProducts().Find(a => a.Id == quotation.ProductId);
            Employee employee = employeeGateway.GetEmployees().Find(a => a.Id == quotation.EmployeeId);
            QuotationView model = new QuotationView();
            //model.Id = quotation.Id;
            model.ContactPerson = quotation.ContactPerson;
            model.CompanyName = quotation.CompanyName;
            model.Address = quotation.Address;
            model.Date = quotation.Date;
            model.QuotationId = quotation.QuotationId;
            model.Product = product.Name;
            model.Model = product.Model;
           
            model.Quantity = quotation.Quantity;

            model.Quantity = quotation.Quantity;
            model.UnitPrice = quotation.UnitPrice;
            model.Description = product.Description;
            model.Employee = employee.Name;
            model.Total = quotation.Quantity*quotation.UnitPrice;
            model.Brand = product.Brand;
            // mrList.Add(model);
            return model;

        }
    }
}