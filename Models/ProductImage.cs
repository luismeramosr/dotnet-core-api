using System;
using System.Collections.Generic;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class ProductImage
    {
        public int Id { get; set; }
        public int? IdProduct { get; set; }
        public string Url { get; set; }

        public virtual Product IdProductNavigation { get; set; }
    }
}
