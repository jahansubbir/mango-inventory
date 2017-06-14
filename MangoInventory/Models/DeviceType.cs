using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangoInventory.Models
{
    public class DeviceType
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Select Category")]
        [DisplayName("Category")]
       // [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [Required]
        [DisplayName("Sub Category")]
        //[ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }
        [Required]
        [DisplayName("Type")]
        public string Name { get; set; }
        
        public Category Category { get; set; }
        //public SubCategory SubCategory { get; set; }
        public  SubCategory SubCategory { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Quotation> Quotations { get; set; }

    }
}