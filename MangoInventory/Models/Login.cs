using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MangoInventory.Models
{
    public class Login
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter User Id")]
        [DisplayName("User ID")]
        public string UserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [PasswordPropertyText(true)]
        public string Password { get; set; }

        public DateTime Date { get; set; }

    }
}