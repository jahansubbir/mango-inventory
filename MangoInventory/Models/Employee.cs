using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangoInventory.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DisplayName("Contact No")]
        public string ContactNo { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DisplayName("Employee Id")]
        public string EmpId { get; set; }

        [Required]
        [DisplayName("Designation")]
        public int DesignationId { get; set; }
        [Required]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        [Required]
        [DisplayName("Password")]
        
        [DataType(DataType.Password)]
        [StringLength(Int32.MaxValue, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string PasswordHash { get; set; }
        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        [Compare("PasswordHash", ErrorMessage = "Password doesn't match")]
        [StringLength(Int32.MaxValue, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string ConfirmPassword { get; set; }

        public int Status { get; set; }

        public Department Department { get; set; }
        public Designation Designation { get; set; }

    }
}