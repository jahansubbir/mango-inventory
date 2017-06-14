using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MangoInventory.Models
{
    [Table("MrView")]

    public class MrView
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }
        public string Supplier { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string MRNo { get; set; }
        public DateTime ReceiveDate { get; set; }

    }
}