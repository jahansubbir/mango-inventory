﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MangoInventory.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}