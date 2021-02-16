using System;
using System.Collections.Generic;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class Vendor
    {
        public Vendor()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
