using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class OrderDetail
    {
        [JsonIgnore]
        public int IdOrder { get; set; }
        [JsonIgnore]
        public int IdProduct { get; set; }
        public OrderDetailsPK id { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }

        [JsonIgnore]
        public virtual Order order { get; set; }
        public virtual Product product { get; set; }
    }
}
