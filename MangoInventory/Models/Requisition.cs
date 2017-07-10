using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MangoInventory.Models
{
    public class Requisition
    {
        public int Id { get; set; }
        [Required]
        public int WorkOrderId { get; set; }

        public string RequisitionId { get; set; }
        [Required]
        public int  EmployeeId { get; set; }

        [Required]
        [DisplayName("Product")]
        public int ProductId { get; set; }

        public string Project { get; set; }
        [Required]
        public string Quantity { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public Product Product { get; set; }

    }
}