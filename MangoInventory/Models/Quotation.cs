using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangoInventory.Models
{
    public class Quotation
    {
        public int Id { get; set; }
        [DisplayName("Contact Person")]
        [Required(ErrorMessage = "Whom are you contacting")]
       // [PlaceHolder("Contact Person")]
        public string ContactPerson { get; set; }
        [Required(ErrorMessage = "Give Company Name")]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Company Address")]
        [DisplayName("Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Pick Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Select Category")]
        [DisplayName("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        [Required(ErrorMessage = "Select Sub Category")]
        [DisplayName("Sub Category")]
        public int SubCategoryId { get; set; }

        public SubCategory SubCategory { get; set; }
        [DisplayName("Type")]
        
        public int TypeId { get; set; }
        [ForeignKey("Type")]
        public DeviceType DeviceType { get; set; }
        [DisplayName("Product")]
        [Required(ErrorMessage = "Select Product")]

        public int ProductId { get; set; }
        [Required(ErrorMessage = "Write Quantity")]
        public Product Product { get; set; }
        public decimal Quantity { get; set; }
        [Required(ErrorMessage = "Unit Price")]
        public decimal UnitPrice { get; set; }
        [Required(ErrorMessage = "Who is preparing the quotation")]
        [DisplayName("Employee")]
        public int EmployeeId { get; set; }
        [DisplayName("Quotation")]
        public string QuotationId { get; set; }

        public int Status { get; set; }



    }
}