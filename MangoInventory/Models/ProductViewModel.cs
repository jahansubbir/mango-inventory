﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MangoInventory.Models
{
    public class ProductViewModel
    {
        [ReadOnly(true)]
        public Category Category { get; set; }

    }
}