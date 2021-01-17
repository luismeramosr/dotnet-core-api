using System;
using System.Collections.Generic;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class ProductImage
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string Url { get; set; }
    }
}
