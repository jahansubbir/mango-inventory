using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace MangoInventory.Models
{
    public class WorkOrder
    {
        [Key]
        public int Id { get; set; }
        public string QuotationId { get; set; }
        
        public string WorkOrderNo { get; set; }

    }
}