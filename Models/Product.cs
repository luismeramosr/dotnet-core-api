using System;
using System.Collections.Generic;

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

        public int Id { get; set; }
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

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual Vendor IdVendorNavigation { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
