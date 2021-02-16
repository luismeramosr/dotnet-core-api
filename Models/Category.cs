using System;
using System.Collections.Generic;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public ulong? Active { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
