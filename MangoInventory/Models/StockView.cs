using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MangoInventory.Models
{
    [Table("StockView")]
    public class StockView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Model { get; set; }
        public DateTime Date { get; set; }
        public string Movement { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
        public string Unit { get; set; }
        public string Remarks { get; set; }


    }
}