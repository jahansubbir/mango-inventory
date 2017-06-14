using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangoInventory.Models
{
    public class MR
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Receive Date")]
        [DataType(DataType.Date), DisplayFormat( DataFormatString="{dd-MM-yy}", ApplyFormatInEditMode=true )]
        public DateTime ReceiveDate { get; set; }
        
        [DisplayName("MR No")]
        
        public string MRNo { get; set; }
        [Required(ErrorMessage = "Give Supplier Name")]
        public string Supplier { get; set; }
        [DisplayName("Product")]
        [Required]
        public int ProductId { get; set; }
        [Required]
        [DisplayName("Quantity")]
        public decimal Quantity { get; set; }
        [DisplayName("Unit Price")]
        public decimal UnitPrice { get; set; }

        public int Status { get; set; }
        //[ForeignKey("Product")]

        public Product Product { get; set; }


    }
}