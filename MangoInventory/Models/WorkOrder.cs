using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MangoInventory.Models
{
    public class WorkOrder
    {
        public int Id { get; set; }
        public string QuotationId { get; set; }
        public string WorkOrderId { get; set; }
        public Quotation Quotation { get; set; }
    }
}