using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
#nullable disable

namespace dotnet_core_api.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            productsImages = new HashSet<ProductImage>();
        }

        public int IdProduct { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Description { get; set; }
        public int? IdCategory { get; set; }
        public int? IdVendor { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public double? SalePrice { get; set; }
        public int? Stock { get; set; }
        public string ThumbnailUrl { get; set; }
        public int? Sku { get; set; }
        public string Slug { get; set; }


        public virtual Category category { get; set; }
        public virtual Vendor vendor { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductImage> productsImages { get; set; }
    }
}
