using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MangoInventory.Models
{
    [Table("WorkOrderView")]
    public class WorkOrderView
    {
        public int Id { get; set; }
        public string ContactPerson { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public string QuotationId { get; set; }
        public string WorkOrderNo { get; set; }

        public string Product { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public decimal Quantity { get; set; }
        [DisplayName("Unit Price")]
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public string Employee { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
    }
}