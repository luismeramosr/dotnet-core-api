using System;
using System.Collections.Generic;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class ProductImage
    {
        public uint Id { get; set; }
        public uint ProductId { get; set; }
        public string Url { get; set; }

        public virtual Product Product { get; set; }
    }
}
