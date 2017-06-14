using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Script.Serialization;

namespace MangoInventory.Models
{
    [DataContract(IsReference = true)]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Required")]
        [DisplayName("Category")]
        [DataMember]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Required")]
        [DisplayName("Subcategory")]
        public int SubCategoryId { get; set; }
        [Required(ErrorMessage = "Required")]
        [DisplayName("Type")]
        public int TypeId { get; set; }
        [Required]
        [DisplayName("Product")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "Required")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Brand { get; set; }

        //public byte[] Image { get; set; }
        [Required]
        [DisplayName("Unit")]
        public int UnitId { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public HttpPostedFileBase File { get; set; }
        [DisplayName("Image")]
        public string ImagePath { get; set; }

        //[DisplayName("Cost Price")]
        //public decimal CostPrice { get; set; }
        //[DisplayName("Min. Sales Price")]
        //public decimal MinSalesPrice { get; set; }
       // [ForeignKey("Category")]
        [ScriptIgnore]
        public virtual Category Category { get; set; }
        //[ForeignKey("Unit")]
        public virtual SubCategory SubCategory { get; set; }
        public DeviceType Type { get; set; }
        public virtual Unit Unit { get; set; }
        public ICollection<MR> Mrs { get; set; }
        public ICollection<Quotation> Quotations { get; set; }

        
       
    }
}