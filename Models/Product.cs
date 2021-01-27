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
            ProductImages = new HashSet<ProductImage>();
        }

        public uint Id { get; set; }
        public uint CategoryId { get; set; }
        public uint VendorId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public int Stock { get; set; }
        public int StockMin { get; set; }

        [JsonIgnore]
        public virtual Category Category { get; set; }
        [JsonIgnore]
        public virtual Vendor Vendor { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
