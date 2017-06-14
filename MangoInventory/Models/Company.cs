using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace MangoInventory.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Company Name cannot be empty")]
        [Column(TypeName = "varchar")]
        [MaxLength(50, ErrorMessage = "Name can be at most 50 charachter long")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address cannot be empty")]
        [Column(TypeName = "varchar")]
        [MaxLength(200, ErrorMessage = "Address can be at most 200 charachter long")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Phone No cannot be empty")]

        [DisplayName("Contact No")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Give a valid Phone number")]
       // [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$", ErrorMessage = "Give Valid Contact No")]
        [Column(TypeName = "varchar")]
        [MaxLength(30, ErrorMessage = "Contact No can be at most 30 charachter long")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Email cannot be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Provide a valid email address")]
        [Column(TypeName = "varchar")]
        [MaxLength(20, ErrorMessage = "Email can be at most 20 charachter long")]
        public string Email { get; set; }
        [DataType(DataType.Url, ErrorMessage = "Provide a valid Web Address")]
        [Column(TypeName = "varchar")]
        [MaxLength(30, ErrorMessage = "Web url can be at most 30 charachter long")]
        public string Website { get; set; }
        //[Required]
        //[DataType(DataType.ImageUrl)]
        //[Column(TypeName = "varchar")]
        //[MaxLength(300,ErrorMessage = "File Must be less than 100 KB")]

        public string Image { get; set; }
        [Required(ErrorMessage = "Please browse your image")]

        [Display(Name = "Upload Image")]

        [NotMapped]

        [ValidateFile]
        public HttpPostedFileBase Photo { get; set; }


    }

    public class ValidateFileAttribute : ValidationAttribute
    {
        public override bool IsValid(Object value)
        {
            int maxContent = 1024 * 100;
            string[] allowedExt = new string[] { ".jpg", ".png", ".bmp" };
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return false;
            }
            else if (!allowedExt.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
            {
                ErrorMessage = "Upload an image of type: " + String.Join(", ", allowedExt);
                return false;
            }
            else if (file.ContentLength > maxContent)
            {
                ErrorMessage = "File is too large, max allowed file size is:" + (maxContent / 100).ToString() + "KB";
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}