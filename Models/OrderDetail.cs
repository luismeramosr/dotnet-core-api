using System;
using System.Collections.Generic;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class OrderDetail
    {
        public int IdOrder { get; set; }
        public int IdProduct { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }

        public virtual Order IdOrderNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
