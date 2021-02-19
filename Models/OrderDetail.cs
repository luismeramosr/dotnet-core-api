﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class OrderDetail
    {
        [JsonIgnore]
        public int idOrder { get; set; }
        [JsonIgnore]
        public int idProduct { get; set; }
        [NotMapped]
        public OrderDetailsPK id { get; set; }
        public double? price { get; set; }
        public int? quantity { get; set; }

        [JsonIgnore]
        public virtual Order order { get; set; }
        public virtual Product product { get; set; }
    }
}
