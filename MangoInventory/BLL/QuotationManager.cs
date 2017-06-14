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

        public string Create(Quotation quotation)
        {
            if (quotationGateway.GetQuotations().Contains(quotation))
            {
                return "Quotation already generated";
            }
            else
            {
                if (quotationGateway.Create(quotation)>0)
                {
                    return "Quotation Generated";
                }
                return "Failed";
            }

        }

        public string Edit(Quotation quotation)
        {
            if (quotationGateway.Edit(quotation)>0)
            {
                return "Edited";
            }
            return "Failed";

        }



        public string GetQuotationId(Quotation quotation)
        {
            var quotations =
                quotationGateway.GetQuotations()
                    .Where(a => a.Date == quotation.Date && a.CompanyName == quotation.CompanyName)
                    .DistinctBy(a => a.QuotationId);
            int count = quotations.Count() + 1;
            string category = quotation.Category.Name;
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
            string quotationId = "Q-" + quotationTitle + "-" + quotation.Date.ToString("yymmdd") + "-" + count.ToString("0000");
            return quotationId;

        }
    }
}