using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MangoInventory.Models
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Product")]
        public int ProductId { get; set; }
        [DisplayName("Date")]
        public DateTime MovementDate { get; set; }
        [DisplayName("Moved From/To")]
        public string Movement { get; set; }
        [DisplayName("Debit")]
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
        public string Remarks { get; set; }
        public Product Product { get; set; }

    }
}