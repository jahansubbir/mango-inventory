using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MangoInventory.Models
{
    public class Unit
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Unit")]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}