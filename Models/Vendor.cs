using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
#nullable disable

namespace dotnet_core_api.Models
{
    public partial class Vendor
    {
        public Vendor()
        {
            Products = new HashSet<Product>();
        }

        public int idVendor { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
