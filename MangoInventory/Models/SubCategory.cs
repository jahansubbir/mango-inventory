using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MangoInventory.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        [DisplayName("Category")]
        [Required(ErrorMessage = "Select Category")]
        public int CategoryId { get; set; }
        [DisplayName("Sub Category")]
        [Required(ErrorMessage = "Name the SubCategory")]
        public string Name { get; set; }

        public Category Category { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<DeviceType> Types { get; set; }
        public ICollection<Quotation> Quotations { get; set; }
        

    }
}