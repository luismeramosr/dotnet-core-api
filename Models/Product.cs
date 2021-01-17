using System;
using System.Collections.Generic;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class Product
    {
        public string Id { get; set; }
        public int CategoryId { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public int Stock { get; set; }
    }
}
