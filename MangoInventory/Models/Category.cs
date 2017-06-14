using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using Newtonsoft.Json;

namespace MangoInventory.Models
{
    public class Category
    {
        public int Id { get; set; }
        [DisplayName("Category")]
        public string Name { get; set; }
        public virtual ICollection<SubCategory> SubCategories { get; set; }
        public virtual ICollection<DeviceType> DeviceTypes { get; set; }
        [JsonIgnore]
        [IgnoreDataMember] 
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Quotation> Quotations { get; set; }

    }
}